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
            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                options.AllowEmptyInputInBodyModelBinding = true;
            });
            services.AddHttpClient();
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICepServices, CepService>();
            serviceCollection.AddScoped<ILoginServices, LoginService>();
            return serviceCollection;
        }

    }
}
