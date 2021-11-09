namespace WorldOfWorms
{
    class FoodController
    {
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
            Food curFood;

            //еда создалась повторно в одной точке - пересоздать
            while (true)
            {
                curFood = new Food();
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
                    if(world.WorldInfo[worm].X==curFood.X && world.WorldInfo[worm].Y == curFood.Y)
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
