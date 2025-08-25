using CepApi.Application.Mapper;


namespace ViaCepApi.Extension
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AddressProfile));
            services.AddControllers();
            services.AddHttpClient();
            return services;
        }


      
    }
}
