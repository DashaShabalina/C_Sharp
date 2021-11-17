//using System.Text;
//using System.Collections.Generic;
//using Microsoft.Extensions.Hosting;
//using System.Threading.Tasks;
//using System.Threading;
//using Microsoft.Extensions.DependencyInjection;

//namespace WorldOfWorms
//{
//    class WorldSimulatorService : IHostedService
//    {
//        private readonly World world;
//        public static readonly int numberMoves = 100;
//        private readonly List<Сoordinates> newWorms = new List<Сoordinates>();
//        private readonly List<Worm> wormsRemove = new List<Worm>();
//        private Writer writer;
//        private bool _running = true;
//        private readonly IApplicationLifetime _applicationLifetime;
//        private readonly IServiceScopeFactory _scopeFactory;

//        public WorldSimulatorService(IApplicationLifetime applicationLifetime, IServiceScopeFactory scopeFactory)
//        {
//            _applicationLifetime = applicationLifetime;
//            _scopeFactory = scopeFactory;
//            world = new World();
//        }
//        private void RemoveWorms()
//        {
//            foreach (Worm worm in world.WorldInfo.Keys)
//            {
//                if (worm.Health == 0)
//                {
//                    wormsRemove.Add(worm);
//                }
//            }
//            foreach (Worm i in wormsRemove)
//            {
//                world.WorldInfo.Remove(i);
//            }
//            wormsRemove.Clear();
//        }
//        private void AskWorms(int move)
//        {
//            foreach (Worm worm in world.WorldInfo.Keys)
//            {
//                StringBuilder sb = new StringBuilder("");
//                foreach (Food i in world.Food)
//                {
//                    sb.Append($"({i.X},{i.Y})");
//                }
//                Actions route = worm.GetBehavior(world.WorldInfo, world.Food);
//                switch (route)
//                {
//                    case Actions.Forward:
//                        if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X, world.WorldInfo[worm].Y+1)))
//                        {
//                            world.WorldInfo[worm].Y++;
//                        }
//                        break;
//                    case Actions.Back:
//                        if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X, world.WorldInfo[worm].Y-1)))
//                        {
//                            world.WorldInfo[worm].Y--;
//                        }
//                        break;
//                    case Actions.Right:
//                        if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X+1, world.WorldInfo[worm].Y)))
//                        {
//                            world.WorldInfo[worm].X++;
//                        }
//                        break;
//                    case Actions.Left:
//                        if (!(WormFinder.Find(world.WorldInfo, world.WorldInfo[worm].X-1, world.WorldInfo[worm].Y)))
//                        {
//                            world.WorldInfo[worm].X--;
//                        }
//                        break;
//                    case Actions.StayPut:
//                        break;
//                    case Actions.Reproduce:
//                        newWorms.Add(new Сoordinates(world.WorldInfo[worm].X, world.WorldInfo[worm].Y));
//                        worm.Health -= 10;
//                        break;
//                }
//                worm.Health--;
//                writer.Write(new PrintInfo(worm.Name, world.WorldInfo[worm].X, world.WorldInfo[worm].Y, move + 1, sb, route, worm.Health));
//               // worm.Health--;
//            }
//        }
//        public void Start()
//        {

//            FoodController foodController = new FoodController();
//            PullulationController controlPullulation = new PullulationController();
//            writer = new Writer();
//            for (int i = 0; i < numberMoves && _running ; i++)
//            {
//                foodController.ControlFood(world);
//                RemoveWorms();
//                AskWorms(i);
//                if (newWorms.Count != 0)
//                {
//                    controlPullulation.ControlPullulation(world, newWorms);
//                }
//            }
//            writer.Close();
//            _applicationLifetime.StopApplication();
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            Task.Run(Start);
//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _running = false;
//            return Task.CompletedTask;
//        }
//    }
//}

