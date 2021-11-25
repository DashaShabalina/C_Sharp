using System.Collections.Generic;
using pos = WorldOfWorms.Сoordinates;

namespace WorldOfWorms
{
    public class Worm
    {
        private readonly string name;

        public Worm()
        {
            name = NameGenerator.GetName();
        }

        public string Name => name;

        public int Health { get; set; } = 10;

        public Actions GetBehavior(Dictionary<Worm, pos> world, List<Food> food)
        {
            return new Behavior(this).Execute(world, food);
        }
    }
}
