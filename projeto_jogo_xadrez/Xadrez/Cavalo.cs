class Cavalo : Peca{
    public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor){     
    }
    public override string ToString(){
        return "C";
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
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //NO2
        pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //NE
        pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //NE2
        pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SE
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SE2
        pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SO
        pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }
        //SO2
        pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)){
            mat[pos.Linha, pos.Coluna] = true;
        }

        return mat;
    }
}