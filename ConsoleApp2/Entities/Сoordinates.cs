using System;

namespace WorldOfWorms
{
    public class Сoordinates
    {
        private static Random rn = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public Сoordinates()
        {
            X = rn.Next();
            Y = rn.Next();
        }
        public Сoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
