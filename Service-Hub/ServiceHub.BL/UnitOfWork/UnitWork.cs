using ServiceHub.BL.Repository;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.UnitOfWork
{

    public class UnitWork
    {
        private readonly ApplicationDbContext db;
        BaseRepository<ChatMessage> chatMessage;
        public UnitWork(ApplicationDbContext db)
        {
            this.db = db;
        }
        public BaseRepository<ChatMessage> ChatMessage 
        {
            get
            {
                if(chatMessage==null)
                {
                    chatMessage = new BaseRepository<ChatMessage>(db);
                }
                return chatMessage;
            }
        }
        public void saveChanges()
        {
            db.SaveChanges();
        }
    }
}
