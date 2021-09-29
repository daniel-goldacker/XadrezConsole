using System;
using tabuleiro;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro teste = new Tabuleiro(8, 8);
            Tela.ImprimirTabuleiro(teste);
            Console.WriteLine("");
        }
    }
}
