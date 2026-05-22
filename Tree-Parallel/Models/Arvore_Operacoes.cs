using Tree_Parallel.Models;
using Tree_Parallel.Views;
using System;
using System.Windows.Forms;

namespace Tree_Parallel.Models
{
    public class Arvore_Operacoes
    {
        public Leaf raiz;

        public string logTela;

        public void Inicia()
        {
            raiz = null;
        }

        public void inserir(Int64 valor, string log)
        {
            inserir(raiz, valor, log);
        }

        public Leaf Busca(Leaf folha, Int64 informacao)
        {
            //if (folha != null && folha.Conteudo > informacao) return folha.NodoEsquerda;

            //if (folha != null && folha.Conteudo < informacao) return folha.NodoDireita;

            //return folha;

            if (folha == null || folha.Value == informacao) return folha;

            if (folha.Value > informacao)
                return Busca(folha.Left, informacao);
            else
                return Busca(folha.Right, informacao);
        }

        public  void inserir(Leaf nodo, Int64 informacao, string log)
        {
            if (raiz == null)
            {
                var node = new Leaf();
                node.Left = null;
                node.Right = null;
                node.Value = informacao;
                
                log = log + Environment.NewLine + "Inserindo " + informacao.ToString() + " na raiz.";
                logTela = log;
                raiz = node;
            }
            else
            {
                if (informacao < nodo.Value)
                {
                    if(nodo.Left != null)
                    {
                        inserir(nodo.Left, informacao, log);

                    }
                    else
                    {
                        var node = new Leaf();
                        node.Left = null;
                        node.Right = null;
                        node.Value = informacao;

                        //MessageBox.Show("Inserindo " + informacao.ToString() + " a esquerda de " + nodo.Conteudo.ToString());
                        log = log + Environment.NewLine + "Inserindo " + informacao.ToString() + " a esquerda de " + nodo.Value.ToString();
                        logTela = log;
                        nodo.Left = node;
                    }
                }

                else if (informacao > nodo.Value)
                {
                    if (nodo.Right != null)
                    {
                        inserir(nodo.Right, informacao, log);
                    }
                    else
                    {
                        var node = new Leaf();
                        node.Left = null;
                        node.Right = null;
                        node.Value = informacao;
                        //MessageBox.Show("Inserindo " + informacao.ToString() + " a direita de " + nodo.Conteudo.ToString());
                        log = log + Environment.NewLine + "Inserindo " + informacao.ToString() + " a direita de " + nodo.Value.ToString();
                        logTela = log;
                        nodo.Right = node;
                    }
                }
            }
            
        }

        public void Imprime(Leaf raiz)
        {
            if (raiz != null)
            {
                Imprime(raiz.Left);
                MessageBox.Show(raiz.Value.ToString());
                Imprime(raiz.Right);
            }
        }

    }
}
