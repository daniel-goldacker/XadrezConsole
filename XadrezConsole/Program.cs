using System;
using tabuleiro;
using xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partidaXadrez = new PartidaXadrez();

                while (!partidaXadrez.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaXadrez.Tab);

                    Console.WriteLine("");
                    Console.Write("Digite a posição de origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partidaXadrez.Tab.Peca(origem).MovimentosPosiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaXadrez.Tab, posicoesPossiveis);

                    Console.WriteLine("");
                    Console.Write("Digite a posição de destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    partidaXadrez.ExecutaMovimento(origem, destino);
                }
            }
            catch (TabuleiroExcecao ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
