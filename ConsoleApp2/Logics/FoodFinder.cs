using System.Collections.Generic;
using pos = WorldOfWorms.Сoordinates;

namespace WorldOfWorms
{
    static class FoodFinder
    {
        public static bool Find(List<Food> food, int x, int y)
        {
            foreach (Food iFood in food)
            {
                if (iFood.X == x && iFood.Y == y)
                    return true;
            }
            return false;
        }
    }
}
