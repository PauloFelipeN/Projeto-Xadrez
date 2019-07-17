using System;
using tabuleiro;
using Execeptions;
using System.Collections.Generic;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get;private set; }
        public int Turno { get;private set; }
        public Cor JogadorAtual { get;private set; }
        public bool Terminada { get;private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get;private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento (Posicao origem,Posicao destino){
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if(pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;

        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if(pecaCapturada != null) {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }
        public void RealizaJogada(Posicao origem,Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExeception("Voce nao pode se colocar me xeque!");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual))) {
                Xeque = true;
            } else
            {
                Xeque = false;
            }

            Turno++;
            MudaJogador();
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null) {
                throw new TabuleiroExeception("Não existe peça na posicao de origem escolhida!");
            }
            if(JogadorAtual != tab.peca(pos).Cor) {
                throw new TabuleiroExeception("A peça de origem escolhida não é sua! ");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroExeception("não há movimentos possiveis para peça de origem escolhida!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem,Posicao destino)
        {
            if (!tab.peca(origem).PodeMoverPara(destino)) {
                throw new TabuleiroExeception("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            } 
            else {
                JogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) {
                if(x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } 
            else {
                return Cor.Branca;
            }
        }
        private Peca Rei (Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor)){
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if(R == null) {
                throw new TabuleiroExeception($"Nao tem rei da cor {cor} no tabuleiro");
            }
            foreach(Peca x in PecasEmJogo(Adversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]) {
                    return true;
                }
            }
            return false;
        }
        private void ColocarNovaPeca(char coluna,int linha,Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
