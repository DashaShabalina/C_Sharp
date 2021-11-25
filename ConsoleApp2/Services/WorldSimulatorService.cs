using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace WorldOfWorms
{
    public class WorldSimulatorService : IHostedService
    {
        private readonly World world;
        private readonly int _numberMoves = 100;
        public readonly List<Сoordinates> newWorms = new List<Сoordinates>();
        private readonly List<Worm> wormsRemove = new List<Worm>();
        private bool _running = true;
        private readonly IApplicationLifetime _applicationLifetime;
        public IServiceScopeFactory _scopeFactory;

        public WorldSimulatorService(IApplicationLifetime applicationLifetime, IServiceScopeFactory scopeFactory,World world, int numberMoves = 100)
        {
            _applicationLifetime = applicationLifetime;
            _scopeFactory = scopeFactory;
            this.world = world == null ? new World() : world;
            //world = new World();
            _numberMoves = numberMoves;
        }
        private void RemoveWorms()
        {
            foreach (Worm worm in world.WorldInfo.Keys)
            {
                if (worm.Health == 0)
                {
                    wormsRemove.Add(worm);
                }
            }
            foreach (Worm i in wormsRemove)
            {
                world.WorldInfo.Remove(i);
            }
            wormsRemove.Clear();
        }
        public void AskWorms(Worm worm, IBehavior behavior,out Actions route)
        {

            route = behavior.Execute(world.WorldInfo, world.Food);
            //route = worm.GetBehavior(world.WorldInfo, world.Food);
            switch (route)
            {
                case Actions.Forward:
                    if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X, world.WorldInfo[worm].Y + 1)))
                    {
                        world.WorldInfo[worm].Y++;
                    }
                    break;
                case Actions.Back:
                    if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X, world.WorldInfo[worm].Y - 1)))
                    {
                        world.WorldInfo[worm].Y--;
                    }
                    break;
                case Actions.Right:
                    if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X + 1, world.WorldInfo[worm].Y)))
                    {
                        world.WorldInfo[worm].X++;
                    }
                    break;
                case Actions.Left:
                    if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X - 1, world.WorldInfo[worm].Y)))
                    {
                        world.WorldInfo[worm].X--;
                    }
                    break;
                case Actions.StayPut:
                    break;
                case Actions.Reproduce:
                    newWorms.Add(new Сoordinates(world.WorldInfo[worm].X, world.WorldInfo[worm].Y));
                    worm.Health -= 10;
                    break;
            }

            // worm.Health--;

        }
        public void Start()
        {
            using (var writerScope = _scopeFactory.CreateScope())
            {
                this.world.AddWorm(0, 0);
                var writer = writerScope.ServiceProvider.GetRequiredService<IWriter>();
 
                PullulationController controlPullulation = new PullulationController();
                // writer = new Writer();
                for (int i = 0; i < _numberMoves && _running; i++)
                {
                    using (var foodScope = _scopeFactory.CreateScope())
                    {
                        var foodGenerator = foodScope.ServiceProvider.GetRequiredService<IFoodGenerator>();
                        Food curFood = foodGenerator.GetFood();
                        FoodController foodController = new FoodController(curFood);
                        foodController.ControlFood(world);
                    }
                    RemoveWorms();
                    foreach (Worm worm in world.WorldInfo.Keys)
                    {
                        StringBuilder sb = new StringBuilder("");
                        foreach (Food j in world.Food)
                        {
                            sb.Append($"({j.X},{j.Y})");
                        }
                        Actions route;
                        AskWorms(worm, new Behavior(worm), out route);
                        worm.Health--;
                        writer.Write(new PrintInfo(worm.Name, world.WorldInfo[worm].X, world.WorldInfo[worm].Y, i + 1, sb, route, worm.Health));
                    }
                    if (newWorms.Count != 0)
                    {
                        controlPullulation.ControlPullulation(world, newWorms);
                    }
                }
                //writer.Close();
            }
            _applicationLifetime.StopApplication();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(Start);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _running = false;
            return Task.CompletedTask;
        }
    }
}

