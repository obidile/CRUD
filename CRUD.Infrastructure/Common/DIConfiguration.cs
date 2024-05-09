using CRUD.Application.Common.Interface;
using CRUD.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Craft.Infrastucture.Common;

//public static class DIConfiguration
//{
// This would have been a better otpion for registration but I was getting some errors and had to take another alternative

//    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddDbContext<ApplicationContext>(options =>  options.UseInMemoryDatabase("CRUDDb"));

//        services.AddScoped<IApplicationContext, ApplicationContext>();

//        return services;
//    }
//}
