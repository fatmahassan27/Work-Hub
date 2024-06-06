using ServiceHub.DAL.Enum;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Entity
{
	public class Notification
	{
		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; } = 0;
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; } = false;
		public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public string? WorkerId { get; set; }
       
        [ForeignKey("WorkerId")]
        public ApplicationUser? Worker { get; set; }

    }
}
