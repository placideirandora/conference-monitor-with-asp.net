using ConferenceMonitorApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceMonitorApi
{
    public class ConferenceRepository<TDbContext> : IConferenceRepository where TDbContext : DbContext
    {
        protected TDbContext dbContext;

        public ConferenceRepository(TDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            this.dbContext.Set<T>().Add(entity);

            _ = await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            this.dbContext.Set<T>().Remove(entity);

            _ = await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> FindAll<T>() where T : class
        {
            return await this.dbContext.Set<T>().ToListAsync();

        }

        public async Task<T> FindById<T>(int id) where T : class
        {
            return await this.dbContext.Set<T>().FindAsync(id);

        }

        public async Task UpdateAsync<T>(int id, T entity) where T : class
        {
            this.dbContext.Set<T>().Update(entity);

            _ = await this.dbContext.SaveChangesAsync();
        }
    }
}