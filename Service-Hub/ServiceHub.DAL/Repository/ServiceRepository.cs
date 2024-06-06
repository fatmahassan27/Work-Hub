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
        public  ApplicationUser FindEmail(string email)
        {
            return  db.Users.FirstOrDefault(a => a.Email == email);
        }


        //public ApplicationUser Findemail(string email)
        //{
        //    return   db..FirstOrDefault(a => a.Email == email);
        //}

        //public async Task SoftDelete(int id)
        //{
        //    var data = await db.ApplicationUser.FindAsync(id);
        //    if (data != null)
        //    {
        //        data.IsDeleted = true;
        //        db.Workers.Remove(data);
        //    }
        //}
        public async Task<List<Order>> GetAllOrdersByUserId(string userId)
        {
            return await db.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrdersByWorkerId(string workerId)
        {
            return await db.Orders.Where(o => o.WorkerId == workerId).ToListAsync();
        }
        //public async Task<List<ApplicationUser>> GetAllWorkersByJobId(string jobId)
        //{
        //    return await db.worker.Where(w => w.JobId == jobId).ToListAsync();
        //}
    }
}
