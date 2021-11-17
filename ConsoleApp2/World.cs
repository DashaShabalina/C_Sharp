using System.Collections.Generic;
using pos = WorldOfWorms.Сoordinates;

namespace WorldOfWorms
{
    class World
    {
        private readonly Dictionary<Worm, pos> worldInfo = new Dictionary<Worm, pos>();
        private readonly List<Food> food = new List<Food>();

        public World()
        {
            AddWorm(0, 0);
        }
        public void AddWorm()
        {
            worldInfo.Add(new Worm(), new pos());
        }
        public void AddWorm(int x, int y)
        {
            worldInfo.Add(new Worm(), new pos(x, y));
        }
        public void AddFood(Food f)
        {
            food.Add(f);
        }

        public void RemoveFood(int index)
        {
            food.RemoveAt(index);
        }
        public Dictionary<Worm, pos> WorldInfo => worldInfo;
        public List<Food> Food => food;
    }
}