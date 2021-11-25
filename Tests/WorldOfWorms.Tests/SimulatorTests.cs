using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System;
using Moq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WorldOfWorms.Tests
{
    [TestFixture]
    public class SimulatorTests
    {
        private IServiceProvider _sp;
        public SimulatorTests()
        {
            _sp = Host.CreateDefaultBuilder().Build().Services;
        }

        [TestCase(Actions.Left)]
        [TestCase(Actions.Right)]
        [TestCase(Actions.Forward)]
        [TestCase(Actions.Back)]
        [TestCase(Actions.StayPut)]
        public void ShouldBeRightInteraction(Actions action)
        {
            var stabBehaviour = new Mock<IBehavior>();
            stabBehaviour
            .Setup(mb => mb.Execute(It.IsAny<Dictionary<Worm, Ñoordinates>>(), It.IsAny<List<Food>>()))
            .Returns(action);

            var state = new World();
            var worm = state.AddWorm(0, 0);

            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
            _sp.GetService<IServiceScopeFactory>(), state, 1);
            Actions route;
            simulator.AskWorms(worm, stabBehaviour.Object, out route);

            switch (action)
            {
                case Actions.Left:
                    state.WorldInfo[worm].Y.Should().Be(0);
                    state.WorldInfo[worm].X.Should().Be(-1);
                    break;
                case Actions.Right:
                    state.WorldInfo[worm].X.Should().Be(1);
                    state.WorldInfo[worm].Y.Should().Be(0);
                    break;
                case Actions.Forward:
                    state.WorldInfo[worm].X.Should().Be(0);
                    state.WorldInfo[worm].Y.Should().Be(1);
                    break;
                case Actions.Back:
                    state.WorldInfo[worm].X.Should().Be(0);
                    state.WorldInfo[worm].Y.Should().Be(-1);
                    break;
                case Actions.StayPut:
                    state.WorldInfo[worm].X.Should().Be(0);
                    state.WorldInfo[worm].Y.Should().Be(0);
                    break;
            }
        }

        [Test]
        public void ShouldRightReproduce()
        {
            var stabBehaviour = new Mock<IBehavior>();
            stabBehaviour
            .Setup(mb => mb.Execute(It.IsAny<Dictionary<Worm, Ñoordinates>>(), It.IsAny<List<Food>>()))
            .Returns(Actions.Reproduce);

            var state = new World();
            var worm = state.AddWorm(0, 0);

            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
            _sp.GetService<IServiceScopeFactory>(), state, 1);
            Actions route;
            simulator.AskWorms(worm, stabBehaviour.Object, out route);
            new PullulationController().ControlPullulation(state, simulator.newWorms);
            state.WorldInfo.Should().HaveCount(2);
        }

        [Test]
        public void ReproduceUnsuccessful()
        {
            var stabBehaviour = new Mock<IBehavior>();
            stabBehaviour
            .Setup(mb => mb.Execute(It.IsAny<Dictionary<Worm, Ñoordinates>>(), It.IsAny<List<Food>>()))
            .Returns(Actions.Reproduce);
            var state = new World();
            var worm = state.AddWorm(0, 0);
            var worm1 = state.AddWorm(0, 1);
            var worm2 = state.AddWorm(1, 0);
            var worm3 = state.AddWorm(-1, 0);
            var worm4 = state.AddWorm(0, -1);

            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
            _sp.GetService<IServiceScopeFactory>(), state, 1);
            Actions route;
            simulator.AskWorms(worm, stabBehaviour.Object, out route);
            new PullulationController().ControlPullulation(state, simulator.newWorms);
            state.WorldInfo.Should().HaveCount(5);
        }

        [Test]
        public void ShouldRighMoveToCellOccupied()
        {
            var stabBehaviour = new Mock<IBehavior>();
            stabBehaviour
            .Setup(mb => mb.Execute(It.IsAny<Dictionary<Worm, Ñoordinates>>(), It.IsAny<List<Food>>()))
            .Returns(Actions.Right);

            var state = new World();
            var worm = state.AddWorm(0, 0);
            var worm1 = state.AddWorm(1, 0);

            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
            _sp.GetService<IServiceScopeFactory>(), state, 1);
            Actions route;
            simulator.AskWorms(worm, stabBehaviour.Object, out route);

            state.WorldInfo[worm].X.Should().Be(0);
            state.WorldInfo[worm].Y.Should().Be(0);
        }

        [Test]
        public void ShouldRighMoveToCageWithFood()
        {
            var state = new World();
            var worm = state.AddWorm(0, 0);
            state.AddFood(new Food(1, 0));

            var simulator = new WorldSimulatorService(_sp.GetService<IApplicationLifetime>(),
            _sp.GetService<IServiceScopeFactory>(), state, 1);
            Actions route;
            simulator.AskWorms(worm, new Behavior(worm), out route);

            worm.Health.Should().Be(20);
        }
    }
}
