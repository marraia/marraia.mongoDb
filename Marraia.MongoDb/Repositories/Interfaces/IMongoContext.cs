using MongoDB.Driver;

namespace Marraia.MongoDb.Repositories.Interfaces
{
    public interface IMongoContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}
