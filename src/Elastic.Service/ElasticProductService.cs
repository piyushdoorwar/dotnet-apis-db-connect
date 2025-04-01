using Common.Interface;
using Common.Model;
using Microsoft.Extensions.Options;
using Nest;

namespace Elastic.Service;

public class ElasticProductService : IProductService
{
    private readonly IElasticClient _client;
    private readonly string _index;

    public ElasticProductService(IElasticClient client, IOptions<ElasticsearchSettings> settings)
    {
        _client = client;
        _index = settings.Value.IndexName;
    }

    public async Task AddProduct(Product product)
    {
        var response = await _client.IndexDocumentAsync(product);
    }

    public async Task<Product?> GetProduct(string id)
    {
        var response = await _client.GetAsync<Product>(id);
        return response.Found ? response.Source : null;
    }

    public async Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var response = await _client.SearchAsync<Product>(s => s
            .From((page - 1) * pageSize)
            .Size(pageSize)
            .MatchAll());

        return (response.Documents.ToList(), response.Total);
    }

    public async Task<bool> UpdateProduct(string id, Product updated)
    {
        var response = await _client.UpdateAsync<Product>(id, u => u.Doc(updated));
        return response.IsValid;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var response = await _client.DeleteAsync<Product>(id);
        return response.IsValid;
    }
}

