namespace Company.Models.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// Email of the user/developer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full name of the user/developer
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// This is the reason why a user needs/wants to make use of the API
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Name of the company which the user works for
        /// If the user does not work in any company, user can add it as personal reasons
        /// </summary>
        public string Company { get; set; }
    }
}
