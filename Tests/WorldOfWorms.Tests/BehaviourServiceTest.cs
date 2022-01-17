using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WorldofWorms.Data;

namespace WorldOfWorms.Tests
{
    public class BehaviourServiceTest
    {
        private DbContextOptions<EnvironmentContext> options;

        [SetUp]
        public void SetUpDb()
        {
            options = new DbContextOptionsBuilder<EnvironmentContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
        }

        [Test]
        public void ShouldBeRightWorkedBehaviourService()
        {
            using var context = new EnvironmentContext(options);
            var foodGenerator = new FoodGenerator();

            var service = new BehaviourService(context, foodGenerator);

            var behaviourName = "Test";
            service.GenerateBehavior(context, behaviourName, foodGenerator);
            service.steps.Should().NotBeNull();
            service.steps.Should().NotBeEmpty();
            service.behavior.Name.Should().Be(behaviourName);

            var listbehaviour = new List<Сoordinates>();
            var state = new World();
            var name = context.Behaviours.Where(b => b.Name == behaviourName);

            IEnumerable<BehaviorInfo> behaviors = service.GetBehavior(context, behaviourName);

            foreach (var j in behaviors)
            {
                listbehaviour.Add(new Сoordinates(j.X, j.Y));
            }

            for (int i = 0; i < 10; i++)
            {
                Food curFood = new Food(listbehaviour[i].X, listbehaviour[i].Y);

                FoodController foodController = new FoodController(curFood);
                foodController.ControlFood(state);

                state.Food[i].X.Should().Be(curFood.X);
                state.Food[i].Y.Should().Be(curFood.Y);
            }
        }
    }
}
