using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorldOfWorms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorldSimulatorService>()
                    .AddSingleton<World>()
                    .AddSingleton<NameGenerator>()
                    .AddScoped<IFoodGenerator, FoodGenerator>()
                    .AddScoped<IWriter, Writer>();
                });
        }
    }
}