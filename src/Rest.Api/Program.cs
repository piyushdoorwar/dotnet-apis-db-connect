using Common.Interface;
using Couchbase;
using Couchbase.Service;
using Microsoft.Extensions.Options;
using Mongo.Service;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<CouchbaseSettings>(builder.Configuration.GetSection("CouchbaseSettings"));
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<IOptions<MongoDbSettings>>().Value.ConnectionString));
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
    return Cluster.ConnectAsync(settings.ConnectionString, settings.Username, settings.Password).Result;
});
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
    var cluster = sp.GetRequiredService<ICluster>();
    return cluster.BucketAsync(settings.Bucket).Result;
});
builder.Services.AddSingleton<CouchbaseDbContext>();
if (builder.Configuration.GetValue<bool>("UseMongo"))
    builder.Services.AddSingleton<IProductService, MongoProductService>();
else
    builder.Services.AddSingleton<IProductService, CouchbaseProductService>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();