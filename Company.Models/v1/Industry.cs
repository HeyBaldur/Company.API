using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Company.Models.v1
{
    /// <summary>
    /// The Industries Search API is available on the Professional, 
    /// Elite, and Enterprise plans only.
    /// </summary>
    class Industry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IndustryName { get; set; }
    }
}
