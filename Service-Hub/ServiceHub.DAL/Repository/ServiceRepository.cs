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
        private readonly UserManager<ApplicationUser> userManager;

        public ServiceRepository(ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = userManager;
        }
      
        public ApplicationUser FindEmail(string email)
        {
           
            return db.Users.FirstOrDefault(a => a.Email == email);

        }

        public async Task Delete(string id)
        {
            var data = await db.Users.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                db.Users.Remove(data);
            }
        }
        public async Task<List<Order>> GetAllOrdersByUserId(int userId)
        {
            return await db.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
    
        public async Task<List<ApplicationUser>> GetAllWorkersByJobId(int jobId)
        {
            return await db.Users.Where(w => w.JobId == jobId).ToListAsync();
        }
    }
}
