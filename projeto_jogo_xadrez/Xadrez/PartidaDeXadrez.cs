class PartidaDeXadrez {
    public Tabuleiro Tabuleiro { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Terminada { get; private set; }
    private HashSet<Peca> Pecas;
    private HashSet<Peca> Capturadas;

    public bool Xeque { get; private set; }
    
    public Peca VulneravelEnPassant { get; private set; } 


    public PartidaDeXadrez(){
        Tabuleiro = new Tabuleiro(8,8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        Terminada = false;
        Xeque = false;
        VulneravelEnPassant = null;
        Pecas = new HashSet<Peca>();
        Capturadas = new HashSet<Peca>();
        ColocarPecas();
    }

    public void ValidarOrigem(Posicao pos){
        if (Tabuleiro.GetPeca(pos) == null){
            throw new TabuleiroException("Não existe peça na posição de origem informada!");
        }
        if (JogadorAtual != Tabuleiro.GetPeca(pos).Cor){
            throw new TabuleiroException("A peça escolhida não é sua!");
        }
        if (!Tabuleiro.GetPeca(pos).ExisteMovimentosPossiveis()){
            throw new TabuleiroException("Não há movimentos possíveis para essa peça!");
        }
    }

    public void ValidarDestino(Posicao origem, Posicao destino){
        if (!Tabuleiro.GetPeca(origem).MovimentoPossivel(destino)){
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }

    public Peca Movimentar(Posicao origem, Posicao destino){
        Peca p = Tabuleiro.RetirarPeca(origem);
        p.IncrementarQtdMovimentos();
        Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(p, destino);
        if (pecaCapturada != null){
            Capturadas.Add(pecaCapturada);
        }

        //#jogadaespecial Roque Pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2){
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.IncrementarQtdMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }
        //#jogadaespecial Roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2){
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = Tabuleiro.RetirarPeca(origemT);
            T.IncrementarQtdMovimentos();
            Tabuleiro.ColocarPeca(T, destinoT);
        }

        //#jogadaespecial En Passant
        if (p is Peao){
            if (origem.Coluna != destino.Coluna && pecaCapturada == null){
                Posicao posP;
                if (p.Cor == Cor.Branca){
                    posP = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else {
                    posP = new Posicao(destino.Linha - 1, destino.Coluna);

                }
                pecaCapturada = Tabuleiro.RetirarPeca(posP);
                Capturadas.Add(pecaCapturada);
            }
        }

        return pecaCapturada;
    }

    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada){
        Peca p = Tabuleiro.RetirarPeca(destino);
        p.DecrementarQtdMovimentos();
        if (pecaCapturada != null){
            Tabuleiro.ColocarPeca(pecaCapturada, destino);
            Capturadas.Remove(pecaCapturada);
        }
        Tabuleiro.ColocarPeca(p, origem);

        //#jogadaespecial Roque Pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2){
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna +3);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            Peca T = Tabuleiro.RetirarPeca(destinoT);
            T.DecrementarQtdMovimentos();
            Tabuleiro.ColocarPeca(T, origemT);
        }
        //#jogadaespecial Roque Grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2){
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            Peca T = Tabuleiro.RetirarPeca(destinoT);
            T.DecrementarQtdMovimentos();
            Tabuleiro.ColocarPeca(T, origemT);
        }

        //#jogadaespecial En Passant
        if (p is Peao){
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant){
                Peca peao = Tabuleiro.RetirarPeca(destino);
                Posicao posP;
                if (peao.Cor == Cor.Branca){
                    posP = new Posicao(3, destino.Coluna);
                }
                else {
                    posP = new Posicao(4, destino.Coluna);

                }
                Tabuleiro.ColocarPeca(peao, posP);
                Capturadas.Add(pecaCapturada);
            }
        }
    }

    public void RealizaJoagada(Posicao origem, Posicao destino){
        Peca pecaCapturada = Movimentar(origem, destino);

        if (EstaEmXeque(JogadorAtual)){
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        Peca p = Tabuleiro.GetPeca(destino);

        //#jogadaespecial Promoção
        if (p is Peao){
            if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7)){
                p = Tabuleiro.RetirarPeca(destino);
                Pecas.Remove(p);
                Peca dama = new Dama(Tabuleiro, p.Cor);
                Tabuleiro.ColocarPeca(dama, destino);
                Pecas.Add(dama);
            }
        }

        if (EstaEmXeque(Adversario(JogadorAtual))){
            Xeque = true;
        }
        else{
            Xeque = false;
        }

        if (XequeMate(Adversario(JogadorAtual))){
            Terminada = true; 
        }
        else{
            Turno++;
            MudarJogador();
        }     


        //#jogadaespecial En Passant
        if (p is Peao && destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2){
            VulneravelEnPassant = p;
        }
        else{
            VulneravelEnPassant = null;
        }
    }

    private void MudarJogador(){
        if (JogadorAtual == Cor.Branca){
            JogadorAtual = Cor.Preta;
        }
        else{
            JogadorAtual = Cor.Branca;
        }
    }

    public HashSet<Peca> PecasCapturadas(Cor cor){
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in Capturadas){
            if (x.Cor == cor){
                aux.Add(x);
            }
        }
        return aux;
    }

    public HashSet<Peca> PecasEmJogo(Cor cor){
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in Pecas){
            if (x.Cor == cor){
                aux.Add(x);
            }
        }
        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }

    public void ColcoarNovasPecas(char coluna, int linha, Peca peca){
        Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        Pecas.Add(peca);
    }

    private Cor Adversario(Cor cor){
        if (cor == Cor.Branca){
            return Cor.Preta;
        }
        else{
            return Cor.Branca;
        }
    }

    private Peca Rei(Cor cor){
        foreach (Peca x in PecasEmJogo(cor)){
            if (x is Rei){
                return x;
            }
        }
        return null;
    }

    public bool EstaEmXeque(Cor cor){
        Peca r = Rei(cor);
        if ( r == null){
            throw new TabuleiroException($"Não existe rei da cor {cor} no tabuleiro!");
        }

        foreach (Peca x in PecasEmJogo(Adversario(cor))){
            bool[,] mat = x.MovimentosPossiveis();
            if (mat[r.Posicao.Linha,r.Posicao.Coluna]){
                return true;
            }
        }
        return false;
    }

    public bool XequeMate(Cor cor){
        if (!EstaEmXeque(cor)){
            return false;
        }
        foreach (Peca x in PecasEmJogo(cor)){
            bool[,] mat = x.MovimentosPossiveis();
            for (int i=0; i<Tabuleiro.Linhas; i++){
                for (int j=0; j<Tabuleiro.Colunas; j++){
                    if (mat[i,j]){
                        Posicao origem = x.Posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = Movimentar(origem, destino);
                        bool testeXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem, destino, pecaCapturada);
                        if (!EstaEmXeque(cor)){
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    private void ColocarPecas(){
        ColcoarNovasPecas('a', 1, new Torre(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('c', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('d', 1, new Dama(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('f', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('h', 1, new Torre(Tabuleiro, Cor.Branca));
        ColcoarNovasPecas('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColcoarNovasPecas('h', 2, new Peao(Tabuleiro, Cor.Branca, this));
        

        ColcoarNovasPecas('a', 8, new Torre(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('c', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('d', 8, new Dama(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('f', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('h', 8, new Torre(Tabuleiro, Cor.Preta));
        ColcoarNovasPecas('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColcoarNovasPecas('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
    }
}