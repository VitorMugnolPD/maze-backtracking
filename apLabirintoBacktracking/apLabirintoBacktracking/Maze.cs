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
            bool voltou_do_S = false;

            for (int i = 0; i < 9; i++)
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
                        break;
                }

                int[] new_position = new int[] {posi[0] + walk_direction[0], posi[1] + walk_direction[1]};


                // verifica se a posição é caminho
                if(matriz[new_position[1], new_position[0]] == ' ')
                {
                    posicao.setDirection(i);
                    caminho.Empilhar(posicao.Clone());
                    matriz[posi[1], posi[0]] = '#';
                    posicao.Walk(walk_direction[0], walk_direction[1]);

                    // Mostra no dgv
                    dgvLabirinto[posi[0], posi[1]].Style.BackColor = Color.Red;
                    dgvLabirinto.CurrentCell = dgvLabirinto[posi[0], posi[1]];
                    dgvLabirinto.CurrentCell.Selected = true;
                    dgvLabirinto.BeginEdit(true);
                    break;
                }

                // verifica se a posição é o final
                if (matriz[new_position[1], new_position[0]] == 'S' && !voltou_do_S)
                {
                    voltou_do_S = true;

                    matriz[posi[1], posi[0]] = '#';
                    
                    // Mostra no dgv
                    dgvLabirinto[posi[0], posi[1]].Style.BackColor = Color.Red;
                    dgvLabirinto.CurrentCell = dgvLabirinto[posi[0], posi[1]];
                    dgvLabirinto.CurrentCell.Selected = true;
                    dgvLabirinto.BeginEdit(true);

                    // entra no S, só para registrar na pilha
                    posicao.setDirection(i);
                    caminho.Empilhar(posicao.Clone());
                    posicao.Walk(walk_direction[0], walk_direction[1]);
                    posicao = caminho.Desempilhar();

                    caminhosEncontrados++;
                    PilhaLista<Position> caminhoClone = (PilhaLista<Position>) caminho.Clone();
                    caminhos.Add(caminhoClone);
                    MessageBox.Show("Achou.");
                }
            }

            dgvLabirinto.Refresh();

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
            //dgvCaminho.RowCount = 10;
            dgvCaminho.ColumnCount = 300;
            string[] caminhosEncontrados;
            foreach (PilhaLista<Position> caminho in caminhos)
            {
                caminhosEncontrados = new string[caminho.Tamanho];
                int cont = 0;
                while (!caminho.EstaVazia)
                {
                    Position p = caminho.Desempilhar();
                    caminhosEncontrados[cont] = "Foi para (" + p.getPosition()[0] + ", " + p.getPosition()[1] + ")";
                    cont++;
                }
                dgvCaminho.Rows.Add(caminhosEncontrados);
            }
            dgvCaminho.Refresh();
            MessageBox.Show("Caminhos listados.");
        }
    }
}