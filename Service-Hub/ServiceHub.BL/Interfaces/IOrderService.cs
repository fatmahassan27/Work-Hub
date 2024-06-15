using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int userId, int workerId);

        Task<IEnumerable<OrderDTO>> GetAllOrdersByUserId(int userId);

        Task<IEnumerable<OrderDTO>> GetAllOrdersByWorkerId(int workerId);

        Task UpdateOrderAsync(int orderId, int newStatus);
    }
}
