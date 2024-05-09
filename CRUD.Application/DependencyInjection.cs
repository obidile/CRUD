using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CRUD.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(x =>
            {
                x.AllowNullCollections = true;
                x.AddMaps(Assembly.GetExecutingAssembly());
            });
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
