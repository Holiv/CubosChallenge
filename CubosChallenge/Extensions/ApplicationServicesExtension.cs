using Core.Interfaces;
using Infrastructure.Data;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace CubosChallenge.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen();
            services.AddDbContext<FinServicesContext>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
