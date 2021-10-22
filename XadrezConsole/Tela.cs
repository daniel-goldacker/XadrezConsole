using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int l = 0; l < tab.Linhas; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tab.Colunas; c++)
                {
                    ImprimirPeca(tab.Peca(l, c));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPartica(PartidaXadrez partidaXadrez)
        {
            Console.Clear();
            ImprimirTabuleiro(partidaXadrez.Tab);

            Console.WriteLine("");
            ImprimirPecasCapturadas(partidaXadrez);
            Console.WriteLine("");
            Console.WriteLine("Turno: {0}", partidaXadrez.Turno);
            Console.WriteLine("Aguardando jogada da peça: {0}", partidaXadrez.JogadorAtual);
            if (partidaXadrez.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write(" Branca: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.WriteLine("");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.WriteLine("");
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (var item in conjunto)
            {
                Console.Write("{0} ", item);
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOrigical = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int l = 0; l < tab.Linhas; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tab.Colunas; c++)
                {
                    if (posicoesPossiveis[l, c])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOrigical;
                    }

                    ImprimirPeca(tab.Peca(l, c));
                    Console.BackgroundColor = fundoOrigical;
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOrigical;
        }

        public static void ImprimirPeca(Peca peca)
        {

            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1]+"");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}
