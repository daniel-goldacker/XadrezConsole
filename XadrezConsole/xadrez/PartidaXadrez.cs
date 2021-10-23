﻿using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }


        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }


        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecasCapturadas)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimento();
            if (pecasCapturadas != null)
            {
                Tab.ColocarPeca(pecasCapturadas, destino);
                capturadas.Remove(pecasCapturadas);
            }

            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExcecao("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TestaXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroExcecao("Não existe peça na posição de origem escolhida!");
            }

            if (Tab.Peca(pos).Cor != JogadorAtual)
            {
                throw new TabuleiroExcecao("A peça de origem escolhida não é a sua!");
            }

            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroExcecao("Não existe movimentos possiveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroExcecao("Posição de destino inválida!");
            }
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var item in capturadas)
            {
                if (item.Cor == cor)
                {
                    aux.Add(item);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var item in pecas)
            {
                if (item.Cor == cor)
                {
                    aux.Add(item);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (var item in PecasEmJogo(cor))
            {
                if (item is Rei)
                {
                    return item;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
            {
                throw new TabuleiroExcecao("Não existe rei da cor: " + cor + " no tabuleiro!");
            }

            foreach (Peca item in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = item.MovimentosPosiveis();
                if(mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestaXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (var item in PecasEmJogo(cor))
            {
                bool[,] mat = item.MovimentosPosiveis();
                for (int l = 0; l < Tab.Linhas; l++)
                {
                    for (int c = 0; c < Tab.Colunas; c++)
                    {
                        if (mat[l, c])
                        {
                            Posicao origem = item. Posicao;
                            Posicao destino = new Posicao(l, c);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void ColocarPecas()
        {





            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));


            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preto));
            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preto));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preto));
        }
    }
}
