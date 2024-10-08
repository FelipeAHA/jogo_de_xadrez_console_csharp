class Bispo : Peca{
    public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor){     
    }
    public override string ToString(){
        return "B";
    }

    private bool PodeMover(Posicao pos){
        Peca p = Tabuleiro.GetPeca(pos);
        return p == null || p.Cor != Cor;
    }
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        Posicao pos = new Posicao(0,0);
        
        //NO
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != Cor){
                break;
            }
            pos.DefinirPosicao(pos.Linha - 1, pos.Coluna - 1);
        }
        //NE
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != Cor){
                break;
            }
            pos.DefinirPosicao(pos.Linha - 1, pos.Coluna + 1);
        }
        //SE
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != Cor){
                break;
            }
            pos.DefinirPosicao(pos.Linha + 1, pos.Coluna + 1);
        }
        //SO
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != Cor){
                break;
            }
            pos.DefinirPosicao(pos.Linha + 1, pos.Coluna - 1);
        }

        return mat;
    }
}