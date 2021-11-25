using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace WorldOfWorms
{
    public class FoodController
    {
        //private IServiceScopeFactory _scopeFactory;
        /*public FoodController(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }*/
        Food curFood;
        public FoodController(Food curFood)
        {
            this.curFood = curFood;
        }
        public void ControlFood(World world)
        {
            bool flag = false;
            int index = 0;
            foreach (Food iFood in world.Food)
            {
                iFood.ShelfLife--;
                if (iFood.ShelfLife == 0)
                {
                    flag = true;
                    index = world.Food.IndexOf(iFood);
                }
            }

            if (flag)
            {
                world.RemoveFood(index);
            }
            

            //еда создалась повторно в одной точке - пересоздать
            while (true)
            {
               /* using (var foodScope = _scopeFactory.CreateScope())
                {
                    var foodGenerator = foodScope.ServiceProvider.GetRequiredService<IFoodGenerator>();
                    curFood = foodGenerator.GetFood();*/
                
                   // curFood = new FoodGenerator().GetFood();
                int flag1 = 0;
                foreach (Food iFood in world.Food)
                {
                    if (curFood.X == iFood.X && curFood.Y == iFood.Y)
                    {
                        flag1++;
                        break;
                    }
                }
                if (flag1 == 0)
                {
                    world.AddFood(curFood);
                    break;
                }
            }
                //случай когда добавили еду, которая совпала с координатами червяка
                if (WormFinder.Find(world.WorldInfo, curFood.X, curFood.Y))
                {
                    foreach (Worm worm in world.WorldInfo.Keys)
                    {
                        if (world.WorldInfo[worm].X == curFood.X && world.WorldInfo[worm].Y == curFood.Y)
                        {
                            worm.Health += 10;
                            break;
                        }
                    }
                    world.Food.Remove(curFood);
                }
            }
        }
    }
//}
