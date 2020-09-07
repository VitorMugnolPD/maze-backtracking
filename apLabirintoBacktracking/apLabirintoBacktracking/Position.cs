using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace back_track
{
    public class Position : IComparable<Position>
    {
        private int x, y, direction;
        private Dictionary<int, int[]> directions = new Dictionary<int, int[]>();

        public Position(int x, int y)
        {
            this.x = x;  // atribui uma linha a posição
            this.y = y;  // atribui uma coluna a posição

            directions.Add(0    , new int[2] { 0, -1 });  //
            directions.Add(1    , new int[2] { 1, 0 });   //
            directions.Add(2    , new int[2] { 0, 1 });   //
            directions.Add(3    , new int[2] { -1, 0 });  // adiciona as possíveis direções de serem seguidas à um dicionário de posições
            directions.Add(4    , new int[2] { 1, -1 });  //
            directions.Add(5    , new int[2] { 1, 1 });   //
            directions.Add(6    , new int[2] { -1, 1 });  //
            directions.Add(7    , new int[2] { -1, -1 }); //
        }

        public void Walk(int x, int y)
        {
            this.x += x;  // adiciona um valor à linha da posição atual
            this.y += y;  // adiciona um valor à coluna da posição atual
        }

        public void Walk(int direction)  // andar de acordo à um dos valores do dicionário de posições
        {
            this.setDirection(direction);
            this.x += directions[direction][0];  // adiciona o valor de linha obtido no dicionário
            this.y += directions[direction][1];  // adiciona o valor de coluna obtido no dicionário

        }

        public void setDirection(int direction)
        {
            this.direction = direction;  // atribui um valor à direção
        }

        public int getDirection()
        {
            return this.direction;  // retorna a direção atual
        }

        public int[] getPosition()
        { 
            int[] a = new int[] {x, y};
            return a;  // retorn um vetor de inteiros com a posição
        }

        public Dictionary<int, int[]> getDicionarioDePosicoes()
        {
            return this.directions;  // retorna o dicionário de posições
        }

        public string toString()
        {
            return $"X: {this.x} | Y: {this.y} | Direction: {this.direction}";  // representação em string dos atributos da classe
        }

        public int CompareTo(Position other)
        {
            return 0;
        }

        public Position Clone()
        {
            return Clone(this);  // retornar classe idêntica à esta
        }

        public Position Clone(Position object_to_clone)
        {
            Position clone = new Position(this.x, this.y);  // objeto de Position com os mesmos X e Y desta
            clone.direction = this.direction;  // atribuir a mesma direção desta

            return clone;  // retornar clone construído
        }
    }
}