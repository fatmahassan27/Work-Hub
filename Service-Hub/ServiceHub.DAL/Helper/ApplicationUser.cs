using Microsoft.AspNetCore.Identity;
using ServiceHub.DAL.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.DAL.Helper
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            ChatMessages = [];
            Notifications = [];
            Ratings = [];
            Orders = [];
        }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreationDate { get; set; } = DateTime.Now.Date;
        public int? JobId { get; set; }
        public int? DistrictId { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; } //average of ratings

        [ForeignKey("DistrictId")]
        public District? District { get; set; }
        public Job? Job { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Rate>? Ratings { get; set; } 
        public ICollection<Order>? Orders { get; set; } 


    }
}
