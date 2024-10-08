class Rei : Peca{
    private PartidaDeXadrez Partida;
    public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor){     
        Partida = partida;
    }
    public override string ToString(){
        return "R";
    }

    private bool PodeMover(Posicao pos){
        Peca p = Tabuleiro.GetPeca(pos);
        return p == null || p.Cor != Cor;
    }

    private bool TesteTorreParaRoque(Posicao pos){
        Peca p = Tabuleiro.GetPeca(pos);
        return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
    }
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0,0);
        
        //N
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //NE
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //E
        pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SE
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //S
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SO
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //O
        pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //NO
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }

        // #jogadaespecial Roque Pequeno
        if (QtdMovimentos == 0 && !Partida.Xeque){
            Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            if (TesteTorreParaRoque(posT1)){
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                if (Tabuleiro.GetPeca(p1) == null && Tabuleiro.GetPeca(p2) == null){
                    mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
            }
        }
        // #jogadaespecial Roque Grande
        if (QtdMovimentos == 0 && !Partida.Xeque){
            Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            if (TesteTorreParaRoque(posT2)){
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                if (Tabuleiro.GetPeca(p1) == null && Tabuleiro.GetPeca(p2) == null && Tabuleiro.GetPeca(p3) == null){
                    mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }
        }

        return mat;
    }
}