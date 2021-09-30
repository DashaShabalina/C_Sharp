using System;

namespace WorldOfWorms
{
    class World
    {
        private readonly Worm worm;
        private readonly WormCondition wormCondition;

        public World()
        {
            worm = new Worm();
            wormCondition = new WormCondition(worm.Id, 0, 0);
        }
        public void start()
        {
            int counter = 0;
            Writer writer = new Writer();
            //бесконечный цикл в будущем
            int size = 12;
            //Console.WriteLine($"{wormCondition.X} {wormCondition.Y}");
            writer.Write(worm.Name, wormCondition.X, wormCondition.Y);
            for (int j = 0; j < size; j++)
            {
                Route route = worm.getBehavior(counter, size);
                switch (route)
                {
                    case Route.Forward:
                        wormCondition.Y++;
                        break;
                    case Route.Back:
                        wormCondition.Y--;
                        break;
                    case Route.Right:
                        wormCondition.X++;
                        break;
                    case Route.Left:
                        wormCondition.X--;
                        break;
                }
                writer.Write(worm.Name, wormCondition.X, wormCondition.Y);
                //Console.WriteLine($"{wormCondition.X} {wormCondition.Y}");
                counter++;
            }
            writer.Close();
        }
    }
}