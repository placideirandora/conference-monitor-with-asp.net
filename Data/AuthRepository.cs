using System.Threading.Tasks;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi
{
    public class AuthRepository : IAuthRepository
    {   
        // Define database context field
        DatabaseContext _dbContext;

        // Construct the database context
        public AuthRepository(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        // Method for registering a user
         public async Task RegisterAsync(User entity)
        {
            _dbContext.Users.Add(entity);

            _ =  await _dbContext.SaveChangesAsync();
        }

        // Method for authenticating a user
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

        }
    }
}