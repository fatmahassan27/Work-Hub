using Microsoft.AspNetCore.Identity;
using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Helper
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public List<Rate>? Ratings { get; set; }
        public ICollection<Order>? UserOrders { get; set; }
        public ICollection<Order>? WorkerOrders { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreationDate { get; set; } = DateTime.Now.Date;
        [Range(1, 5)]
        public int? Rating { get; set; } //average of ratings	
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job? Job { get; set; }
  

    }
}
