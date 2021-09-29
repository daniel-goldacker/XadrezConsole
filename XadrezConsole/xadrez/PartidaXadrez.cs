using System;
using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        private int Turno { get; set; }
        private Cor JogadorAtual { get; set; }
        public bool Terminada { get; private set; }


        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;

            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 1).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('d', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 1).toPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());


            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 8).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('d', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 8).toPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Preto), new PosicaoXadrez('d', 8).toPosicao());
        }
        
    }
}
