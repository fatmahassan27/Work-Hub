using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Entity
{
    public class WorkerConnection
    {
        
        public string ConnectionId { get; set; }

        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public ApplicationUser Worker { get; set; }



    }
}
