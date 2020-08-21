using System.IO;
using static System.Console;

namespace back_track
{
    public class Maze
    {
        private int colunas, linhas;
        private char[,] matriz;
        private PilhaLista<Position> caminho = new PilhaLista<Position>();
        Position posicao = new Position(1, 1);

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

        public void Find()
        {
            
            int[] posi = posicao.getPosition();
            // WriteLine(posi[0] + " | " + posi[1]);
            int[] walk_direction = new int[] {0, 0};

            for(int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    // Cima
                    case 0:
                        walk_direction = new int[] {0, -1};
                        break;

                    // Cima direita
                    case 1:
                        walk_direction = new int[] {1, -1};
                        break;

                    // direita
                    case 2:
                        walk_direction = new int[] {1, 0};
                        break;
                        
                    // direita baixo
                    case 3:
                        walk_direction = new int[] {1, 1};
                        break;

                    // baixo
                    case 4:
                        walk_direction = new int[] {0, 1};
                        break;

                    // baixo esquerda
                    case 5:
                        walk_direction = new int[] {-1, 1};
                        break;

                    // esquerda
                    case 6:
                        walk_direction = new int[] {-1, 0};
                        break;

                    // erqueda cima
                    case 7:
                        walk_direction = new int[] {-1, -1};                        
                        break;

                    case 8:
                        matriz[posi[1], posi[0]] = '#';
                        caminho.Desempilhar();
                        posicao = caminho.OTopo();
                        //WriteLine(caminho.OTopo().toString());
                        break;
                }

                int[] new_position = new int[] {posi[0] + walk_direction[0], posi[1] + walk_direction[1]};
                //WriteLine(new_position[0] + " # " + new_position[1] + " | " + matriz[new_position[1], new_position[0]] );
                if(!(matriz[new_position[1], new_position[0]] == '#'))
                {
                    posicao.setDirection(i);
                    caminho.Empilhar(posicao.Clone());
                    matriz[posi[1], posi[0]] = '#';
                    posicao.Walk(walk_direction[0], walk_direction[1]);
                    break;
                }
            }

            posi = posicao.getPosition();
            matriz[posi[1], posi[0]] = '*';
            toString();
            ReadLine();
            Clear();
            Find();
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
    }
}