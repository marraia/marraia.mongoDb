using Marraia.MongoDb.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marraia.MongoDb.Repositories.Context
{
    public class MongoContext : IMongoContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public MongoContext(IConfiguration configuration)
        {
            if (configuration["MongoSettings:Connection"] is null
                || configuration["MongoSettings:Database"] is null)
                throw new MongoClientException("Informe em seu arquivo de configuração, o servidor e database MongoDb");

            Client = new MongoClient(configuration["MongoSettings:Connection"]);
            Database = Client.GetDatabase(configuration["MongoSettings:Database"]);

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        }
    }
}
