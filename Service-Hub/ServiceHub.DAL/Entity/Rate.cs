using ServiceHub.DAL.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.DAL.Entity
{
	public class Rate
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, 5)]
        public int Value { get; set; }
        public string? Review { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [ForeignKey("FromUserId")]
        public ApplicationUser Rater { get; set; }

        [ForeignKey("ToUserId")]
        public ApplicationUser Rated { get; set; }
    }
}
