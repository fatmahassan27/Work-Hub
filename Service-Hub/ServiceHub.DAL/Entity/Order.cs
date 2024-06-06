using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHub.DAL.Enum;
using ServiceHub.DAL.Helper;

namespace ServiceHub.DAL.Entity
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public string WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public ApplicationUser? Worker { get; set; }
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;


    }
}
