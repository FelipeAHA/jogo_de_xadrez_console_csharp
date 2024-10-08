try{

    PartidaDeXadrez p = new PartidaDeXadrez();

    while (!p.Terminada){

        try{
            Console.Clear();
            Tela.ImprimirPartida(p);

            System.Console.WriteLine();
            System.Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
            p.ValidarOrigem(origem);

            bool[,] posicoesPossiveis = p.Tabuleiro.GetPeca(origem).MovimentosPossiveis();

            Console.Clear();
            Tela.ImprimirTabuleiro(p.Tabuleiro, posicoesPossiveis);

            System.Console.WriteLine();
            System.Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            p.ValidarDestino(origem, destino);

            p.RealizaJoagada(origem, destino);
        }
        catch (TabuleiroException e){
            System.Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
    Console.Clear();
    Tela.ImprimirPartida(p);
}
catch (TabuleiroException e){
    System.Console.WriteLine(e.Message);
}

