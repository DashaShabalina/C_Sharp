using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FluentAssertions;
using WorldofWorms.Data;

namespace WorldOfWorms.Tests
{
    public class BehaviorGenerationTest 
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
        public void ShouldBeRightGenerate()
        {
            using var context = new EnvironmentContext(options);
            var foodGenerator = new FoodGenerator();

            var service = new BehaviourService(context, foodGenerator);

            var behaviourName = "Test";
            service.GenerateBehavior(context, behaviourName, foodGenerator);
            service.steps.Should().NotBeNull();
            service.steps.Should().NotBeEmpty();
            service.behavior.Name.Should().Be(behaviourName);
        }
    }
}
