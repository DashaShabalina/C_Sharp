using System;

namespace WorldOfWorms
{
    class Food
    {
        static Random rn = new Random();
        public int ShelfLife { get; set; } = 10;
        public int X { get; }
        public int Y { get; }
        public Food()
        {
            X = rn.NextNormal();
            Y = rn.NextNormal();
        }
    }

    static class Generator
    {
        public static int NextNormal(this Random r, double mu = 0, double sigma = 5)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;
            return (int)Math.Round(randNormal);
        }
    }
}
