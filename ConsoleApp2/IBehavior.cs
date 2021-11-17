using System.Collections.Generic;

namespace WorldOfWorms
{
    interface IBehavior
    {
        Actions Execute(Dictionary<Worm, Сoordinates> world, List<Food> food);
    }
}