﻿using System;
// using System.Windows.Forms;
using System.Threading;

namespace back_track
{
    class PilhaLista<Dado> : ListaSimples<Dado>, IStack<Dado>
                             where Dado : IComparable<Dado>
    {
        public Dado Desempilhar()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("pilha vazia!");

            Dado valor = base.Primeiro.Info;

            NoLista<Dado> pri = base.Primeiro;
            NoLista<Dado> ant = null;
            base.RemoverNo(ref pri, ref ant);
            return valor;
        }

        public void Empilhar(Dado elemento)
        {
            base.InserirAntesDoInicio
                  (
                    new NoLista<Dado>(elemento, null)
                  );
        }

        new public bool EstaVazia
        {
            get => base.EstaVazia;
        }

        public Dado OTopo()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("pilha vazia!");

            return base.Primeiro.Info;
        }

        public int Tamanho { get => base.QuantosNos; }

        public int CompareTo(PilhaLista<Dado> other)
        {
            return 0;
        }


        public Object Clone()
        {
            PilhaLista<Dado> clone = new PilhaLista<Dado>();

            NoLista<Dado> dado = new NoLista<Dado>(Primeiro.Info, Primeiro.Prox);
            while (dado.Prox != null)
            {
                clone.Empilhar(dado.Info);
                dado = dado.Prox;

            }
            return clone;

        }

        // public void Exibir(DataGridView dgv)
        // {
        //     for (int j = 0; j < 20; j++)
        //         dgv[j, 0].Value = "";

        //     var auxiliar = new PilhaLista<Dado>();
        //     int i = 0;
        //     while (!this.EstaVazia)
        //     {
        //         dgv[i++, 0].Value = this.OTopo();
        //         Thread.Sleep(300);
        //         Application.DoEvents();
        //         auxiliar.Empilhar(this.Desempilhar());
        //     }

        //     while (!auxiliar.EstaVazia)
        //         this.Empilhar(auxiliar.Desempilhar());
        // }
    }
}
