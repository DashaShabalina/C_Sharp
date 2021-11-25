using System.Collections.Generic;
using pos = WorldOfWorms.Сoordinates;

namespace WorldOfWorms
{
    public static class WormFinder
    {
        public static bool Find(Dictionary<Worm, pos> world, int x, int y)
        {
            foreach (pos posI in world.Values)
            {
                if (posI.X == x && posI.Y == y)
                    return true;
            }
            return false;
        }
    }
}
