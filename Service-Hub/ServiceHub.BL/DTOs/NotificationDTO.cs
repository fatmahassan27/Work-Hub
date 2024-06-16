using ServiceHub.DAL.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.BL.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
