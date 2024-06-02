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
	public class Job
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public List<Worker> Workers { get; set; }



    }
}
