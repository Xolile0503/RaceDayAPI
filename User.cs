
namespace RaceDayAPI.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Store a password hash, never a plain-text password.
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Participant";
    }
}
