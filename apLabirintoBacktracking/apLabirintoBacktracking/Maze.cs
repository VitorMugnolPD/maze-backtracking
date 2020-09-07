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
            colunas = int.Parse(sr.ReadLine());  // pegar o n�mero de colunas fornecido pelo arquivo
            linhas = int.Parse(sr.ReadLine());  // pegar o n�mero de linhas fornecido pelo arquivo

            matriz = new char[linhas, colunas];  // matriz de char com o tamanho doi labirinto

            for (int i = 0; i < linhas; i++)  // for para atribuir os valores do labirito do arquivo � matriz
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {
                    matriz[i, j] = linhaArquivo[j];
                }
            }
        }

        public void Update_DataGridView_Position(DataGridView dgv)  // fun��o para atualizar o DataGridView da esquerda, para ele seguir o caminho
        {
            int[] CordenadasDaPosicao = Posicaocao_atual.getPosition();
            dgv.CurrentCell = dgv[CordenadasDaPosicao[0], CordenadasDaPosicao[1]];
            dgv.CurrentCell.Selected = true;
            dgv.BeginEdit(true);
        }

        public void Back_Way()  // fun��o para voltar uma posi��o
        {
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = ' ';
            last_direction = Posicaocao_atual.getDirection() + 1;  // atribui o valor para a �ltima dire��o
            Posicaocao_atual = caminho.Desempilhar();  // atrui como posi��o o �ltimo valor da pilha
        }

        public void Progress(int direction)  // fun��o para avan�ar um posi��o
        {
            caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posi��o atual � pilha
            matriz[CordenadasDaPosicao[1], CordenadasDaPosicao[0]] = '#';  // atruibui '#' � posi��o na matriz
            Posicaocao_atual.Walk(direction);  // chama a fun��o para andar de acordo com a dire��o do par�metro
            last_direction = 0;
        }

        public void Find(DataGridView dgvLabirinto, DataGridView dgvCaminho)
        {
            Posicaocao_atual.setDirection(-1);  // atribui o valor a dire��o da posi��o atual
            caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posi��o atual � pilha
            do
            {
                Step(dgvLabirinto);  // chama a fun��o para andar no labirinto
                dgvLabirinto.Refresh();  // atualizar o DataGridView da esquerda
                Thread.Sleep(1);
            }
            while (!caminho.EstaVazia);  // executa enquanto a pilha n�o estiver vazia

            MessageBox.Show("Caminhos encontrados: " + caminhosEncontrados);  // MessageBox com o n�mero de caminhos encontrados
            mostrarCaminhos(dgvCaminho);  // chama a fun��o para mostrar caminhos
        }

        public void Step(DataGridView dgvLabirinto)
        {
            CordenadasDaPosicao = Posicaocao_atual.getPosition();  // coordenadas da posi��o atual

            for (int i = last_direction; i < 9; i++)
            {
                // Voltar no caminho
                if (i > 7)  // caso a posi��o ultrapassar 7
                {
                    Back_Way();  // chama a fun��o para voltar
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.White;  // muda a cor de fundo da c�lula atual
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda
                    continue;
                }

                int[] CordenadasDaPosicaocao_teste = new int[] { CordenadasDaPosicao[0] + directions[i][0], CordenadasDaPosicao[1] + directions[i][1] };  // adiciona o valor obtido na dire��o �s coordenadas

                // verifica se a CordenadasDaPosicao��o � caminho
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == ' ')  // caso a coordenada obtida seja ' '
                {
                    // Anda
                    Progress(i);  // chama a fun��o para avan�ar

                    // Mostra no dgv
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;  // muda a cor de fundo da c�lula atual
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda

                    break;
                }

                // verifica se a CordenadasDaPosicao��o � o final
                if (matriz[CordenadasDaPosicaocao_teste[1], CordenadasDaPosicaocao_teste[0]] == 'S')  // caso a coordenada obtida seja 'S'
                {
                    caminho.Empilhar(Posicaocao_atual.Clone());  // adiciona a posi��o atual � pilha
                    last_direction = i + 1;

                    // Mostra no dgv
                    Update_DataGridView_Position(dgvLabirinto);  // atualiza o DataGridView da esquerda
                    dgvLabirinto[CordenadasDaPosicao[0], CordenadasDaPosicao[1]].Style.BackColor = Color.Red;  // muda a cor de fundo da c�lula atual

                    caminhosEncontrados++;  // soma 1 ao n�mero de caminhos encontrados
                    caminhos.Add((PilhaLista<Position>)caminho.Clone());  // adiciona o caminho encontrado � lista de caminhos
                    //MessageBox.Show("Achou.");
                    caminho.Desempilhar();  // retira o �ltimo valor da pilha
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
            return this.colunas;  // retorna o n�mero de colunas
        }

        public int getRows()
        {
            return this.linhas;  // retorna o n�mero de linhas
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

        public void mostrarCaminhos(DataGridView dgvCaminho)  // fun��o para mostrar os caminhos no DataGridView da direita
        {
            //dgvCaminho.RowCount = 10;
            dgvCaminho.ColumnCount = 300;
            string[] caminhosEncontrados;
            foreach (PilhaLista<Position> caminho in caminhos)  // para cada pilha de caminho na lista de caminhos
            {
                PilhaLista<Position> caminhoClone = (PilhaLista<Position>) caminho.Clone();  // cria um clone do caminho
                caminhosEncontrados = new string[caminhoClone.Tamanho];
                int cont = 0;
                while (!caminhoClone.EstaVazia)  // enquanto a pilha do caminho n�o estiver vazia
                {
                    Position p = caminhoClone.Desempilhar();  // pega o �ltimo caminho da pilha
                    caminhosEncontrados[cont] = "Foi para (" + p.getPosition()[0] + ", " + p.getPosition()[1] + ")";
                    cont++;
                }
                dgvCaminho.Rows.Add(caminhosEncontrados);  // adiciona o vetor de string com caminho para o DatGridView da direita
            }
            dgvCaminho.Refresh();  // atualiza o DatGridView da direita
            //MessageBox.Show("Caminhos listados.");
        }

        public void destacarCaminho (int caminhoIndice, DataGridView dgvLabirinto)  // fun��o para destacar um caminho espec�fico
        {
            PilhaLista<Position> caminho = (PilhaLista<Position>) caminhos[caminhoIndice].Clone();  // clona o caminho espec�fico
            while(!caminho.EstaVazia)  // enquanto a pilha n�o estiver vazia
            {
                Position p = caminho.Desempilhar().Clone();  // pega a �ltima posi��o da pilha
                int[] coordenadas = p.getPosition();  // vetor de string com as coordenadas da posi��o
                dgvLabirinto[coordenadas[0], coordenadas[1]].Style.BackColor = Color.Green;  // muda a cor de fundo da c�lula
            }
        }
    }
}