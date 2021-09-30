using System;

namespace WorldOfWorms
{
    class WormCondition
    {
        private const int maxStartCoord = 100;
        static Random rng = new Random();

        public int health = 100;
        public int Id { get; }
        public int X { get; set; }
        public int Y { get; set; }

        public WormCondition(int id)
        {
            Id = id;
            X = rng.Next(maxStartCoord);
            Y = rng.Next(maxStartCoord);
        }
        public WormCondition(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 0 && value < 100)
                {
                    health = value;
                }
            }
        }
    }
}
