using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Common.Model;
using Common.Interface;

namespace Mongo.Service;

public class MongoProductService: IProductService
{
    private readonly IMongoCollection<Product> _products;

    public MongoProductService(IOptions<MongoDbSettings> options, IMongoClient client)
    {
        var mongoSettings = options.Value;
        var database = client.GetDatabase(mongoSettings.DatabaseName);
        _products = database.GetCollection<Product>("Products");
    }

    public async Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var totalCount = await _products.CountDocumentsAsync(_ => true);
        var items = await _products.Find(_ => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<Product?> GetProduct(string id) =>
        await _products.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task AddProduct(Product product) =>
        await _products.InsertOneAsync(product);

    public async Task<bool> UpdateProduct(string id, Product updated)
    {
        var result = await _products.ReplaceOneAsync(p => p.Id == id, updated);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await _products.DeleteOneAsync(p => p.Id == id);
        return result.DeletedCount > 0;
    }
}
