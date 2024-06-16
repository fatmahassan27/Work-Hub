using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{
    public class JobRepo : GenericRepo<Job> , IJobRepo
    {
        private readonly ApplicationDbContext db;

        public JobRepo(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
 
    }
}
