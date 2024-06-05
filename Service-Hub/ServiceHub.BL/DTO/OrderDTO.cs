using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTO
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
                UserId = order.UserId,
                WorkerId = order.WorkerId,
                Status = order.Status,
                CreatedDate = order.CreatedDate
            };
        }

        public static Order ToOrder(OrderDTO orderDTO)
        {
            return new Order()
            {
                Id = orderDTO.Id,
                UserId = orderDTO.UserId,
                WorkerId = orderDTO.WorkerId,
                Status = orderDTO.Status,
                CreatedDate = orderDTO.CreatedDate
            };
        }
    }
}
