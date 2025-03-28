using Mongo.Service;
using MongoDB.Driver;
using Common.Model;

namespace GraphQl.Api;

public class Mutation
{
    public async Task<Product> AddProductAsync(Product input, [Service] MongoDbContext context)
    {
        await context.Products.InsertOneAsync(input);
        return input;
    }

    public async Task<bool> DeleteProductAsync(string id, [Service] MongoDbContext context)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await context.Products.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
