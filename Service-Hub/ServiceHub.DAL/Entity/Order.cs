using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHub.DAL.Enum;

namespace ServiceHub.DAL.Entity
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;


    }
}
