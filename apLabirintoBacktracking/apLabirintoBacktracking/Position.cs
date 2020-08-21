using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back_track
{
    public class Position : IComparable<Position>
    {
        private int x, y, direction;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Walk(int x, int y)
        {
            this.x += x;
            this.y += y;
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