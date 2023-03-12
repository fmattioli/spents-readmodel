using Microsoft.Extensions.DependencyInjection;
using Spents.ReadModel.Crosscutting.Middlewares;

namespace Spents.ReadModel.Crosscutting.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            return services;
        }
    }
}
