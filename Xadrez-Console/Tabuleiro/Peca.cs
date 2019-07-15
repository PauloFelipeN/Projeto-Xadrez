﻿using System;


namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get;  protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get;protected set; }

        public Peca( Tabuleiro tab,Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }
        public void IncrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }
        public abstract bool[,] MovimentosPossiveis();
        
    }
}
