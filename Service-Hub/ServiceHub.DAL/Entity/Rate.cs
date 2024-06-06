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
	public class Rate
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, 5)]
        public int Value { get; set; }
        public string? Review { get; set; }
        public string WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [NotMapped]
        public ApplicationUser? Worker { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        [NotMapped]
        public ApplicationUser? User { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
