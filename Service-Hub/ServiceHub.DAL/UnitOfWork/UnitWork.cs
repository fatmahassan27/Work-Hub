
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Interfaces;
using ServiceHub.DAL.Repositories;

namespace ServiceHub.BL.UnitOfWork
{

    public class UnitWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        private IJobRepo jobRepo;
        //

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
        public async Task<int> saveAsync()
        {
            return await db.SaveChangesAsync();
        }

    }
}
