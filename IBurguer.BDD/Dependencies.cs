using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using IBurguer.BDD.Infrastructure.Menu;
using IBurguer.BDD.Infrastructure.ShoppingCart;
using IBurguer.BDD.Infrastructure.Orders;
using IBurguer.BDD.Infrastructure.Payments;

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
            services.Configure<ShoppingCartConfiguration>(Configuration.GetSection("ShoppingCartService"));
            services.Configure<OrdersServiceConfiguration>(Configuration.GetSection("OrderService"));
            services.Configure<PaymentsServiceConiguration>(Configuration.GetSection("PaymentsService"));

            services.AddHttpClient<IMenuService, MenuService>();
            services.AddHttpClient<IShoppingCartService, ShoppingCartService>();
            services.AddHttpClient<IOrdersService, OrdersService>();
            services.AddHttpClient<IPaymentsService, PaymentsService>();

            return services;
        }
    }
}
