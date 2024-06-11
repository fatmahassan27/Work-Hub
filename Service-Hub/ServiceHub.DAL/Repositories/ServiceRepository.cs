using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;

namespace ServiceHub.BL.Repositories
{
    public class ServiceRepository 
    {
        private readonly ApplicationDbContext db;

        public ServiceRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
    
        public async Task<List<ApplicationUser>> GetAllWorkersByJobIdAsync(int jobId)
        {
            return await db.Users.Where(w => w.JobId == jobId).ToListAsync();
        }
    }
}
