
using ServiceHub.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceHub.BL.DTOs
{
    public class RegistrationDTO
    {
        [Required]
        public string? FullName { get; set; }
        [Required,EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int DistrictId { get; set; }



    }
}
