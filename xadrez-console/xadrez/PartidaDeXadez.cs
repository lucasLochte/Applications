using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public  Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(Tab.RetornaPeca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (!Tab.RetornaPeca(pos).ExisteMovimentoPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
            if (Tab.RetornaPeca(pos).Cor != JogadorAtual)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua");
            }
        }
        public void ValidarPosicaodeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.RetornaPeca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> PecasCapturdas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Pecas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturdas(cor));
            return aux;
        }
        public void ColocarNovaPeca(Peca peca, char coluna, int linha)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'c',1);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'c', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'd', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'e', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'e', 1);
            ColocarNovaPeca(new Rei(Tab, Cor.Branca), 'd', 1);

            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'c', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'c', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'd', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'e', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'e', 8);
            ColocarNovaPeca(new Rei(Tab, Cor.Preta), 'd', 8);

        }
    }
}
