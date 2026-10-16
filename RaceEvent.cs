
using System.ComponentModel.DataAnnotations;

namespace RaceDayAPI.Models
{
    public class RaceEvent
    {
        public int RaceEventId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int OrganiserId { get; set; }

        public Organiser? Organiser { get; set; }
    }
}
