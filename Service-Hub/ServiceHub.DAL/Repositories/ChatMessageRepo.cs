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

        public ChatMessageRepo(ApplicationDbContext db ):base(db)
        {

        }
    }
}
