using ConferenceMonitorApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceMonitorApi
{
    public class UserRepository<TDbContext> : IUserRepository where TDbContext : DbContext
    {   
        // Define database context field
        protected TDbContext dbContext;

        // Construct the database context
        public UserRepository(TDbContext context)
        {
            dbContext = context;
        }

        // Method for registering a user
        public async Task CreateAsync<T>(T entity) where T : class
        {
            this.dbContext.Set<T>().Add(entity);

            _ = await this.dbContext.SaveChangesAsync();
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