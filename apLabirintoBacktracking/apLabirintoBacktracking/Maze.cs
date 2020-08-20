using System.IO;
using static System.Console;

namespace back_track
{
    public class Maze
    {
        private int colunas, linhas;
        private char[,] matriz;

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
            Position posicao = new Position(1, 1);
            int[] posi = posicao.getPosition();
            
            for(int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    // Cima
                    case 0:
                        if(matriz[posi[0], posi[1]-1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // Cima direita
                    case 1:
                        if(matriz[posi[0]+1, posi[1]-1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // direita
                    case 2:
                        if(matriz[posi[0]+1, posi[1]] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;
                        
                    // direita baixo
                    case 3:
                        if(matriz[posi[0]+1, posi[1]+1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // baixo
                    case 4:
                        if(matriz[posi[0]+0, posi[1]+1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // baixo esquerda
                    case 5:
                        if(matriz[posi[0]-1, posi[1]+1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // esquerda
                    case 6:
                        if(matriz[posi[0]-1, posi[1]] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;

                    // erqueda cima
                    case 7:
                        if(matriz[posi[0]-1, posi[1]-1] == '#')
                        {
                            WriteLine("aqui");
                            break;
                        }
                        posicao.Walk(i);
                        break;
                }
            }
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
    }
}