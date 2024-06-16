using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{

    public class CityRepo : GenericRepo<City>, ICityRepo
    {
        private readonly ApplicationDbContext db;

        public CityRepo(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

    }


}