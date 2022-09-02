using Marraia.MongoDb.Repositories.Context;
using Marraia.MongoDb.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marraia.MongoDb.Configurations
{
    public static class MongoDbConfigurationExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection service)
        {
            service.AddScoped<IMongoContext, MongoContext>();
            return service;
        }

        public static IServiceCollection AddMongoDbSingleton(this IServiceCollection service)
        {
            service.AddSingleton<IMongoContext, MongoContext>();
            return service;
        }
    }
}
