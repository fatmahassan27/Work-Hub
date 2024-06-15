namespace ServiceHub.DAL.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T obj);
        Task UpdateAsync(int id,T obj);
        Task DeleteAsync(int id);
    }
}
