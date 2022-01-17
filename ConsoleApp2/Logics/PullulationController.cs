using System.Collections.Generic;
namespace WorldOfWorms
{
    public class PullulationController
    {
        public void ControlPullulation(World world, List<Сoordinates> newWorms)
        {
            var count = world.WorldInfo.Count;
            foreach (Сoordinates j in newWorms)
            {
                int flag1 = 0, flag2 = 0, flag3 = 0, flag4 = 0;
                foreach (Worm worm in world.WorldInfo.Keys)
                {
                    if (WormFinder.Find(world.WorldInfo, j.X + 1, j.Y) )
                    {
                        flag1++;
                    }
                    if (WormFinder.Find(world.WorldInfo, j.X - 1, j.Y))
                    {
                        flag2++;
                    }
                    if (WormFinder.Find(world.WorldInfo, j.X, j.Y + 1))
                    {
                        flag3++;
                    }
                    if (WormFinder.Find(world.WorldInfo, j.X, j.Y - 1))
                    {
                        flag4++;
                    }
                }

                foreach (Food iFood in world.Food)
                {
                    if (FoodFinder.Find(world.Food, j.X + 1, j.Y))
                    {
                        flag1++;
                    }
                    if (FoodFinder.Find(world.Food, j.X - 1, j.Y))
                    {
                        flag2++;
                    }
                    if (FoodFinder.Find(world.Food, j.X, j.Y + 1))
                    {
                        flag3++;
                    }
                    if (FoodFinder.Find(world.Food, j.X, j.Y - 1))
                    {
                        flag4++;
                    }
                }

                if (flag1 == 0)
                {
                    world.AddWorm(j.X + 1, j.Y);
                }
                else if (flag2 == 0)
                {
                    world.AddWorm(j.X - 1, j.Y);
                }
                else if (flag3 == 0)
                {
                    world.AddWorm(j.X, j.Y + 1);
                }
                else if (flag4 == 0)
                {
                    world.AddWorm(j.X, j.Y - 1);
                }
                else
                {
                    //если червяк решил размножаться, то уже -10 здоровья, надо вернуть
                }
            }
            newWorms.Clear();
        }
    }
}
