using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ServiceHub.DAL.Enum;
using ServiceHub.DAL.Helper;

namespace ServiceHub.DAL.Entity
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkerId { get; set; }
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("WorkerId")]
        public ApplicationUser Worker { get; set; }

        public Order()
        {
            //Status = OrderStatus.New;
            CreatedDate = DateTime.Now;
        }


    }
}
