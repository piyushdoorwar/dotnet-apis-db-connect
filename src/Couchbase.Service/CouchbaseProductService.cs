using Common.Interface;
using Common.Model;
using Couchbase.KeyValue;

namespace Couchbase.Service;

public class CouchbaseProductService: IProductService
{
    private readonly ICouchbaseCollection _collection;

    public CouchbaseProductService(CouchbaseDbContext context)
    {
        _collection = context.Bucket.DefaultCollection();
    }

    public async Task AddProduct(Product product)
    {
        await _collection.InsertAsync(product.Id, product);
    }

    public async Task<Product?> GetProduct(string id)
    {
        var result = await _collection.GetAsync(id);
        return result.ContentAs<Product>();
    }

    public async Task<bool> UpdateProduct(string id, Product updated)
    {
        var result = await _collection.ReplaceAsync(id, updated);
        return result.Cas > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        await _collection.RemoveAsync(id);
        return true;
    }

    public async Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var offset = (page - 1) * pageSize;

        var query = @$"
        SELECT p.* FROM `products` p
        LIMIT {pageSize} OFFSET {offset};
        
        SELECT COUNT(*) AS total FROM `products`;";

        var cluster = _collection.Scope.Bucket.Cluster;

        var pagedResult = await cluster.QueryAsync<Product>($@"
        SELECT p.* FROM `products` p
        LIMIT {pageSize} OFFSET {offset}");

        var items = await pagedResult.ToListAsync();

        var countResult = await cluster.QueryAsync<dynamic>("SELECT COUNT(*) AS total FROM `products`");
        var totalRow = await countResult.Rows.FirstOrDefaultAsync();
        long totalCount = totalRow?.total ?? 0;

        return (items, totalCount);
    }
}

