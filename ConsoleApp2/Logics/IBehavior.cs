using System.Collections.Generic;

namespace WorldOfWorms
{
    public interface IBehavior
    {
        Actions Execute(Dictionary<Worm, Сoordinates> world, List<Food> food);
    }
}