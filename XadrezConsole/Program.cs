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
                Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preto), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preto), new Posicao(0, 3));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Branco), new Posicao(2, 4));

                Tela.ImprimirTabuleiro(tabuleiro);
                Console.WriteLine("");
            }
            catch (TabuleiroExcecao ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
