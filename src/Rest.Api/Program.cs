using Common.Interface;
using Couchbase;
using Couchbase.Service;
using Elastic.Service;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Mongo.Service;
using MongoDB.Driver;
using Nest;
using Rest.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddCouchbase(builder.Configuration);
builder.Services.AddElasticsearch(builder.Configuration);
builder.Services.AddProductService(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();