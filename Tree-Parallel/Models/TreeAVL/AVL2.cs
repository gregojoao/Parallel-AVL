using System;

namespace Tree_Parallel.Models.TreeAVL
{
    class AVL2
    {
        public Node root;

        public string logTela = "";

        public string logConsole = "";

        public int pulos = 0;

        public void Initialize()
        {
            root = null;
        }

        Node init_node(Int64 valor)
        {
            Node node_ptr = new Node();

            node_ptr.Esquerda = null;
            node_ptr.Direita = null;
            node_ptr.Informacao = valor;

            return node_ptr;
        }

        int altura(Node node_ptr)
        {

            int altura_esq = 0;
            int altura_dir = 0;

            if (node_ptr.Esquerda != null)
                altura_esq = altura(node_ptr.Esquerda);

            if (node_ptr.Direita != null)
                altura_dir = altura(node_ptr.Direita);

            return max(altura_dir, altura_esq) + 1;
        }
        int max(int x, int y)
        {
            if (x > y)
                return x;
            return y;
        }

        int fator_bal(Node node_ptr)
        {
            int fator = 0;

            if (node_ptr.Esquerda != null)
                fator += altura(node_ptr.Esquerda);

            if (node_ptr.Direita != null)
                fator -= altura(node_ptr.Direita);

            return fator;
        }


        Node rotacionar_esq_esq(Node node_ptr)
        {
            Node temp_ptr = node_ptr;
            Node esq_ptr = temp_ptr.Esquerda;

            temp_ptr.Esquerda = esq_ptr.Direita;
            esq_ptr.Direita = temp_ptr;

            return esq_ptr;
        }

        Node rotationar_esq_dir(Node node_ptr)
        {
            Node temp_ptr = node_ptr;
            Node esq_ptr = temp_ptr.Esquerda;
            Node dir_ptr = esq_ptr.Direita;

            temp_ptr.Esquerda = dir_ptr.Direita;
            esq_ptr.Direita = dir_ptr.Esquerda;
            dir_ptr.Esquerda = esq_ptr;
            dir_ptr.Direita = temp_ptr;

            return dir_ptr;
        }

        Node rotacionar_dir_esq(Node node_ptr)
        {
            Node temp_ptr = node_ptr;
            Node dir_ptr = temp_ptr.Direita;
            Node esq_ptr = dir_ptr.Esquerda;

            temp_ptr.Direita = esq_ptr.Esquerda;
            dir_ptr.Esquerda = esq_ptr.Direita;
            esq_ptr.Direita = dir_ptr;
            esq_ptr.Esquerda = temp_ptr;

            return esq_ptr;
        }

        Node rotacionar_dir_dir(Node node_ptr)
        {
            Node temp_ptr = node_ptr;
            Node dir_ptr = temp_ptr.Direita;

            temp_ptr.Direita = dir_ptr.Esquerda;
            dir_ptr.Esquerda = temp_ptr;

            return dir_ptr;
        }

        Node balancear_node(Node node_ptr)
        {
            Node node_balanceado = null;

            if (node_ptr.Esquerda != null)
                node_ptr.Esquerda = balancear_node(node_ptr.Esquerda);

            if (node_ptr.Direita != null)
                node_ptr.Direita = balancear_node(node_ptr.Direita);

            int fator = fator_bal(node_ptr);

            if (fator >= 2)
            {
                /* pesando pra esquerda */

                if (fator_bal(node_ptr.Esquerda) <= -1)
                    node_balanceado = rotationar_esq_dir(node_ptr);
                else
                    node_balanceado = rotacionar_esq_esq(node_ptr);

            }
            else if (fator <= -2)
            {
                /* pesando pra direita */

                if (fator_bal(node_ptr.Direita) >= 1)
                    node_balanceado = rotacionar_dir_esq(node_ptr);
                else
                    node_balanceado = rotacionar_dir_dir(node_ptr);

            }
            else
            {
                node_balanceado = node_ptr;
            }

            return node_balanceado;
        }


        void balancear_tree(Tree tree_ptr)
        {
            Node nova_raiz = null;

            nova_raiz = balancear_node(tree_ptr.root);

            if (nova_raiz != tree_ptr.root)
            {
                tree_ptr.root = nova_raiz;
            }
        }

        public void inserir(Tree tree_ptr, Int64 valor)
        {
            Node novo_node_ptr = null;
            Node next_ptr = null;
            Node last_ptr = null;

            if (tree_ptr.root == null)
            {
                novo_node_ptr = init_node(valor);
                tree_ptr.root = novo_node_ptr;
            }
            else
            {
                next_ptr = tree_ptr.root;

                while (next_ptr != null)
                {
                    last_ptr = next_ptr;

                    if (valor < next_ptr.Informacao)
                    {
                        next_ptr = next_ptr.Esquerda;

                    }
                    else if (valor > next_ptr.Informacao)
                    {
                        next_ptr = next_ptr.Direita;
                    }
                    else if (valor == next_ptr.Informacao)
                    {
                        return;
                    }
                }

                novo_node_ptr = init_node(valor);

                if (valor < last_ptr.Informacao)
                    last_ptr.Esquerda = novo_node_ptr;

                if (valor > last_ptr.Informacao)
                    last_ptr.Direita = novo_node_ptr;
            }

            balancear_tree(tree_ptr);
        }
    }
}
