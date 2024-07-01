﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; } = "";
        public bool IsSeen { get; set; } 
        public DateTime createdDate { get; set; } 
    }
}
