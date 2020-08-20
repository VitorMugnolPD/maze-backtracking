namespace back_track
{
    public class Position
    {
        private int x, y, direction;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Walk(int direction)
        {
            this.direction = direction;

            switch (direction)
            {
                // Cima
                case 0:
                    Walk(0, -1);
                    break;

                // Cima direita
                case 1:
                    Walk(1, -1);
                    break;

                // direita
                case 2:
                    Walk(1, 0);
                    break;
                    
                // direita baixo
                case 3:
                    Walk(1, 1);
                    break;

                // baixo
                case 4:
                    Walk(0, 1);
                    break;

                // baixo esquerda
                case 5:
                    Walk(-1, 1);
                    break;

                // esquerda
                case 6:
                    Walk(-1, 0);
                    break;

                // erqueda cima
                case 7:
                    Walk(-1, -1);
                    break;



            }
        }

        private void Walk(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public int[] getPosition()
        { 
            int[] a = new int[] {x, y};
            return a;
        }

        public string toString()
        {
            return $"X: {this.x} | Y: {this.y}";
        }
    }
}