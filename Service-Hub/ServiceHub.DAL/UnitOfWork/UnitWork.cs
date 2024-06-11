
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.Repositories;

namespace ServiceHub.BL.UnitOfWork
{

    public class UnitWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        private IJobRepo jobRepo;
        private ICityRepo cityRepo;
        private IOrderRepo orderRepo;

        public UnitWork(ApplicationDbContext db)
        {
            this.db = db;
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

        public async Task<int> saveAsync()
        {
            return await db.SaveChangesAsync();
        }

    }
}
