using NUnit.Framework;
using FluentAssertions;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace WorldOfWorms.Tests
{
    [TestFixture]
    public class FoodTests
    {
        private IServiceProvider _sp;
        public FoodTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IFoodGenerator, FoodGenerator>();
            _sp = services.BuildServiceProvider();
        }

        [TestCase(5)]
        public void ShouldBeRightGenerateFood(int countFoods)
        {
            var state = new World();
            for (int i = 0; i < countFoods; i++)
            {
                using (var foodScope = _sp.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var foodGenerator = foodScope.ServiceProvider.GetRequiredService<IFoodGenerator>();
                    Food curFood = foodGenerator.GetFood();
                    FoodController foodController1 = new FoodController(new FoodGenerator().GetFood());
                    foodController1.ControlFood(state);
                }
                state.Food.Should().OnlyHaveUniqueItems();
            }
        }

        [Test]
        public void ShouldBeRightFood()
        {
            var state = new World();
            var worm = state.AddWorm(0, 0);
            using (var foodScope = _sp.GetService<IServiceScopeFactory>().CreateScope())
            {
                var foodGenerator = foodScope.ServiceProvider.GetRequiredService<IFoodGenerator>();
                Food curFood = foodGenerator.GetFood();
                FoodController foodController1 = new FoodController(new Food(0, 0));
                foodController1.ControlFood(state);
            }
            worm.Health.Should().Be(20);
        }
    }
}