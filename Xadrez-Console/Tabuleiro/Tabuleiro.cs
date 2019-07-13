﻿using System;
using tabuleiro;
using Execeptions;


namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private  Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas,colunas];
        }
        public Peca peca (int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca peca (Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return peca(pos) != null;
        }
        public void ColocarPeca(Peca p,Posicao pos)
        {
            if (ExistePeca(pos)){
                throw new TabuleiroExeception("Ja existe uma peca nessa posicao!");
            } 
            else {
                pecas[pos.Linha, pos.Coluna] = p;
                p.Posicao = pos;
            }
            
                
        }
        public bool PosicaoValida(Posicao pos)
        {
           if( pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna <0 || pos.Coluna>= Colunas) {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos)) {
                throw new TabuleiroExeception("Posicao Invalida!");
            }
        }
    }
}
