
using GraphQl.Api;
using Mongo.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MongoDbContext>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddMongoDbFiltering()
    .AddMongoDbSorting()
    .AddMongoDbPagingProviders();

var app = builder.Build();
app.MapGraphQL();
app.Run();