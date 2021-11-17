using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//namespace WorldOfWorms
//{
//    private class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();

//        }
//        public static IHostBuilder CreateHostBuilder(string[] args)
//        {
//            return Host.CreateDefaultBuilder(args)
//                .ConfigureServices((hostContext, services) =>
//                {
//                    services.AddHostedService<WorldSimulatorService>()
//                    .AddSingleton<World>()
//                    .AddSingleton<NameGenerator>()
//                    .AddScoped<IFoodGenerator,FoodGenerator>()
//                    .AddScoped<IWriter, Writer>();
//                    // Служба симулятора мира
//                    //services.AddScoped<FoodGenerator>();
//                    // Генератор еды
//                    // ...
//                });
//        }

//    }
//}