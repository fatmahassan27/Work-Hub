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
        BaseRepository<ChatMessage> chatMessageRepo;
        BaseRepository<User> userRepo;
        ServiceRepository serviceRepository;
        BaseRepository<Worker> workerRepo;
        public UnitWork(ApplicationDbContext db)
        {
            this.db = db;
        }
        public BaseRepository<ChatMessage> ChatMessageRepo
        {
            get
            {
                if(chatMessageRepo == null)
                {
                    chatMessageRepo = new BaseRepository<ChatMessage>(db);
                }
                return chatMessageRepo;
            }
        }
       
        public BaseRepository<User> UserRepo
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new BaseRepository<User>(db);
                }
                return userRepo;
            }
        }
        public BaseRepository<Worker> WorkerRepo
        {
            get
            {
                if (workerRepo == null)
                {
                    workerRepo = new BaseRepository<Worker>(db);
                }
                return workerRepo;
            }
        }
        public ServiceRepository ServiceRepository
        {
            get
            {
                if (serviceRepository == null)
                {
                    serviceRepository = new ServiceRepository(db);
                }
                return serviceRepository;
            }
        }
        public void saveChanges()
        {
            db.SaveChanges();
        }

    }
}
