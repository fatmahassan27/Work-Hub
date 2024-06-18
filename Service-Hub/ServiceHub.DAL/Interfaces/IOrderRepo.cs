
using Microsoft.AspNetCore.Mvc;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;

namespace ServiceHub.DAL.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task  CreateOrderAsync(int userId, int workerId);
        Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId);
        Task<IEnumerable<Order>> GetAllOrdersByWorkerId(int workerId);
        Task UpdateOrderAsync(int id,int status);
       


    }
}
