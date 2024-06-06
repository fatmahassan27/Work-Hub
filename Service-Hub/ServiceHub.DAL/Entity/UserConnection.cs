﻿using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
