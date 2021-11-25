using System.Collections.Generic;
using System;

namespace WorldOfWorms
{
    public class Behavior : IBehavior
    {
        private readonly Worm worm;
        public Behavior(Worm worm)
        {
            this.worm = worm;
        }
        public Actions Execute(Dictionary<Worm, Сoordinates> world, List<Food> food)
        {
            int minDist = Int32.MaxValue;
            int index = -1;
            int xNearFood = 0, yNearFood = 0;
            if (worm.Health >= 12)
            {
                return Actions.Reproduce;
            }
            else
            {
                foreach (Food iFood in food)
                {
                    int dist = Math.Abs(iFood.X - world[worm].X) + Math.Abs(iFood.Y - world[worm].Y);
                    if (dist < minDist && dist <= iFood.ShelfLife)
                    {
                        minDist = dist;
                        xNearFood = iFood.X;
                        yNearFood = iFood.Y;
                        index = food.IndexOf(iFood);
                    }
                }

                if (worm.Health < minDist)
                {
                    return Actions.StayPut;
                }
                else
                {
                    if (xNearFood != world[worm].X)
                    {
                        int coorX = world[worm].X + 1;
                        int coorX1 = world[worm].X - 1;
                        int coorY = world[worm].Y;
                        if ((yNearFood == coorY && xNearFood == coorX) || (yNearFood == coorY && xNearFood == coorX1))
                        {
                            worm.Health += 10;
                            food.RemoveAt(index);
                        }
                        return xNearFood < world[worm].X ? Actions.Left : Actions.Right;
                    }
                    else
                    {
                        int coor = world[worm].Y + 1;
                        int coor1 = world[worm].Y - 1;
                        if (yNearFood == coor || yNearFood == coor1)
                        {
                            worm.Health += 10;
                            food.RemoveAt(index);
                        }
                        return yNearFood < world[worm].Y ? Actions.Back : Actions.Forward;
                    }
                }
            }
        }
    }
}
