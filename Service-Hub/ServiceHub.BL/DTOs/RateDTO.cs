
using System.ComponentModel.DataAnnotations;

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
