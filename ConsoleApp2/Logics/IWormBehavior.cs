using System.Collections.Generic;

namespace WorldOfWorms
{
    public interface IWormBehavior
    {
        Actions Execute(Dictionary<Worm, Сoordinates> world, List<Food> food);
    }
}