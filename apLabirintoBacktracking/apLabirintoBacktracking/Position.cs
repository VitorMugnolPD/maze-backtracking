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
            this.x = x;  // atribui uma linha a posi��o
            this.y = y;  // atribui uma coluna a posi��o

            directions.Add(0    , new int[2] { 0, -1 });  //
            directions.Add(1    , new int[2] { 1, 0 });   //
            directions.Add(2    , new int[2] { 0, 1 });   //
            directions.Add(3    , new int[2] { -1, 0 });  // adiciona as poss�veis dire��es de serem seguidas � um dicion�rio de posi��es
            directions.Add(4    , new int[2] { 1, -1 });  //
            directions.Add(5    , new int[2] { 1, 1 });   //
            directions.Add(6    , new int[2] { -1, 1 });  //
            directions.Add(7    , new int[2] { -1, -1 }); //
        }

        public void Walk(int x, int y)
        {
            this.x += x;  // adiciona um valor � linha da posi��o atual
            this.y += y;  // adiciona um valor � coluna da posi��o atual
        }

        public void Walk(int direction)  // andar de acordo � um dos valores do dicion�rio de posi��es
        {
            this.setDirection(direction);
            this.x += directions[direction][0];  // adiciona o valor de linha obtido no dicion�rio
            this.y += directions[direction][1];  // adiciona o valor de coluna obtido no dicion�rio

        }

        public void setDirection(int direction)
        {
            this.direction = direction;  // atribui um valor � dire��o
        }

        public int getDirection()
        {
            return this.direction;  // retorna a dire��o atual
        }

        public int[] getPosition()
        { 
            int[] a = new int[] {x, y};
            return a;  // retorn um vetor de inteiros com a posi��o
        }

        public Dictionary<int, int[]> getDicionarioDePosicoes()
        {
            return this.directions;  // retorna o dicion�rio de posi��es
        }

        public string toString()
        {
            return $"X: {this.x} | Y: {this.y} | Direction: {this.direction}";  // representa��o em string dos atributos da classe
        }

        public int CompareTo(Position other)
        {
            return 0;
        }

        public Position Clone()
        {
            return Clone(this);  // retornar classe id�ntica � esta
        }

        public Position Clone(Position object_to_clone)
        {
            Position clone = new Position(this.x, this.y);  // objeto de Position com os mesmos X e Y desta
            clone.direction = this.direction;  // atribuir a mesma dire��o desta

            return clone;  // retornar clone constru�do
        }
    }
}