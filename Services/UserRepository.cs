using ConferenceMonitorApi.Data;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceMonitorApi
{
    public class UserRepository : IUserRepository
    {   
        // Define database context field
        protected DatabaseContext dbContext;

        // Construct the database context
        public UserRepository(DatabaseContext context)
        {
            dbContext = context;
        }

        // Method for deleting a user
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            this.dbContext.Set<T>().Remove(entity);

            _ = await this.dbContext.SaveChangesAsync();
        }

        // Method for retrieving all users
        public async Task<List<T>> FindAllAsync<T>() where T : class
        {
            return await this.dbContext.Set<T>().ToListAsync();

        }
        
        // Method for retrieving a user
        public async Task<T> FindByIdAsync<T>(int id) where T : class
        {
            return await this.dbContext.Set<T>().FindAsync(id);

        }

        // Method for updating a user
        public async Task UpdateAsync<T>(int id, T entity) where T : class
        {
            this.dbContext.Set<T>().Update(entity);

            _ = await this.dbContext.SaveChangesAsync();
        }
    }
}