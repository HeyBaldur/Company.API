﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company.Models.v1
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string ClientKey { get; set; }
        public string SecretId { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Reason { get; set; }
        public string Company { get; set; }
    }
}
