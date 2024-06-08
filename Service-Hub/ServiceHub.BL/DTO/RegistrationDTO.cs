using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTO
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
