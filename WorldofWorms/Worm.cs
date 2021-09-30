using Generator;

namespace WorldOfWorms
{
    class Worm
    {
        private readonly int id;
        private readonly string name = "Worm";
        public Worm()
        {
            id = IdGenerator.GetId();
            name = name + id;
        }
        public Worm(string name)
        {
            this.name = name;
            id = IdGenerator.GetId();
        }
        public string Name => name;
        public int Id => id;

        public Route getBehavior(int counter = 0, int size = 10)
        {
            Route[] behavior = new Route[size];
            for (int i = 0; i < size; i++)
            {
                if (i < size / 4)
                    behavior[i] = Route.Forward;
                else if (i >= (size / 4) && i < 2 * (size / 4))
                    behavior[i] = Route.Right;
                else if (i >= 2 * (size / 4) && i < 3 * (size / 4))
                    behavior[i] = Route.Back;
                else
                    behavior[i] = Route.Left;
            }
            return behavior[counter];
        }
    }
}
