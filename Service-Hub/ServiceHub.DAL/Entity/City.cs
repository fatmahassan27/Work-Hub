using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.DAL.Entity
{
	public class City
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }
		[Required]
		public string Name{ get; set; }
		public ICollection<District> Districtlist { get; set; } = new HashSet<District>();
    }
}
