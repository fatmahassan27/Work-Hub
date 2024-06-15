using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{
    public class RateRepo :GenericRepo<Rate> ,IRateRepo
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public RateRepo(ApplicationDbContext db) :base(db)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IEnumerable<Rate> GetAllRatingsByWorkerId(int workerId)
        {
            return  db.Ratings.Where(a=>a.ToUserId==workerId).ToList();
        }
        public  async Task AddRate(Rate rate)
        {
            await db.Ratings.AddAsync(rate);
        }



    }
}
