class Tela{
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro){
        for (int l=0; l<tabuleiro.Linhas; l++){
            System.Console.Write(8 - l + " ");
            for (int c=0; c<tabuleiro.Colunas; c++){
                ImprimirPeca(tabuleiro.GetPeca(l, c));
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine("  A B C D E F G H");
    }

    public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis){

        ConsoleColor fundoOriginal = Console.BackgroundColor;
        ConsoleColor fundoALterado = ConsoleColor.DarkGreen;

        for (int l=0; l<tabuleiro.Linhas; l++){
            System.Console.Write(8 - l + " ");
            for (int c=0; c<tabuleiro.Colunas; c++){
                if (posicoesPossiveis[l,c]){
                    Console.BackgroundColor = fundoALterado;
                }
                else{
                    Console.BackgroundColor = fundoOriginal;
                }
                ImprimirPeca(tabuleiro.GetPeca(l, c));
                Console.BackgroundColor = fundoOriginal;
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = fundoOriginal;
    }
    public static void ImprimirPeca(Peca peca){
        if ( peca == null){
            System.Console.Write("- ");
        }
        else{
            if (peca.Cor == Cor.Branca){
                System.Console.Write(peca);
            }
            else{
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            System.Console.Write(" ");
        }
    }

    public static void ImprimirPartida(PartidaDeXadrez p){
        ImprimirTabuleiro(p.Tabuleiro);
        System.Console.WriteLine();
        ImprimirPecasCapturadas(p);

        System.Console.WriteLine();
        System.Console.WriteLine($"Turno: {p.Turno}");
            if (!p.Terminada){
            System.Console.WriteLine($"Aguardando jogada: {p.JogadorAtual}");
            if (p.Xeque){
                System.Console.WriteLine("XEQUE!");
            }
        }
        else{
            System.Console.WriteLine("XEQUEMATE!!!");
            System.Console.WriteLine($"Vencedor: {p.JogadorAtual}");
        }
    }

    public static void ImprimirPecasCapturadas(PartidaDeXadrez p){
        System.Console.WriteLine("Peças capturadas: ");
        System.Console.Write("Brancas: ");
        ImprimirConjunto(p.PecasCapturadas(Cor.Branca));
        System.Console.WriteLine();
        System.Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        ImprimirConjunto(p.PecasCapturadas(Cor.Preta));
        Console.ForegroundColor = aux;
        System.Console.WriteLine();
    }

    public static void ImprimirConjunto(HashSet<Peca> c){
        System.Console.Write("[");
        foreach (Peca x in c){
            System.Console.Write($"{x} ");
        }
        System.Console.Write("]");
    }

    public static PosicaoXadrez LerPosicaoXadrez(){
        string s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s[1] + " ");
        return new PosicaoXadrez(coluna, linha);
    }
}