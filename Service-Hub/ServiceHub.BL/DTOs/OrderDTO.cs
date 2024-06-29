﻿using ServiceHub.DAL.Enums;
namespace ServiceHub.BL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public int WorkerId { get; set; }
        public WorkerDTO worker { get; set; }
        public WorkerDTO user { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
