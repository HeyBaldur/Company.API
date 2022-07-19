using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company.Models.v1
{
    public class Developer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientKey { get; set; }
        public string SecretId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
