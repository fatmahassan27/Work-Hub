
using Microsoft.AspNetCore.Identity;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.Repositories;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Interfaces
{

    public class UnitWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private IJobRepo jobRepo;
        private ICityRepo cityRepo;
        private IDistrictRepo districtRepo;
        private IOrderRepo orderRepo;
        private IRateRepo rateRepo;


        public UnitWork(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IJobRepo JobRepo
        {
            get
            {
                return jobRepo ??= new JobRepo(db);
            }
        }

        public ICityRepo CityRepo
        {
            get
            {
             return cityRepo ??= new CityRepo(db);    
            }
        }

        public IOrderRepo OrderRepo
        {
            get
            {
                return orderRepo ??= new OrderRepo(db);
            }
        }

        public IDistrictRepo DistrictRepo => districtRepo ?? new DistrictRepo(db);

        public IRateRepo RateRepo
        {
            get
            {
                return rateRepo ??= new RateRepo(db,userManager);
            }
        }
           

        public async Task<int> saveAsync()
        {
            return await db.SaveChangesAsync();
        }

    }
}
