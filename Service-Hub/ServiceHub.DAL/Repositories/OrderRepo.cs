using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.Enums;
namespace ServiceHub.DAL.Repositories
{
    public class OrderRepo :  GenericRepo<Order> , IOrderRepo
    {
        private readonly ApplicationDbContext db;

        public OrderRepo(ApplicationDbContext db) :base(db)
        {
            this.db = db;
        }

        public async Task CreateOrderAsync( int userId, int wokerId)
        {
            Order order = new Order()
            {
                UserId = userId,
                WorkerId = wokerId,
                Status = OrderStatus.New
            };
            await db.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId)
        {
            var orders = await db.Orders.Where(a=>a.UserId==userId).ToListAsync();
            return (orders);
        }

        public  async Task<IEnumerable<Order>> GetAllOrdersByWorkerId(int workerId)
        {
            var orders = await db.Orders.Where(a => a.WorkerId == workerId).ToListAsync();
            return (orders);
        }

        public async Task UpdateOrderAsync(int id, int status)
        {
            var order = await db.Orders.FindAsync(id);
            order.Status = (OrderStatus)status;
            db.Orders.Update(order);
        }
    }
}
