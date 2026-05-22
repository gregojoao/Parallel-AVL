using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tree_Parallel.Models.TreeAVL;

namespace Tree_Parallel.Views
{
    public partial class Main : Form
    {
        #region MOVIMENTAÇÃO DA TELA

        private bool mouseDown;
        private Point lastLocation;

        private void pnPrincipal_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pnPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pnPrincipal_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        AVL tree;

        int TamanhoInteiro = 100000;

        long[] vetor;

        string appPath;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tree = new AVL();

            tree.Initialize();

            string log = "Árvore Iniciada" + Environment.NewLine;

            txtLog.Text = log;

            appPath = Path.Combine(AppContext.BaseDirectory, "100000 - Random.txt");
        }

        private void MaKeFile_Click(object sender, EventArgs e)
        {
            MakeFileRandom();
            //MaleFileOrder();
        }

        private void MaleFileOrder()
        {
            using StreamWriter Writer = new StreamWriter(appPath);

            Int64 informacao;

            Stopwatch stopWatch = new Stopwatch(); // É o responsável pela marcação do tempo

            stopWatch.Start();

            for (Int64 i = 1; i <= TamanhoInteiro; i++)
            {
                informacao = i;
                Writer.WriteLine(informacao.ToString());
            }

            stopWatch.Stop();

            TimeSpan tempo = stopWatch.Elapsed;

            txtLog.Text = txtLog.Text
                + Environment.NewLine
                + "Arquivo de tamanho: "
                + TamanhoInteiro.ToString()
                + " criado, tempo decorrido: "
                + tempo.TotalMinutes.ToString();
        }

        private void MakeFileRandom()
        {
            using StreamWriter Writer = new StreamWriter(appPath);

            Int64 informacao;

            Random random = new Random();

            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();

            vetor = new long[TamanhoInteiro];

            for (int i = 0; i < TamanhoInteiro; i++)
            {
                informacao = 0;

                informacao = random.Next(0, 1000000);
                informacao = informacao * random.Next(0, 150);

                vetor[i] = informacao;

            }

            foreach (Int64 value in vetor)
            {
                Writer.WriteLine(value.ToString());
            }

            stopWatch.Stop();

            TimeSpan tempo = stopWatch.Elapsed;

            txtLog.Text = txtLog.Text
                + Environment.NewLine
                + "Arquivo de tamanho: "
                + TamanhoInteiro.ToString()
                + " criado, tempo decorrido: "
                + tempo.TotalMinutes.ToString();
        }

        private async void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                tree.Initialize();

                string log =
                    "Resetando Árvore .." + Environment.NewLine + "Árvore Resetada"
                    + Environment.NewLine + Environment.NewLine +
                    "Iniciando Árvore .." + Environment.NewLine + "Árvore Iniciada"
                    + Environment.NewLine + Environment.NewLine;

                txtLog.Text = log;
                txtConsole.Text = "";
            }
            else if (e.KeyCode == Keys.F2)
            {
                AtualizaLabelsArvore();
            }
            else if (e.KeyCode == Keys.F3)
            {
                for (int i = 1; i <= 20; i++)
                {
                    await tree.InsertIntoTree(Convert.ToInt64(i), txtLog.Text);
                    ImprimeSimetrica();

                    txtInfo.Text = "20";
                    btnSearch.PerformClick();

                    lblRoot.Text = "Raíz: " + tree.Root.Value.ToString();
                    lblHeight.Text = "Altura: " + tree.Root.Height.ToString();
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                Boolean buscaElemento = false;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                buscaElemento = await tree.DoSearchInTree6(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    string log = txtLog.Text + Environment.NewLine + "(" + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8) + ") " + "- Busca por elemento: " + (txtInfo.Text).Replace(" ", "");
                    txtLog.Text = log;

                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Método só com a thread do caminho certo: "
                        + tree.Thread.ToString()
                        + " tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString()
                        + Environment.NewLine;
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                Boolean buscaElemento = false;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                buscaElemento = await tree.DoSearchInTree2(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    string log = txtLog.Text + Environment.NewLine + "(" + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8) + ") " + "- Busca por elemento: " + (txtInfo.Text).Replace(" ", "");
                    txtLog.Text = log;

                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Somente a primeira thread: "
                        + tree.Thread.ToString()
                        + " tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
            }
        }

        private async void Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int64 value;

                if (txtInfo.Text == "")
                {
                    Random random = new Random();

                    value = random.Next(0, 100000);

                    await tree.InsertIntoTree(value, txtLog.Text);

                    txtLog.Text = tree.logTela;

                    ImprimeSimetrica();
                }
                else
                {
                    var stopwatch = new Stopwatch();
                    stopwatch = Stopwatch.StartNew();

                    value = Convert.ToInt64(txtInfo.Text);

                    string hora = await tree.InsertIntoTree(value, txtLog.Text);

                    stopwatch.Stop();

                    var tempoDecorrido = stopwatch.Elapsed.TotalSeconds;

                    txtLog.Text = tree.logTela + " tempo decorrido: " + tempoDecorrido.ToString();

                    ImprimeSimetrica();

                    AtualizaLabelsArvore();
                }

                txtInfo.Text = "";

                if (tree.Root.Height >= 8)
                {
                    tree.AtualizaVetor();
                    //tree.ImprimeVetor();
                }
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtInfo.Text != "")
            {
                Boolean buscaElemento = false;

                string log = txtLog.Text + Environment.NewLine + "(" + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8) + ") " + "- Busca por elemento: " + (txtInfo.Text).Replace(" ", "");
                txtLog.Text = log;

                if (tree.Root.Height >= 8)
                {
                    tree.AtualizaVetor();
                    //tree.ImprimeVetor();
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                buscaElemento = await tree.DoSearchInTree(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Última thread: "
                        + tree.Thread.ToString()
                        + " tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                stopwatch.Restart();
                buscaElemento = await tree.DoSearchInTree2(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Somente a primeira thread, tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                stopwatch.Restart();
                buscaElemento = await tree.DoSearchInTree3(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Somente a segunda thread, tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                stopwatch.Restart();
                buscaElemento = await tree.DoSearchInTree4(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Método com auxílio do vetor, última thread: "
                        + tree.Thread.ToString()
                        + " tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                stopwatch.Restart();
                buscaElemento = await tree.DoSearchInTree5(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Método com todas as threads em ordem, última: "
                        + tree.Thread.ToString()
                        + " tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                stopwatch.Restart();
                buscaElemento = await tree.DoSearchInTree6(tree.Root, Convert.ToInt64(txtInfo.Text));
                if (buscaElemento)
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Método só com a thread do caminho certo, tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString()
                        + Environment.NewLine;
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
                else
                {
                    log = log
                        + Environment.NewLine
                        + "("
                        + (DateTime.Now.TimeOfDay.ToString()).Substring(0, 8)
                        + ") "
                        + "- Elemento não encontrado, tempo decorrido: "
                        + stopwatch.Elapsed.TotalSeconds.ToString();
                    txtLog.Text = log;

                    stopwatch.Stop();
                }
            }
        }

        private async void btnLoadTree_Click(object sender, EventArgs e)
        {
            await LoadTreeAsync();
        }

        private async System.Threading.Tasks.Task LoadTreeAsync()
        {
            if (!File.Exists(appPath))
            {
                txtLog.Text = txtLog.Text + Environment.NewLine + "Arquivo não encontrado: " + appPath;
                return;
            }

            string[] lines = File.ReadAllLines(appPath);
            Int64 informacao;

            int i = 0;
            string texto = txtLog.Text;
            
            foreach (string line in lines) // Pega todos os valores do txt e insere na árvore AVL
            {
                if (line != "")
                {
                    i++;
                    if (!Int64.TryParse(line, out informacao))
                        continue;

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    string texto2 = await tree.InsertIntoTree(informacao, txtLog.Text);
                    
                    AtualizaLabelsArvore();
                }                
            }
            
            ImprimeSimetrica();
        }

        private void ImprimeSimetrica()
        {
            txtConsole.Text = "";
            tree.logConsole = "";
            tree.PrintOutTree(tree.Root, txtConsole.Text);
            txtConsole.Text = "Imprindo: " + Environment.NewLine + tree.logConsole;
        }

        private void AtualizaLabelsArvore()
        {
            if (tree.Root == null)
                return;

            lblRoot.Text = "Raíz: " + tree.Root.Value.ToString();
            lblHeight.Text = "Altura: " + tree.Root.Height.ToString();
        }
    }
}
