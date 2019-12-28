namespace ConferenceMonitorApi {
    // Define an Interface for validating user login credentials
    public interface ISignIn
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}