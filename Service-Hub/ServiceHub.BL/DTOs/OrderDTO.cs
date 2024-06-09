using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Enums;
namespace ServiceHub.BL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }


        public static OrderDTO FromOrder(Order order)
        {
            return new OrderDTO()
            {
                Id = order.Id,
                Status = order.Status,
                CreatedDate = order.CreatedDate
            };
        }
        public static Order ToOrder(OrderDTO orderDTO)
        {
            return new Order()
            {
                Id = orderDTO.Id,
                Status = orderDTO.Status,
                //CreatedDate = orderDTO.CreatedDate
            };
        }
    }
}
