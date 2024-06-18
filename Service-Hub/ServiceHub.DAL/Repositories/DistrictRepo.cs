using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{
    public class DistrictRepo : GenericRepo<District> , IDistrictRepo
    {
        private readonly ApplicationDbContext db;

        public DistrictRepo(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<IEnumerable<District>> GetAllDistrictsByCityId(int cityId)
        {
            return await db.Districts.Where(d=>d.CityId==cityId).ToListAsync();
        }
    }
}
