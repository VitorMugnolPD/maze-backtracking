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
            this.x = x;
            this.y = y;

            directions.Add(0    , new int[2] {0, -1 });
            directions.Add(1    , new int[2] { 1, 0 });
            directions.Add(2    , new int[2] { 0, 1 });
            directions.Add(3    , new int[2] { -1, 0 });
            directions.Add(4    , new int[2] { 1, -1 });
            directions.Add(5    , new int[2] { 1, 1 });
            directions.Add(6    , new int[2] { -1, 1 });
            directions.Add(7    , new int[2] { -1, -1 });
        }

        public void Walk(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public void Walk(int direction)
        {
            this.setDirection(direction);
            this.x += directions[direction][0];
            this.y += directions[direction][1];

        }

        public void setDirection(int direction)
        {
            this.direction = direction;
        }

        public int[] getPosition()
        { 
            int[] a = new int[] {x, y};
            return a;
        }

        public Dictionary<int, int[]> getDicionarioDePosicoes()
        {
            return this.directions;
        }

        public string toString()
        {
            return $"X: {this.x} | Y: {this.y} | Direction: {this.direction}";
        }

        public int CompareTo(Position other)
        {
            return 0;
        }

        public Position Clone()
        {
            return Clone(this);
        }

        public Position Clone(Position object_to_clone)
        {
            Position clone = new Position(this.x, this.y);
            clone.direction = this.direction;

            return clone;
        }
    }
}