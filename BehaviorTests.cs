using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using pos = WorldOfWorms.Сoordinates;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WorldOfWorms.Tests
{
    [TestFixture]
    public class BahaviorTests
    {
        private IServiceProvider _sp;
        public BahaviorTests()
        {
            _sp = Host.CreateDefaultBuilder().Build().Services;
        }

        [Test]
        public void ShouldBeRightBehaviorWorms()
        {
            var state = new World();
            var worm = state.AddWorm(0, 0);
            var expectedBehavior = new List<Actions>()
            {
                Actions.Right,
                Actions.Right,
                Actions.Right,
                Actions.Right,
                Actions.Right,
                Actions.Forward,
                Actions.Forward
            };
            var actualBehavior = new List<Actions>();
            state.AddFood(new Food(5, 2));
            var route = new Actions();
            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
           _sp.GetService<IServiceScopeFactory>(), state, 1);

            for (int i = 0; i < 7; i++)
            {
                simulator.AskWorms(worm, new Behavior(worm), out route);
                actualBehavior.Add(route);
            }
            expectedBehavior.Should().Equal(actualBehavior);
        }
    }
}
