using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;

namespace ServiceHub.DAL.Interfaces
{
    public interface IUserConnectionRepo : IGenericRepo<UserConnection>
    {
        Task<UserConnection> GetRowByConnectionId(string connectionId);
        Task<IEnumerable<UserConnection>> GetRowsByUserId(int userId);
        Task RemoveAsync(UserConnection obj);

    }
}
