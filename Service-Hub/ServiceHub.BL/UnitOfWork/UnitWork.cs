﻿using ServiceHub.BL.Repository;
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
        
        BaseRepository<Notification> notificationRepo;
        BaseRepository<Rate> rateRepo;
        BaseRepository<Job> jobRepo;
        BaseRepository<Order> orderRepo;
        CustomWorkerRepo customWorkerRepo;
        CustomOrderRepo customOrderRepo;

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

        public BaseRepository<Notification> NotificationRepo
        {
            get
            {
                if (notificationRepo == null)
                {
                    notificationRepo = new BaseRepository<Notification>(db);
                }
                return notificationRepo;
            }
        }
        public BaseRepository<Rate> RateRepo
        {
            get
            {
                if (rateRepo == null)
                {
                    rateRepo = new BaseRepository<Rate>(db);
                }
                return rateRepo;
            }
        }
        public BaseRepository<Job> JobRepo
        {
            get
            {
                if (jobRepo == null)
                {
                    jobRepo = new BaseRepository<Job>(db);
                }
                return jobRepo;
            }
        }
        public BaseRepository<Order> OrderRepo
        {
            get
            {
                if (orderRepo == null)
                {
                    orderRepo = new BaseRepository<Order>(db);
                }
                return orderRepo;
            }
        }
        public CustomWorkerRepo CustomWorkerRepo
        {
            get
            {
                if (customWorkerRepo == null)
                {
                    customWorkerRepo = new CustomWorkerRepo(db);
                }
                return customWorkerRepo;
            }
        }
        public CustomOrderRepo CustomOrderRepo
        {
            get
            {
                if (customOrderRepo == null)
                {
                    customOrderRepo = new CustomOrderRepo(db);
                }
                return customOrderRepo;
            }
        }

        public void saveChanges()
        {
            db.SaveChanges();
        }

    }
}
