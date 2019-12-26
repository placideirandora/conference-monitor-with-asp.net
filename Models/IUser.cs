using System;

namespace ConferenceMonitorApi {
    // Define an Interface for registering a user
    public interface IUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Role { get; set; }
        string Registered { get; set; }
    }
}