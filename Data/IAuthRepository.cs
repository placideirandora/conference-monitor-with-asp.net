using System.Threading.Tasks;
using ConferenceMonitorApi.Models;

namespace ConferenceMonitorApi {
    // Define an interface for the auth repository functionalities
    public interface IAuthRepository 
    {
        Task RegisterAsync(User entity);
        Task<User> AuthenticateAsync(string email, string password);
    }
}