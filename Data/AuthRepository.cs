using System.Threading.Tasks;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi
{
    public class AuthRepository : IAuthRepository
    {
        DatabaseContext _dbContext;

        public AuthRepository(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

         public async Task RegisterAsync(User entity)
        {
            _dbContext.Users.Add(entity);

            _ =  await _dbContext.SaveChangesAsync();
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

        }
    }
}