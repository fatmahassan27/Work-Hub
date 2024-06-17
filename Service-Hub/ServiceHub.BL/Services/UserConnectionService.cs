using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserConnectionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(UserConnection obj)
        {
            await unitOfWork.UserConnectionRepo.CreateAsync(obj);
            await unitOfWork.saveAsync();
        }

        public async Task<UserConnection> GetRowByConnectionId(string connectionId)
        {
            return await unitOfWork.UserConnectionRepo.GetRowByConnectionId(connectionId);
        }

        public async Task<IEnumerable<UserConnection>> GetRowsByUserId(int userId)
        {
            return await unitOfWork.UserConnectionRepo.GetRowsByUserId(userId); 
        }

        public async Task RemoveAsync(UserConnection obj)
        {
            await unitOfWork.UserConnectionRepo.RemoveAsync(obj);
            await unitOfWork.saveAsync();
        }
    }
}
