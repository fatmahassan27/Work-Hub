using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Repositories
{
    public class ChatMessageRepo :GenericRepo<ChatMessage>,IChatMessageRepo
    {
        private readonly ApplicationDbContext db;

        public ChatMessageRepo(ApplicationDbContext db ):base(db)
        {
            this.db = db;
        }

        public  async Task<IEnumerable<ChatMessage>> GetAllMessageByAnId(int id)
        {
             return await db.ChatMessage.Where(a => a.SenderId == id || a.ReceiverId == id).ToListAsync();
        }
    }
}
