using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTOs
{
    public class RateDTO
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Value { get; set; }
        public string? Review { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }
}
