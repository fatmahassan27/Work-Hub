using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.BL.Interface;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Repository
{
    public class ServiceRepository : IServiceRepo
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
      //GEtAll
    }
}
