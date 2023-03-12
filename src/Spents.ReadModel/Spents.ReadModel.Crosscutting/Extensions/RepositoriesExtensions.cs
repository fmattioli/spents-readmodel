using Microsoft.Extensions.DependencyInjection;
using Spents.ReadModel.Domain.Interfaces;
using Spents.ReadModel.Infrastructure.Data;
using Spents.ReadModel.Infrastructure.Persistence;

namespace Spents.ReadModel.Crosscutting.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<DbSession>();
            services.AddTransient<IReceiptRepository, ReceiptRepository>();
            return services;
        }
    }
}
