using System.Collections.Generic;
using pos = WorldOfWorms.Сoordinates;

namespace WorldOfWorms
{
    public class World
    {
        private readonly Dictionary<Worm, pos> worldInfo = new Dictionary<Worm, pos>();
        private readonly List<Food> food = new List<Food>();

        public World()
        {
           // AddWorm(0, 0);
        }
        public void AddWorm()
        {
            worldInfo.Add(new Worm(), new pos());
        }
        public Worm AddWorm(int x, int y)
        {
            var worm = new Worm();
            worldInfo.Add(worm, new pos(x, y));
            return worm;
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