using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ServiceHub.DAL.Helper;

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
        public ICollection<ApplicationUser> Workers { get; set; } = new HashSet<ApplicationUser>();
    }
}
