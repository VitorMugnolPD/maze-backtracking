using System.IO;
using static System.Console;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Security;

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
        private int last_direction = 0;



        public Maze(string arquivo)
        {
            StreamReader sr = new StreamReader(arquivo);
            colunas = int.Parse(sr.ReadLine());  // pegar o número de colunas fornecido pelo arquivo
            linhas = int.Parse(sr.ReadLine());  // pegar o número de linhas fornecido pelo arquivo

            matriz = new char[linhas, colunas];  // matriz de char com o tamanho doi labirinto

            for (int i = 0; i < linhas; i++)  // for para atribuir os valores do labirito do arquivo à matriz
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {
                    matriz[i, j] = linhaArquivo[j];
                }
            }
        }

        public void Update_DataGridView_Position(DataGridView dgv)  // função para atualizar o DataGridView da esquerda, para ele seguir o caminho
        {
            int[] CordenadasDaPosicao = Posicaocao_atual.getPosition();
            dgv.CurrentCell = dgv[CordenadasDaPosicao[0], CordenadasDaPosicao[1]];
            dgv.CurrentCell.Selected = true;
            dgv.BeginEdit(true);
        }

        public void Back_Way()  // função para voltar uma posição
        {
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = ' ';
            last_direction = Posicaocao_atual.getDirection() + 1;  // atribui o valor para a última direção
            Posicaocao_atual = caminho.Desempilhar();  // atrui como posição o último valor da pilha
        }

        public void Progress(int direction)  // função para avançar um posição
        {
            caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posição atual à pilha
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = '#';  // atruibui '#' à posição na matriz
            Posicaocao_atual.Walk(direction);  // chama a função para andar de acordo com a direção do parâmetro
            last_direction = 0;
        }

        public void Find(DataGridView dgvLabirinto, DataGridView dgvCaminho)
        {
            Posicaocao_atual.setDirection(-1);  // atribui o valor a direção da posição atual
            caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posição atual à pilha
            do
            {
                Step(dgvLabirinto);  // chama a função para andar no labirinto
                dgvLabirinto.Refresh();  // atualizar o DataGridView da esquerda
                Thread.Sleep(1);
            }
            while (!caminho.EstaVazia);  // executa enquanto a pilha não estiver vazia

            MessageBox.Show("Caminhos encontrados: " + caminhosEncontrados);  // MessageBox com o número de caminhos encontrados
            mostrarCaminhos(dgvCaminho);  // chama a função para mostrar caminhos
        }

        public void Step(DataGridView dgvLabirinto)
        {
            CordenadasDaPosicao = Posicaocao_atual.getPosition();  // coordenadas da posição atual

            for (int i = last_direction; i < 9; i++)
            {
                // Voltar no caminho
                if (i > 7)  // caso a posição ultrapassar 7
                {
                    Back_Way();  // chama a função para voltar
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.White;  // muda a cor de fundo da célula atual
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda
                    continue;
                }

                int[] CordenadasDaPosicaocao_teste = new int[] { CordenadasDaPosicao[0] + directions[i][0], CordenadasDaPosicao[1] + directions[i][1] };  // adiciona o valor obtido na direção às coordenadas

                // verifica se a CordenadasDaPosicaoção é caminho
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == ' ')  // caso a coordenada obtida seja ' '
                {
                    // Anda
                    Progress(i);  // chama a função para avançar

                    // Mostra no dgv
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;  // muda a cor de fundo da célula atual
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda

                    break;
                }

                // verifica se a CordenadasDaPosicaoção é o final
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == 'S')  // caso a coordenada obtida seja 'S'
                {
                    caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posição atual à pilha
                    last_direction = i + 1;

                    // Mostra no dgv
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;  // muda a cor de fundo da célula atual

                    caminhosEncontrados++;  // soma 1 ao número de caminhos encontrados
                    caminhos.Add((PilhaLista<Position>)caminho.Clone());  // adiciona o caminho encontrado à lista de caminhos
                    //MessageBox.Show("Achou.");
                    caminho.Desempilhar();  // retira o último valor da pilha
                    break;
                }
            }
        }

        public char[,] getMaze()
        {
            return this.matriz;  // retorna a matriz do labirinto
        }

        public int getColumns()
        {
            return this.colunas;  // retorna o número de colunas
        }

        public int getRows()
        {
            return this.linhas;  // retorna o número de linhas
        }

        public void toString()  // escreve o labirinto
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

        public void mostrarCaminhos(DataGridView dgvCaminho)  // função para mostrar os caminhos no DataGridView da direita
        {
            //dgvCaminho.RowCount = 10;
            dgvCaminho.ColumnCount = 300;
            string[] caminhosEncontrados;
            foreach (PilhaLista<Position> caminho in caminhos)  // para cada pilha de caminho na lista de caminhos
            {
                PilhaLista<Position> caminhoClone = (PilhaLista<Position>) caminho.Clone();  // cria um clone do caminho
                caminhosEncontrados = new string[caminhoClone.Tamanho];
                int cont = 0;
                while (!caminhoClone.EstaVazia)  // enquanto a pilha do caminho não estiver vazia
                {
                    Position p = caminhoClone.Desempilhar();  // pega o último caminho da pilha
                    caminhosEncontrados[cont] = "Foi para (" + p.getPosition()[0] + ", " + p.getPosition()[1] + ")";
                    cont++;
                }
                dgvCaminho.Rows.Add(caminhosEncontrados);  // adiciona o vetor de string com caminho para o DatGridView da direita
            }
            dgvCaminho.Refresh();  // atualiza o DatGridView da direita
            //MessageBox.Show("Caminhos listados.");
        }

        public void destacarCaminho (int caminhoIndice, DataGridView dgvLabirinto)  // função para destacar um caminho específico
        {
            PilhaLista<Position> caminho = (PilhaLista<Position>) caminhos[caminhoIndice].Clone();  // clona o caminho específico
            while(!caminho.EstaVazia)  // enquanto a pilha não estiver vazia
            {
                Position p = caminho.Desempilhar().Clone();  // pega a última posição da pilha
                int[] coordenadas = p.getPosition();  // vetor de string com as coordenadas da posição
                dgvLabirinto[coordenadas[0], coordenadas[1]].Style.BackColor = Color.Green;  // muda a cor de fundo da célula
            }
        }
    }
}