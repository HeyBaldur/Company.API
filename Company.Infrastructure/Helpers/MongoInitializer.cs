using Company.Common.Connection.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Company.Infrastructure.Helpers
{
    /// <summary>
    /// Generic Mongo Initializer
    /// </summary>
    public static class MongoInitializer
    {
        public static IMongoCollection<T> Init<T>(IOptions<DatabaseSettings> dbSettings, string collection)
        {
            var mongoClient = new MongoClient(
                dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);

            return mongoDatabase.GetCollection<T>(collection);
        }
    }
}
