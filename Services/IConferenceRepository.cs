using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceMonitorApi.Data
{   
    // Define an interface for the conference repository functionalities
    public interface IConferenceRepository
    {
        Task<List<T>> FindAllAsync<T>() where T : class;
        Task<T> FindByIdAsync<T>(int id) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(int id, T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}