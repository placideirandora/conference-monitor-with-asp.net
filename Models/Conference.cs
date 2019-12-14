namespace api_with_asp.net.Models
{
    public class Conference {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Location { get; set; }
        public string StartDateAndTime { get; set; }
        public string EndDateAndTime { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public decimal TicketPrice { get; set; }
    }
}