
using ServiceHub.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.BL.DTOs
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public JobDTO job { get; set; }
        public DistrictDTO district { get; set; }

        public int? Rating { get; set; } = 3;//average of ratings	


    }
}
