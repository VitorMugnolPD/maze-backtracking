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
                labirinto = new Maze(dlgAbrir.FileName);
                exibirLabirinto();
            }
        }

        private void exibirLabirinto()
        {
            labirintoMatriz = labirinto.getMaze();
            int linhas = labirinto.getRows();
            int colunas = labirinto.getColumns();
            dgvLabirinto.RowCount = linhas;
            dgvLabirinto.ColumnCount = colunas;
            for (int i = 0; i < linhas; i++)
            {
                for (int n = 0; n < colunas-1; n++)
                {
                    dgvLabirinto[n, i].Value = labirintoMatriz[i, n].ToString();
                    dgvLabirinto[n, i].Style.BackColor = Color.White;
                }
            }
        }

        private void btnEncontrarCaminhos_Click(object sender, EventArgs e)
        {
            labirinto.Find(dgvLabirinto);
        }
    }
}
