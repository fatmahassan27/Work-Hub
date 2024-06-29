using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.Enums;
using ServiceHub.DAL.GenericRepository;
using Microsoft.AspNetCore.Identity;
using ServiceHub.DAL.Helper;
namespace ServiceHub.DAL.Repositories
{
    public class OrderRepo :  GenericRepo<Order> , IOrderRepo
    {
        private readonly ApplicationDbContext db;


        public OrderRepo(ApplicationDbContext db,UserManager<ApplicationUser> userManager) :base(db)
        {
            this.db = db;
        }

        public async Task CreateOrderAsync( int userId, int workerId)
        {
            Order order = new Order()
            {
                UserId = userId,
                WorkerId = workerId,
                Status = OrderStatus.New
            };
            await db.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId)
        {
            var orders = await db.Orders
                     .Where(a => a.UserId == userId)
                     .Include(o => o.User).Include(x=>x.Worker) // Ensure correct include for related data
                     .ToListAsync();
            return orders;
            //var orders = await db.Orders.Where(a=>a.UserId==userId).Include("ApplicationUser").ToListAsync();
            //return (orders);
        }

        public  async Task<IEnumerable<Order>> GetAllOrdersByWorkerId(int workerId)
        {
            var orders = await db.Orders
                    .Where(a => a.WorkerId == workerId)
                    .Include(o => o.User).Include(x => x.Worker) // Ensure correct include for related data
                    .ToListAsync();
            return orders;
            //var orders = await db.Orders.Where(a => a.WorkerId == workerId).Include("ApplicationUser").ToListAsync();
            //return (orders);
        }

        public async Task UpdateOrderAsync(int id, int status)
        {
            var order = await db.Orders.FindAsync(id);
            order.Status = (OrderStatus)status;
            db.Orders.Update(order);
        }
    }
}
