using System;
using tabuleiro;

namespace Xadrez
{
    class Peao :Peca
    {
        public Peao (Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "P";
        }
    }
}
