using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ServiceHub.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Interfaces
{
    public interface IChatMessageService
    {
        Task CreateMessage(ChatDTO obj);

        Task<IEnumerable<ChatDTO>> GetAllMessages(int senderId,int receiverId);


        
    }
}
