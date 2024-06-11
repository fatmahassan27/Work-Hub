
using Microsoft.AspNetCore.Mvc;
using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task  CreateOrderAsync(int userId, int wokerId);
        Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId);
        Task<IEnumerable<Order>> GetAllOrdersByWorkerId(int workerId);
        Task UpdateOrderAsync(int id,int status);
       


    }
}
