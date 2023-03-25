using MongoDB.Driver;

namespace TodoApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoDB"));
            _database = client.GetDatabase("TodoDB");
        }

        public IMongoDatabase Database => _database;
    }
}
