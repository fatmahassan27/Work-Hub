using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateOrderAsync(int userId, int workerId)
        {
            await unitOfWork.OrderRepo.CreateOrderAsync(userId, workerId);

            await unitOfWork.saveAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersByUserId(int userId)
        {
            var orders= await unitOfWork.OrderRepo.GetAllOrdersByUserId(userId);
            var ordersDTO = mapper.Map<IEnumerable<OrderDTO>>(orders);
            return ordersDTO; 
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersByWorkerId(int workerId)
        {
            var orders = await unitOfWork.OrderRepo.GetAllOrdersByWorkerId(workerId);
            var ordersDTO = mapper.Map<IEnumerable<OrderDTO>>(orders);
            return ordersDTO;
        }

        public async Task UpdateOrderAsync(int orderId , int newStatus)
        {
            await unitOfWork.OrderRepo.UpdateOrderAsync(orderId, newStatus);
            await unitOfWork.saveAsync();
        }
    }
}
