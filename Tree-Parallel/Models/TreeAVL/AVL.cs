using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree_Parallel.Models.TreeAVL
{
    public class AVL
    {
        public int leap;

        public Leaf Root;

        public Leaf[] Vetor;

        public string logTela;

        public string logConsole;

        public int Thread = 0;

        public void Initialize()
        {
            Root = null;
        }

        public async Task<bool> DoSearchInTree(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin(root, value, 1);
            }

            return false;
        }

        public async Task<bool> DoSearchInTree2(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin2(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin2(root, value, 1);
            }

            return false;
        }

        public async Task<bool> DoSearchInTree3(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin3(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin3(root, value, 1);
            }

            return false;
        }

        public async Task<bool> DoSearchInTree4(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin4(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin4(root, value, 1);
            }

            return false;
        }

        public async Task<bool> DoSearchInTree5(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin5(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin5(root, value, 1);
            }

            return false;
        }

        public async Task<bool> DoSearchInTree6(Leaf root, Int64 value)
        {
            if (root == null)
                return false;

            if (value == root.Value)
                return true;

            if (value < root.Value)
            {
                return await DoMultiThreadin6(root, value, 0);
            }
            else if (value > root.Value)
            {
                return await DoMultiThreadin6(root, value, 1);
            }

            return false;
        }

        private async Task<Boolean> DoMultiThreadin(Leaf raiz, Int64 informacao, int direcao)
        {
            var tasks = new List<Task<bool>>
            {
                Task.Run(() => Search(raiz, informacao, 0, 1))
            };

            Leaf raizTemporaria = FindWay(raiz, informacao, direcao);

            if (raizTemporaria != null)
                tasks.Add(Task.Run(() => Search(raizTemporaria, informacao, 0, 2)));

            bool[] resultados = await Task.WhenAll(tasks);

            return ContemResultado(resultados);
        }

        private async Task<Boolean> DoMultiThreadin2(Leaf root, Int64 value, int diretion)
        {
            return await Task.Run(() => Search(root, value, 0, 1));
        }

        private async Task<Boolean> DoMultiThreadin3(Leaf root, Int64 value, int diretion)
        {
            Leaf raizTemporaria = FindWay(root, value, diretion);

            if (raizTemporaria == null)
                return false;

            return await Task.Run(() => Search(raizTemporaria, value, 0, 2));
        }

        private async Task<Boolean> DoMultiThreadin4(Leaf root, Int64 value, int diretion)
        {
            Leaf raizTemporaria = FindWayOtimized(root, value, diretion);

            if (raizTemporaria == null)
                return false;

            return await Task.Run(() => Search(raizTemporaria, value, 0, 2));
        }

        private async Task<Boolean> DoMultiThreadin5(Leaf root, Int64 value, int diretion)
        {
            var tasks = new List<Task<bool>>
            {
                Task.Run(() => Search(root, value, 0, 1))
            };

            AddSearchTask(tasks, Vetor, 4, value, 2);
            AddSearchTask(tasks, Vetor, 5, value, 3);
            AddSearchTask(tasks, Vetor, 6, value, 4);

            bool[] resultados = await Task.WhenAll(tasks);

            return ContemResultado(resultados);
        }

        private async Task<Boolean> DoMultiThreadin6(Leaf root, Int64 value, int diretion)
        {
            Leaf raizTemporaria = root;

            if (Vetor == null || Vetor.Length < 7 || Vetor[4] == null || Vetor[5] == null || Vetor[6] == null)
                return await Task.Run(() => Search(root, value, 0, 2));

            if (value < Vetor[4].Value)
                raizTemporaria = root;

            else if (value > Vetor[4].Value && value < Vetor[5].Value)
                raizTemporaria = Vetor[4];

            else if (value > Vetor[5].Value && value < Vetor[6].Value)
                raizTemporaria = Vetor[5];

            else if (value > Vetor[6].Value)
                 raizTemporaria = Vetor[6];

            if (raizTemporaria == null)
                return false;

            return await Task.Run(() => Search(raizTemporaria, value, 0, 2));
        }

        public Leaf FindWay(Leaf root, Int64 value, int diretion)
        {
            int height = root.Height;

            Leaf raizAuxiliar = root;

            for (int i = 0; i < root.Height; i++)
            {
                height /= 2;

                if (diretion == 0)
                {
                    for (int j = 0; j < height; j++)
                        raizAuxiliar = raizAuxiliar.Left;

                    if (value < raizAuxiliar.Value)
                    {
                        Leaf raizAuxiliar2 = raizAuxiliar;

                        height /= 2;

                        for (int j = 0; j < height; j++)
                            raizAuxiliar2 = raizAuxiliar2.Left;

                        if (value < raizAuxiliar2.Value)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (value < raizAuxiliar2.Value) raizAuxiliar2 = raizAuxiliar2.Left;

                                else return raizAuxiliar2;
                            }
                        }
                        else
                            return raizAuxiliar;
                    }
                    else
                    {
                        height /= 2;

                        Leaf raizAuxiliar2 = root;
                        raizAuxiliar = raizAuxiliar2;

                        for (int j = 0; j < height; j++)
                            raizAuxiliar = raizAuxiliar.Left;

                        if (value < raizAuxiliar.Value)
                        {
                            for (int j = 0; j < height + 1; j++)
                            {
                                if (value < raizAuxiliar2.Value)
                                    raizAuxiliar2 = raizAuxiliar2.Left;
                                else
                                    return raizAuxiliar;
                            }

                            return raizAuxiliar2;
                        }
                    }
                }
                else if (diretion == 1)
                {
                    for (int j = 0; j < height; j++)
                        raizAuxiliar = raizAuxiliar.Right;

                    if (value > raizAuxiliar.Value)
                    {
                        Leaf raizAuxiliar2 = raizAuxiliar;

                        height /= 2;

                        for (int j = 0; j < height; j++)
                            raizAuxiliar2 = raizAuxiliar2.Right;

                        if (value > raizAuxiliar2.Value)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (value > raizAuxiliar2.Value) raizAuxiliar2 = raizAuxiliar2.Right;

                                else return raizAuxiliar;
                            }

                            return raizAuxiliar2;
                        }
                        else  return raizAuxiliar;
                    }
                    else
                    {
                        height /= 2;

                        Leaf raizAuxiliar2 = root;
                        raizAuxiliar = raizAuxiliar2;

                        for (int j = 0; j < height; j++)
                            raizAuxiliar = raizAuxiliar.Right;

                        if (value > raizAuxiliar.Value)
                        {
                            for (int j = 0; j < height + 1; j++)
                            {
                                if (value > raizAuxiliar2.Value) raizAuxiliar2 = raizAuxiliar2.Right;

                                else return raizAuxiliar;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public Leaf FindWayOtimized(Leaf root, Int64 value, int diretion)
        {
            int height = root.Height;

            Leaf raizAuxiliar = root;

            for (int i = 0; i < root.Height; i++)
            {
                height = root.Height / 2;

                if (diretion == 0) // Quando é na esquerda
                {
                    Int64 x = root.Value;

                    raizAuxiliar = Vetor[2];

                    if (value < raizAuxiliar.Value)
                    {
                        height = root.Height / 4;

                        Leaf raizAuxiliar2 = raizAuxiliar;

                        raizAuxiliar2 = Vetor[3];

                        if (value < raizAuxiliar2.Value)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (value < raizAuxiliar2.Value)
                                    raizAuxiliar2 = raizAuxiliar2.Left;
                                else
                                    return raizAuxiliar2;
                            }
                        }
                        else
                            return raizAuxiliar;

                    }
                    else
                    {
                        height = root.Height / 4;

                        Leaf raizAuxiliar2 = root;
                        raizAuxiliar = raizAuxiliar2;

                        raizAuxiliar = Vetor[1];

                        if (value < raizAuxiliar.Value)
                        {
                            for (int j = 0; j < height + 1; j++)
                            {
                                if (value < raizAuxiliar2.Value)
                                    raizAuxiliar2 = raizAuxiliar2.Left;
                                else
                                    return raizAuxiliar;
                            }

                            return raizAuxiliar2;
                        }
                    }
                }
                else if (diretion == 1) // Quando é na direita
                {
                    height = root.Height / 2;

                    raizAuxiliar = Vetor[5];

                    if (value > raizAuxiliar.Value)
                    {
                        height = root.Height / 4;

                        Leaf raizAuxiliar2 = raizAuxiliar;

                        raizAuxiliar2 = Vetor[6];

                        if (value > raizAuxiliar2.Value)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (value > raizAuxiliar2.Value)
                                    raizAuxiliar2 = raizAuxiliar2.Right;
                                else
                                    return raizAuxiliar;
                            }

                            return raizAuxiliar2;
                        }
                        else
                            return raizAuxiliar;

                    }
                    else
                    {
                        height = root.Height / 4;

                        Leaf raizAuxiliar2 = root;
                        raizAuxiliar = Vetor[4];

                        raizAuxiliar = raizAuxiliar.Right;

                        if (value > raizAuxiliar.Value)
                        {
                            for (int j = 0; j < height + 1; j++)
                            {
                                if (value > raizAuxiliar2.Value)
                                    raizAuxiliar2 = raizAuxiliar2.Right;
                                else
                                    return raizAuxiliar;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public Boolean Search(Leaf leaf, Int64 value, int leap, int thread)
        {
            if (leaf == null)
                return false;

            if (leaf.Value == value)
            {
                leap += 1;
                Thread = thread;
                //MessageBox.Show("Elemento encontrado pela Thread: " + thread.ToString() + " pulos: " + leap.ToString());
                return true;
            }

            else if (leaf.Value > value)
            {
                if (leaf.Left != null) return Search(leaf.Left, value, leap + 1, thread);

                else return false;
            }
            else
            {
                if (leaf.Right != null) return Search(leaf.Right, value, leap + 1, thread);

                else return false;
            }
        }

        private void AddSearchTask(List<Task<bool>> tasks, Leaf[] vetor, int indice, Int64 value, int thread)
        {
            if (vetor == null || indice >= vetor.Length || vetor[indice] == null)
                return;

            Leaf raiz = vetor[indice];
            tasks.Add(Task.Run(() => Search(raiz, value, 0, thread)));
        }

        private static bool ContemResultado(bool[] resultados)
        {
            foreach (bool resultado in resultados)
            {
                if (resultado)
                    return true;
            }

            return false;
        }
        
        private Leaf AddToLeaf(Int64 value)
        {
            Leaf leaf = new Leaf();

            leaf.Left = null;
            leaf.Right = null;
            leaf.Value = value;

            return leaf;
        }

        private static int GetHeight(Leaf leaf)
        {
            return leaf == null ? -1 : leaf.Height;
        }
        
        private static int GetHighest(int leftLeaf, int rightLeaf)
        {
            return leftLeaf > rightLeaf ? leftLeaf : rightLeaf;
        }

        private int GetFactorBalance(Leaf leaf)
        {
            return GetHeight(leaf.Left) - GetHeight(leaf.Right);
        }

        public async Task<string> InsertIntoTree(Int64 value, string log)
        {
            var horaInicial = DateTime.Now;

            Task threadPrincipal = new Task(() => Root = insert(value, Root, log));

            threadPrincipal.Start();

            await Task.WhenAll(threadPrincipal);

            var horaFinal = DateTime.Now;

            return horaFinal.Subtract(horaInicial).ToString();
        }

        private Leaf insert(Int64 value, Leaf leaf, string log)
        {
            if (leaf == null) leaf = AddToLeaf(value);

            else if (value < leaf.Value) leaf.Left = insert(value, leaf.Left, log);
            else if (value > leaf.Value) leaf.Right = insert(value, leaf.Right, log);

            leaf = Balance(leaf);

            return leaf;
        }

        private Leaf Balance(Leaf leaf)
        {
            if (GetFactorBalance(leaf) == 2)
            {
                if (GetFactorBalance(leaf.Left) > 0) leaf = OnlyRightRotation(leaf);
                else leaf = DoubleRightRotation(leaf);
            }
            else if (GetFactorBalance(leaf) == -2)
            {
                if (GetFactorBalance(leaf.Right) < 0) leaf = OnlyLeftRotation(leaf);
                else leaf = DoubleLeftRotation(leaf);
            }
            leaf.Height = GetHighest(GetHeight(leaf.Left), GetHeight(leaf.Right)) + 1;
            return leaf;
        }
        
        private static Leaf OnlyRightRotation(Leaf leaf)
        {
            Leaf leafTemp = leaf.Left;
            leaf.Left = leafTemp.Right;
            leafTemp.Right = leaf;
            leaf.Height = GetHighest(GetHeight(leaf.Left), GetHeight(leaf.Right)) + 1;
            leafTemp.Height = GetHighest(GetHeight(leafTemp.Left), leaf.Height) + 1;
            return leafTemp;
        }

        private static Leaf OnlyLeftRotation(Leaf leaf)
        {
            Leaf leafTemp = leaf.Right;
            leaf.Right = leafTemp.Left;
            leafTemp.Left = leaf;
            leaf.Height = GetHighest(GetHeight(leaf.Left), GetHeight(leaf.Right)) + 1;
            leafTemp.Height = GetHighest(GetHeight(leafTemp.Right), leaf.Height) + 1;
            return leafTemp;
        }
        
        private static Leaf DoubleRightRotation(Leaf leaf)
        {
            leaf.Left = OnlyLeftRotation(leaf.Left);
            return OnlyRightRotation(leaf);
        }

        private static Leaf DoubleLeftRotation(Leaf leaf)
        {
            leaf.Right = OnlyRightRotation(leaf.Right);
            return OnlyLeftRotation(leaf);
        }

        public void PrintOutTree(Leaf root, string log)
        {
            if (root != null)
            {
                PrintOutTree(root.Left, log);

                log = "Informação: " + root.Value.ToString();
                logConsole = logConsole + Environment.NewLine + log;

                PrintOutTree(root.Right, log);
            }
        }
        
        public void AtualizaVetor()
        {
            Vetor = new Leaf[7];

            Leaf leaf = new Leaf();
            leaf = Root;
            int pulos = 0;

            int raizHeight = Root.Height;
            int pos25 = raizHeight / 4;
            int pos50 = raizHeight / 2;
            int pos75 = pos25 + pos50;

            while (leaf != null)
            {

                if (pulos == 0)
                {
                    Vetor[0] = new Leaf();
                    Vetor[0] = leaf;
                }

                if (pulos == pos25)
                {
                    Vetor[1] = new Leaf();
                    Vetor[1] = leaf;
                }

                if (pulos == pos50)
                {
                    Vetor[2] = new Leaf();
                    Vetor[2] = leaf;
                }

                if (pulos == pos75)
                {
                    Vetor[3] = new Leaf();
                    Vetor[3] = leaf;
                }

                leaf = leaf.Left;
                pulos += 1;
            }

            leaf = Root;
            pulos = 0;

            while (leaf != null)
            {
                if (pulos == pos25)
                {
                    Vetor[4] = new Leaf();
                    Vetor[4] = leaf;
                }

                if (pulos == pos50)
                {
                    Vetor[5] = new Leaf();
                    Vetor[5] = leaf;
                }

                if (pulos == pos75)
                {
                    Vetor[6] = new Leaf();
                    Vetor[6] = leaf;
                }

                leaf = leaf.Right;
                pulos += 1;
            }
        }

        public void ImprimeVetor()
        {
            string texto = "";

            for (int i = 0; i < 7; i++)
            {
                Leaf leaf = new Leaf();
                leaf = Vetor[i];
                texto = texto + " " + leaf.Value.ToString();
            }

            MessageBox.Show(texto);
        }


        // Funções antigas .. ignorá-las
        private Boolean MultiThreadin_Antiga(Leaf root, Int64 informacao, int direcao)
        {
            //int height = Root.Altura;
            //int numThread = 0;

            //Task task0 = new Task(() =>
            //{
            //    Int64 value = raiz.Informacao;

            //    Search(raiz, informacao, 0, 0);
            //});

            //task0.Start();

            //Node folhaAux = raiz;

            //while (height >= numNodesThreads)
            //{
            //    numThread += 1;

            //    folhaAux = AdvanceNode1(folhaAux, 8, direcao);

            //    Task task = new Task(() => Search(raiz, informacao, 0, numThread));

            //    task.Start();

            //    height -= numNodesThreads;
            //}

            //task0.Wait();
            return true;
        }

        public Leaf AdvanceNode_Antiga(Leaf folhaAux, int contador, int direcao)
        {
            for (int i = 0; i < contador; i++)
            {
                if (direcao == 0)
                {
                    folhaAux = folhaAux.Left;
                }
                else
                {
                    folhaAux = folhaAux.Right;
                }
            }

            return folhaAux;
        }
    }
}
