using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTO
{
    public class WorkerDTO2
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreationDate { get; set; }
        public int JobId { get; set; }
        public int DistrictId { get; set; }
        public int Rating { get; set; } //average of ratings	


    }
}
