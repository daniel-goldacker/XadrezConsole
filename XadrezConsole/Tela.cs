﻿using System;
using tabuleiro;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {

            for (int l = 0; l < tab.Linhas; l++)
            {
                for (int c = 0; c < tab.Colunas; c++)
                {
                    if (tab.Peca(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.Peca(l, c) + " ");
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
