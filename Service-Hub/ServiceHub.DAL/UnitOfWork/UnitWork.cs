using ServiceHub.BL.Interface;
using ServiceHub.BL.Repository;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Interface;
using ServiceHub.DAL.Repository;
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
        private ApplicationDbContext db;
        private OrderRepo customOrderRepo;
        private GenericRepository<Order> orderRepo;
        private GenericRepository<ApplicationUser> appUsers;
        private ServiceRepository serviceRepo;

        //public IGenericRepo<Order> OrderRepo
        //{
        //    get
        //    {
        //        if (orderRepo == null)
        //        {
        //            orderRepo = new GenericRepository<Order>(db);
        //        }
        //        return orderRepo;
        //    }
        //}

        //public IOrderRepo CustomOrderRepo
        //{
        //    get
        //    {
        //        if (customOrderRepo == null)
        //        {
        //            customOrderRepo = new GenericRepository<Order>(db);
        //        }
        //        return customOrderRepo;
        //    }
        //}

        public IGenericRepo<ApplicationUser> AppUsers
        {
            get
            {
                if (appUsers == null)
                {
                    appUsers = new GenericRepository<ApplicationUser>(db);
                }
                return appUsers;
            }
        }
        public ServiceRepository ServiceRepository
        {
            get
            {
                if (serviceRepo == null)
                {
                    serviceRepo = new ServiceRepository(db);
                }
                return serviceRepo;
            }
        }

        //BaseRepository<ChatMessage> chatMessageRepo;
        //BaseRepository<ApplicationUser> appUserRepo;
        //ServiceRepository serviceRepository;
        //BaseRepository<Notification> notificationRepo;
        //BaseRepository<Rate> rateRepo;
        //BaseRepository<Job> jobRepo;

        //public BaseRepository<ChatMessage> ChatMessageRepo
        //{
        //    get
        //    {
        //        if (chatMessageRepo == null)
        //        {
        //            chatMessageRepo = new BaseRepository<ChatMessage>(db);
        //        }
        //        return chatMessageRepo;
        //    }
        //}

        //public BaseRepository<ApplicationUser> AppUserRepo
        //{
        //    get
        //    {
        //        if (appUserRepo == null)
        //        {
        //            appUserRepo = new BaseRepository<ApplicationUser>(db);
        //        }
        //        return appUserRepo;
        //    }
        //}



        //public BaseRepository<Notification> NotificationRepo
        //{
        //    get
        //    {
        //        if (notificationRepo == null)
        //        {
        //            notificationRepo = new BaseRepository<Notification>(db);
        //        }
        //        return notificationRepo;
        //    }
        //}
        //public BaseRepository<Rate> RateRepo
        //{
        //    get
        //    {
        //        if (rateRepo == null)
        //        {
        //            rateRepo = new BaseRepository<Rate>(db);
        //        }
        //        return rateRepo;
        //    }
        //}
        //public BaseRepository<Job> JobRepo
        //{
        //    get
        //    {
        //        if (jobRepo == null)
        //        {
        //            jobRepo = new BaseRepository<Job>(db);
        //        }
        //        return jobRepo;
        //    }
        //}
        //public BaseRepository<Order> OrderRepo
        //{
        //    get
        //    {
        //        if (orderRepo == null)
        //        {
        //            orderRepo = new BaseRepository<Order>(db);
        //        }
        //        return orderRepo;
        //    }
        //}


        public async Task<int> saveChanges()
        {
            return  db.SaveChanges();
        }


     
    }
}
