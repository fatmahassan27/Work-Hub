using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.GenericRepository;
using Microsoft.EntityFrameworkCore;
namespace ServiceHub.DAL.Repositories
{
    public class UserConnectionsRepo : GenericRepo<UserConnection>, IUserConnectionRepo
    {
        public UserConnectionsRepo(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<UserConnection> GetRowByConnectionId(string connectionId)
        {
            return await db.UserConnections.FirstOrDefaultAsync(uc => uc.ConnectionId == connectionId);
        }

        public async Task<IEnumerable<UserConnection>> GetRowsByUserId(int userId)
        {
            return await db.UserConnections.Where(uc => uc.UserId == userId).ToListAsync();
        }

        public async Task RemoveAsync(UserConnection obj)
        {
            db.UserConnections.Remove(obj);
            await db.SaveChangesAsync();
        }
    }
}
