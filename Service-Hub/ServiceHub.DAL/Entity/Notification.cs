using ServiceHub.DAL.Enum;
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

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int? WorkerId { get; set; }
       
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }

    }
}
