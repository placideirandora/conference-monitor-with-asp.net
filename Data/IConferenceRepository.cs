using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceMonitorApi.Data
{
    public interface IConferenceRepository
    {
        Task<List<T>> FindAll<T>() where T : class;
        Task<T> FindById<T>(int id) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(int id, T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}