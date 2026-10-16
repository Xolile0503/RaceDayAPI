
namespace RaceDayAPI.Models
{
    public class Organiser
    {
        public int OrganiserId { get; set; }

        public int UserId { get; set; }

        public string OrganisationName { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
