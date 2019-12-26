using System.Threading.Tasks;
using ConferenceMonitorApi.Models;

namespace ConferenceMonitorApi {
    public interface IAuthRepository 
    {
        Task RegisterAsync(User entity);
        Task<User> AuthenticateAsync(string email, string password);
    }
}