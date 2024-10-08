 abstract class Peca{
    public Posicao Posicao {get; set; }
    public Cor Cor { get; protected set; }
    public int QtdMovimentos { get; protected set; }
    public Tabuleiro Tabuleiro { get; set; }

    public Peca(Tabuleiro tabuleiro, Cor cor){
        Posicao = null;
        Tabuleiro = tabuleiro;
        Cor = cor;
        QtdMovimentos = 0;
    }

    public void IncrementarQtdMovimentos(){
        QtdMovimentos++;
    }

    public void DecrementarQtdMovimentos(){
        QtdMovimentos--;
    }

    public abstract bool[,] MovimentosPossiveis();

    public bool ExisteMovimentosPossiveis(){
        bool[,] mat = MovimentosPossiveis();
        for (int l = 0; l<Tabuleiro.Linhas; l++){
            for (int c = 0; c<Tabuleiro.Colunas; c++){
                if (mat[l,c]){
                    return true;
                }
            }
        }
        return false;
    }

    public bool MovimentoPossivel(Posicao pos){
        return MovimentosPossiveis()[pos.Linha, pos.Coluna];
    }
        
}