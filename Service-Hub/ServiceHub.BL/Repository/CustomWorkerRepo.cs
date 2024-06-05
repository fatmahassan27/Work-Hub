using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Repository
{
    public class CustomWorkerRepo
    {
        private readonly ApplicationDbContext db;

        public CustomWorkerRepo(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Worker>> GetAllByJobId(int jobId)
        {
            return await db.Workers.Where(w=>w.JobId == jobId).ToListAsync();
        }
    }
}
