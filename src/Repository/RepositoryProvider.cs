using Repository.Interfaces;
using Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryProviderExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRequestRepository, ProductRequestRepository>();
            return services;
        }
    }
}