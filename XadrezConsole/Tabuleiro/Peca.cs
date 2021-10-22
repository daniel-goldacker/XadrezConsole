namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimento { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtdMovimento = 0;
        }

        public void IncrementarQtdMovimento()
        {
            QtdMovimento += 1;
        }

        public void DecrementarQtdMovimento()
        {
            QtdMovimento -= 1;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPosiveis();
            for (int l = 0; l < Tab.Linhas; l++)
            {
                for (int c = 0; c < Tab.Colunas; c++)
                {
                    if (mat[l, c])
                    {
                        return true;
                     }
                }
            }

            return false;
        }

        public bool MovimentoPossivel(Posicao pos)
        {
            return MovimentosPosiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPosiveis();

    }
}
