using Spents.ReadModel.Application.Queries;
using MediatR;

namespace Spents.ReadModel.API.Extensions
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetReceiptsQuery));
            return services;
        }
    }
}
