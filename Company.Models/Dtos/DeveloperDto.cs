using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company.Models.Dtos
{
    public class DeveloperDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientKey { get; set; }
        public string SecretId { get; set; }
    }
}
