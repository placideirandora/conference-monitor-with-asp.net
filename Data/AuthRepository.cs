using System.Threading.Tasks;
using ConferenceMonitorApi.Models;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi
{
    public class AuthRepository : IAuthRepository
    {
        DatabaseContext _dbContext;

        public AuthRepository(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

        }
    }
}