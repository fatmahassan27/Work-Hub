using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{

    public class CityRepo : GenericRepo<City>, ICityRepo
    {
        private readonly ApplicationDbContext db;

        public CityRepo(ApplicationDbContext db) : base(db)
        {
          this.db = db;
        }

    }


}