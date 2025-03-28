using Common.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Mongo.Service;
public class MongoDbContext
{
    private readonly IMongoDatabase _db;
    public IMongoCollection<Product> Products => _db.GetCollection<Product>("Products");

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
        _db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
    }
}
