using back_track;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apLabirintoBacktracking
{
    public partial class Form1 : Form
    {
        Maze labirinto;
        char[,] labirintoMatriz;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                labirinto = new Maze(dlgAbrir.FileName);  // objeto de labirinto do arquivo específico
                exibirLabirinto();  // exibir o labirinto no DataGridView da esquerda
            }
        }

        private void exibirLabirinto()
        {
            labirintoMatriz = labirinto.getMaze();  // transformar o labirinto em uma matriz de char
            int linhas = labirinto.getRows();
            int colunas = labirinto.getColumns();
            dgvLabirinto.RowCount = linhas;  // atribuir um numero de linhas ao DataGridView da esquerda
            dgvLabirinto.ColumnCount = colunas;  // atribuir um numero de colunas ao DataGridView da esquerda
            for (int i = 0; i < linhas; i++)
            {
                for (int n = 0; n < colunas-1; n++)
                {
                    dgvLabirinto[n, i].Value = labirintoMatriz[i, n].ToString();  // atribuir um valor de texto à célula
                    dgvLabirinto[n, i].Style.BackColor = Color.White;  // mudar a cor de fundo da célula
                }
            }
        }

        private void btnEncontrarCaminhos_Click(object sender, EventArgs e)
        {
            labirinto.Find(dgvLabirinto, dgvCaminhos);  // função para encontrar os caminhos
        }

        private void dgvCaminhos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int caminhoIndice = dgvCaminhos.CurrentCell.RowIndex;  // pegar o índice da linha da célula selecionada, para assim pegar o índice do caminho desejado
            labirinto.destacarCaminho(caminhoIndice, dgvLabirinto);  // função para destacar um caminho específico, de acordo com a célula selecionada
        }
    }
}
