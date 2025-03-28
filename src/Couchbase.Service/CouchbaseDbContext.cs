using Couchbase.KeyValue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Couchbase.Service;

public class CouchbaseDbContext
{
    private readonly ICluster _cluster;
    private readonly CouchbaseSettings _settings;
    public IBucket Bucket { get; }

    public CouchbaseDbContext(IBucket bucket, IOptions<CouchbaseSettings> options)
    {
        _settings = options.Value;
        Bucket = bucket;
    }

    public ICouchbaseCollection GetCollection(string scope = "_default", string collection = "_default") =>
        Bucket.Scope(scope).Collection(collection);
}

