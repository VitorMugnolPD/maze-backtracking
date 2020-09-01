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
        private int parteCaminho = 0;
        private int caminhosEncontrados = 0;
        private int colunas, linhas;
        private char[,] matriz;
        private PilhaLista<Position> caminho = new PilhaLista<Position>();
        private List<PilhaLista<Position>> caminhos = new List<PilhaLista<Position>>();
        Position posicao = new Position(1, 1);
        //ListaSimples<PilhaLista<Position>> lista = new ListaSimples<PilhaLista<Position>>();

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

        public void Find(DataGridView dgvLabirinto, DataGridView dgvCaminho)
        {

            int[] posi = posicao.getPosition();
            int[] walk_direction = new int[] {0, 0};

            for(int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    // Cima
                    case 0:
                        walk_direction = new int[] {0, -1};
                        break;

                    // direita
                    case 1:
                        walk_direction = new int[] {1, 0};
                        break;

                    // baixo
                    case 2:
                        walk_direction = new int[] {0, 1};
                        break;

                    // esquerda
                    case 3:
                        walk_direction = new int[] {-1, 0};
                        break;

                    // Cima direita
                    case 4:
                        walk_direction = new int[] {1, -1};
                        break;

                    // direita baixo
                    case 5:
                        walk_direction = new int[] {1, 1};
                        break;

                    // baixo esquerda
                    case 6:
                        walk_direction = new int[] {-1, 1};
                        break;

                    // erqueda cima
                    case 7:
                        walk_direction = new int[] {-1, -1};                        
                        break;

                    case 8:
                        matriz[posi[1], posi[0]] = '#';
                        posicao = caminho.Desempilhar();
                        posi = posicao.getPosition();
                        dgvLabirinto[posi[0], posi[1]].Style.BackColor = Color.White;
                        //dgvCaminho[parteCaminho, caminhosEncontrados].Value = "Voltou para (" + posi[1] + ", " + posi[0] + ")";
                        //dgvCaminho.CurrentCell = dgvCaminho[parteCaminho, caminhosEncontrados];
                        //dgvCaminho.CurrentCell.Selected = true;
                        //dgvCaminho.BeginEdit(true);
                        break;
                }

                int[] new_position = new int[] {posi[0] + walk_direction[0], posi[1] + walk_direction[1]};

                if(!(matriz[new_position[1], new_position[0]] == '#'))
                {
                    posicao.setDirection(i);
                    caminho.Empilhar(posicao.Clone());
                    matriz[posi[1], posi[0]] = '#';
                    posicao.Walk(walk_direction[0], walk_direction[1]);
                    dgvLabirinto[posi[0], posi[1]].Style.BackColor = Color.Red;
                    dgvLabirinto.CurrentCell = dgvLabirinto[posi[0], posi[1]];
                    dgvLabirinto.CurrentCell.Selected = true;
                    dgvLabirinto.BeginEdit(true);
                    //dgvCaminho[parteCaminho, caminhosEncontrados].Value = "Foi para (" + posi[1] + ", " + posi[0] + ")";
                    //dgvCaminho.CurrentCell = dgvCaminho[parteCaminho, caminhosEncontrados];
                    //dgvCaminho.CurrentCell.Selected = true;
                    //dgvCaminho.BeginEdit(true);
                    break;
                }
            }

            posi = posicao.getPosition();
            dgvLabirinto.Refresh();
            if (matriz[posi[1], posi[0]] == 'S')
            {
                //dgvCaminho.Refresh();
                caminhosEncontrados++;
                parteCaminho = 0;
                caminhos.Add(caminho);
                MessageBox.Show("Achou.");
                //return;
            }
            parteCaminho++;

            if(caminho.EstaVazia)
            {
                MessageBox.Show("Não possui mais caminhos.");
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
            dgvCaminho.RowCount = 10;
            dgvCaminho.ColumnCount = 300;
            int c = 0;
            int pa = 0;
            foreach (PilhaLista<Position> caminho in caminhos)
            {
                int t = caminho.Tamanho;
                do
                {
                    if (caminho.EstaVazia)
                        break;
                    Position p = caminho.Desempilhar();
                    dgvCaminho[t - pa, c].Value = "Foi para " + p.getPosition()[0] + ", " + p.getPosition()[1] + ")";
                    pa++;
                }
                while (!caminho.EstaVazia);
                c++;
            }
            dgvCaminho.Refresh();
            MessageBox.Show("Caminhos listados.");
        }
    }
}