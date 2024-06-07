using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.DAL.Entity
{
	public class District
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int CityId {  get; set; }
		[ForeignKey("CityId")]
		public City City { get; set; }
    }
}
