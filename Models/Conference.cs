using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConferenceMonitorApi.Data;

namespace ConferenceMonitorApi.Models
{   
    // Create and validate the model for conferences table
    [Table("Conferences")]
    public class Conference : IConference {
        public int Id { get; set; }

        [Required(ErrorMessage = "Conference Name is required")]
        [StringLength(50, ErrorMessage = "Conference Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Conference Theme is required")]
        [StringLength(100, ErrorMessage = "Conference Theme cannot be longer than 100 characters")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "Conference Location is required")]
        [StringLength(50, ErrorMessage = "Conference location cannot be longer than 30 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Conference Start Date and Time are required")]
        public string StartDateAndTime { get; set; }

        [Required(ErrorMessage = "Conference End Date and Time are required")]
        public string EndDateAndTime { get; set; }

        [Required(ErrorMessage = "Conference Description is required")]
        [StringLength(400, ErrorMessage = "Conference Description cannot be longer than 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Conference Organizer is required")]
        [StringLength(30, ErrorMessage = "Conference Organizer cannot be longer than 30 characters")]
        public string Organizer { get; set; }

        public decimal TicketPrice { get; set; }

        [ForeignKey(nameof(User))]

        public int PublisherID { get; set; }

        public string PublisherEmail { get; set; }
    }
}