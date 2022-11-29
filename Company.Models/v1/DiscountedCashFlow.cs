using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Company.Models.v1
{
    public class DiscountedCashFlow
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public double CashFlow { get; set; }
        public double DiscountRate { get; set; }
    }
}
