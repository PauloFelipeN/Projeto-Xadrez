using System;
using tabuleiro;
using Execeptions;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get;private set; }
        public int Turno { get;private set; }
        public Cor JogadorAtual { get;private set; }
        public bool Terminada { get;private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento (Posicao origem,Posicao destino){
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);

        }
        public void RealizaJogada(Posicao origem,Posicao destino)
        {
            ExecutaMovimento(origem, destino);
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
        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
