using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using IBurguer.BDD.Infrastructure.Menu;

namespace IBurguer.BDD
{
    public class Dependencies
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        [ScenarioDependencies]
        public static IServiceCollection AddDependencies()
        {
            var services = new ServiceCollection();

            services.Configure<MenuServiceConfiguration>(Configuration.GetSection("MenuService"));

            services.AddHttpClient<IMenuService, MenuService>();

            return services;
        }
    }
}
