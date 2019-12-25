namespace ConferenceMonitorApi.Data
{
    public interface IConference
    {
        int Id { get; set; }
        string Name { get; set; }
        string Theme { get; set; }
        string Location { get; set; }
        string StartDateAndTime { get; set; }
        string EndDateAndTime { get; set; }
        string Description { get; set; }
        string Organizer { get; set; }
        decimal TicketPrice { get; set; }
    }
}