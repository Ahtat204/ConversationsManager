using MongoDB.Driver;

namespace ChatHistory.Services
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var ConnectionString = _configuration.GetConnectionString("DbConnection");
            var MongoURL=MongoUrl.Create(ConnectionString);
            var client = new MongoClient(MongoURL);
            _database = client.GetDatabase(MongoURL.DatabaseName);

        }

        public IMongoDatabase? DataBase => _database;
    }
}
