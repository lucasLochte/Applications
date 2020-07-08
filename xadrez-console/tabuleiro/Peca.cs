using System;
using System.Collections.Generic;
using System.Text;

namespace xadrez_console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }
        public bool ExisteMovimentoPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i=0; i<Tab.Linhas; i++)
            {
                for(int j=0; j < Tab.Colunas; j++)
                {
                    if(mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool MovimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] MovimentosPossiveis();
        public void IncrementarMovimentos()
        {
            QtdMovimentos++;
        }
        public void DecrementarMovimentos()
        {
            QtdMovimentos--;
        }
    }
}
