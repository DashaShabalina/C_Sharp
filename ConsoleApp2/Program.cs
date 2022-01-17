using Microsoft.EntityFrameworkCore;
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
                    .AddScoped<IWriter, Writer>()
                    .AddDbContextFactory<WorldofWorms.Data.EnvironmentContext>(options =>
                    {
                        var connectionString =
                                        @"Server = DASHA\DASHA;Database = WormsWorld.Environment;User Id = dasha; Password = dasha";
                        options.UseSqlServer(connectionString);
                    })
                    .AddScoped<IBehaviourService,BehaviourService>();
                });
        }
    }
}