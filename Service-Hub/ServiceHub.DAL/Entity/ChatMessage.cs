using ServiceHub.DAL.Enum;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Entity
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = " ";
        public string WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public ApplicationUser Worker { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;


    }
}
