using ServiceHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Interfaces
{
    public interface IUserConnectionService
    {
        Task<UserConnection> GetRowByConnectionId(string connectionId);
        Task<IEnumerable<UserConnection>> GetRowsByUserId(int userId);
        Task RemoveAsync(UserConnection obj);
        Task CreateAsync(UserConnection obj);
    }
}
