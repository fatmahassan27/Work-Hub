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
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int WorkerId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }


    }
}
