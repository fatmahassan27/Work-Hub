using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string? FullName { get; set; }
        [EmailAddress]
        public bool? IsDeleted { get; set; } = false;

        public string? Email { get; set; }
        [MaxLength(10), MinLength(3), DataType(DataType.Password)]
        public string? Password { get; set; }
        public int JobId { get; set; }
        public int DistrictId { get; set; }
        public int Rating { get; set; } //average of ratings	


    }
}
