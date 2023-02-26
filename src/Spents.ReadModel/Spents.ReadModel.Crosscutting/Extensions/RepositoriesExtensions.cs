using Microsoft.Extensions.DependencyInjection;
using Spents.ReadModel.Domain.Interfaces;

namespace Spents.ReadModel.Crosscutting.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IReceiptRepository, ReceiptEventsRepository>();
            return services;
        }
    }
}
