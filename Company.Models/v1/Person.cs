using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Company.Models.v1
{
    /// <summary>
    /// Our Person API lets you lookup a person based on an email address OR 
    /// based on a domain name + first name + last name. You get a full overview 
    /// of the person including name, location, email, phone number, social links and more.
    /// </summary>
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string JobFunction { get; set; }
        public string ManagementLevel { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string EmailStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileDirectdial { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string LinkedinUrl { get; set; }
        public string Industry { get; set; }
        public string Domain { get; set; }
        public string CompanyName { get; set; }
    }
}
