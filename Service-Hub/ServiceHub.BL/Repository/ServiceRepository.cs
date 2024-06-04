using Microsoft.EntityFrameworkCore;
using ServiceHub.BL.Interface;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
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
        public  User FindEmail(string email)
        {
            return  db.Users.FirstOrDefault(a => a.Email == email);
        }


        public Worker Findemail(string email)
        {
            return   db.Workers.FirstOrDefault(a => a.Email == email);
        }

        public async Task SoftDelete(int id)
        {
            var data = await db.Workers.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                db.Workers.Remove(data);
            }
        }
    }
}
