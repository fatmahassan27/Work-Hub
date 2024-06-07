using ServiceHub.DAL.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceHub.DAL.Entity
{
    public class UserConnection
    {
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
