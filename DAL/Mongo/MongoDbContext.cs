using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NewYearLanding.Model;

namespace NewYearLanding.DAL.Mongo {
    public class MongoDbContext {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<MongoDbConnectionSettings> settings) {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Company> Companies => _database.GetCollection<Company>(nameof(Companies).ToLower());

        public IMongoCollection<Statistics> Statistics => _database.GetCollection<Statistics>(nameof(Statistics).ToLower());
    }
}