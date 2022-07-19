namespace Company.Models.v1
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientKey { get; set; }
        public string SecretId { get; set; }
    }
}
