using Common.Interface;
using Couchbase;
using Couchbase.Service;
using Elastic.Service;
using Elasticsearch.Net;
using Google.Api;
using Microsoft.Extensions.Options;
using Mongo.Service;
using MongoDB.Driver;
using Nest;

namespace Rest.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductService(this IServiceCollection services, IConfiguration config)
    {
        var provider = config.GetValue<string>("DataProvider")?.ToLower();
        switch (provider)
        {
            case "mongo":
                services.AddSingleton<IProductService, MongoProductService>();
                break;
            case "couchbase":
                services.AddSingleton<IProductService, CouchbaseProductService>();
                break;
            case "elastic":
                services.AddSingleton<IProductService, ElasticProductService>();
                break;
            default:
                throw new InvalidOperationException("Unsupported DataProvider");
        }
        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<IOptions<MongoDbSettings>>().Value.ConnectionString));
        return services;
    }

    public static IServiceCollection AddCouchbase(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CouchbaseSettings>(config.GetSection("CouchbaseSettings"));
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
            return Cluster.ConnectAsync(settings.ConnectionString, settings.Username, settings.Password).Result;
        });
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
            var cluster = sp.GetRequiredService<ICluster>();
            return cluster.BucketAsync(settings.Bucket).Result;
        });
        services.AddSingleton<CouchbaseDbContext>();
        return services;
    }

    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ElasticsearchSettings>(config.GetSection("ElasticsearchSettings"));
        services.AddSingleton<IElasticClient>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
            var settings = new ConnectionSettings(cloudId: "my-elasticsearch-project-af1ad6.es.ap-southeast-1.aws.elastic.cloud", credentials: new ApiKeyAuthenticationCredentials(config.EncodedApiKey))
            .DefaultIndex(config.IndexName)
            .EnableApiVersioningHeader();
            return new ElasticClient(settings);
        });
        return services;
    }
}
