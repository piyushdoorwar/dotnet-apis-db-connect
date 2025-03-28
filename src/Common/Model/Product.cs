using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Common.Model;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
