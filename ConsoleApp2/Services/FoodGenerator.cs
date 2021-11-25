using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfWorms
{
    public interface IFoodGenerator
    {
        public Food GetFood();
    }

    public class FoodGenerator : IFoodGenerator
    {
        static Random rn = new Random();
        public Food GetFood() => new Food(rn.NextNormal(), rn.NextNormal());
    }
}

