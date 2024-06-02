using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Entity
{
	public class Worker
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(10), MinLength(3), DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreationDate { get; set; } = DateTime.Now.Date;

        public int JobId { get; set; }
        public int DistrictId { get; set; }
        [Range(1, 5)]
	    public int Rating { get; set; } //average of ratings	

        [ForeignKey("JobId")]
        public Job? Job { get; set; }
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
        public List<ChatMessage>? ChatMessages { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<Rate>? Ratings { get; set; }
        public List<Order>? Orders { get; set; }


    }
}
