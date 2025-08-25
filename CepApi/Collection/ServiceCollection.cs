using CepApi.Application.Interfaces;
using CepApi.Application.Mapper;
using CepApi.Application.Services;


namespace CepApi.Extension
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AddressProfile));
            services.AddServices();
            services.AddControllers();
            services.AddHttpClient();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICepServices, CepService>();
            return serviceCollection;
        }

    }
}
