using System.IO;
using static System.Console;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace back_track
{
    public class Maze
    {
        private int caminhosEncontrados = 0;
        private int colunas, linhas;
        private char[,] matriz;

        private PilhaLista<Position> caminho = new PilhaLista<Position>();
        private List<PilhaLista<Position>> caminhos = new List<PilhaLista<Position>>();
        private static Position Posicaocao_atual = new Position(1, 1);
        private int[] CordenadasDaPosicao = Posicaocao_atual.getPosition();
        private Dictionary<int, int[]> directions = Posicaocao_atual.getDicionarioDePosicoes();
        private bool voltou_do_S;



        public Maze(string arquivo)
        {
            StreamReader sr = new StreamReader(arquivo);
            colunas = int.Parse(sr.ReadLine());
            linhas = int.Parse(sr.ReadLine());

            matriz = new char[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {
                    matriz[i, j] = linhaArquivo[j];
                }
            }
        }

        public void Update_DataGridView_Position(DataGridView dgv)
        {
            int[] CordenadasDaPosicao = Posicaocao_atual.getPosition();
            dgv.CurrentCell = dgv[CordenadasDaPosicao[0], CordenadasDaPosicao[1]];
            dgv.CurrentCell.Selected = true;
            dgv.BeginEdit(true);
        }

        public void Back_Way()
        {
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = '#';
            Posicaocao_atual = caminho.Desempilhar();
            voltou_do_S = false;
    }

        public void Progress(int direction)
        {
            caminho.Empilhar(Posicaocao_atual.Clone());
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = '#';
            Posicaocao_atual.Walk(direction);
        }

        public void Find(DataGridView dgvLabirinto, DataGridView dgvCaminho)
        {
            CordenadasDaPosicao = Posicaocao_atual.getPosition();

            for (int i = 0; i < 9; i++)
            {
                // Voltar no caminho
                if (i > 7)
                {
                    Back_Way();
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.White;
                    Update_DataGridView_Position(dgvLabirinto);
                    continue;
                }

                int[] CordenadasDaPosicaocao_teste = new int[] { CordenadasDaPosicao[0] + directions[i][0], CordenadasDaPosicao[1] + directions[i][1] };

                // verifica se a CordenadasDaPosicao��o � caminho
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == ' ')
                {
                    // Anda
                    Progress(i);

                    // Mostra no dgv
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;
                    Update_DataGridView_Position(dgvLabirinto);

                    break;
                }

                // verifica se a CordenadasDaPosicao��o � o final
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == 'S' && !voltou_do_S)
                {
                    voltou_do_S = true;

                    //Progress(i);
                    caminho.Empilhar(Posicaocao_atual.Clone());

                    // Mostra no dgv
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;
                    Update_DataGridView_Position(dgvLabirinto);                  

                    caminhosEncontrados++;
                    caminhos.Add((PilhaLista<Position>)caminho.Clone());
                    MessageBox.Show("Achou.");
                    caminho.Desempilhar();
                    break;
                }
            }

            dgvLabirinto.Refresh();

            if (caminho.EstaVazia)
            {
                MessageBox.Show("N�o possui mais caminhos.");
                mostrarCaminhos(dgvCaminho);
                return;
            }


            Thread.Sleep(30);
            Find(dgvLabirinto, dgvCaminho);
        }

        public char[,] getMaze()
        {
            return this.matriz;
        }

        public int getColumns()
        {
            return this.colunas;
        }

        public int getRows()
        {
            return this.linhas;
        }

        public void toString()
        {
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    Write(matriz[i, j]);
                }
                WriteLine("");
            }
        }

        public void mostrarCaminhos(DataGridView dgvCaminho)
        {
            //dgvCaminho.RowCount = 10;
            dgvCaminho.ColumnCount = 300;
            string[] caminhosEncontrados;
            foreach (PilhaLista<Position> caminho in caminhos)
            {
                PilhaLista<Position> caminhoClone = (PilhaLista<Position>) caminho.Clone();
                caminhosEncontrados = new string[caminhoClone.Tamanho];
                int cont = 0;
                while (!caminhoClone.EstaVazia)
                {
                    Position p = caminhoClone.Desempilhar();
                    caminhosEncontrados[cont] = "Foi para (" + p.getPosition()[0] + ", " + p.getPosition()[1] + ")";
                    cont++;
                }
                dgvCaminho.Rows.Add(caminhosEncontrados);
            }
            dgvCaminho.Refresh();
            MessageBox.Show("Caminhos listados.");
        }

        public void destacarCaminho (int caminhoIndice, DataGridView dgvLabirinto)
        {
            PilhaLista<Position> caminho = (PilhaLista<Position>) caminhos[caminhoIndice].Clone();
            while(!caminho.EstaVazia)
            {
                Position p = caminho.Desempilhar().Clone();
                int[] coordenadas = p.getPosition();
                dgvLabirinto[coordenadas[0], coordenadas[1]].Style.BackColor = Color.Green;
            }
        }
    }
}