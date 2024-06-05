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
    public class CustomOrderRepo
    {
        private readonly ApplicationDbContext db;

        public CustomOrderRepo(ApplicationDbContext _db)
        {
            db = _db;
        }

        //public async Task<>


        public async Task<List<Order>> GetAllOrdersByUserId(int userId)
        {
            return await db.Orders.Where(o=> o.UserId == userId).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrdersByWorkerId(int workerId)
        {
            return await db.Orders.Where(o => o.WorkerId == workerId).ToListAsync();
        }

    }
}
