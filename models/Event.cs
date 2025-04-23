using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models
{
    [Table("events")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Duration { get; set; }

        [Required]
        [MaxLength(50)]
        public string Time { get; set; }

        [Required]
        public string Date { get; set; }

        public List<EventUser> EventUsers { get; set; }
    }
}
