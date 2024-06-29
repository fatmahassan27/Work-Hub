using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
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
        public async Task<IEnumerable<Rate>> GetAllRatingsByWorkerId(int workerId)
        {
            return  await db.Ratings.Where(a=>a.ToUserId==workerId).ToListAsync();
        }
        public  async Task AddRate(Rate rate)
        {
            await db.Ratings.AddAsync(rate);
        }

        public async Task<double> getAverageWorkerRating(int workerId)
        {
           

            var TotalRatings = await db.Ratings.Where(x => x.ToUserId == workerId).CountAsync();
            var SumRatings = await db.Ratings.Where(x => x.ToUserId == workerId).SumAsync(x => x.Value);
            double avrg = 0;

            if (TotalRatings==0)
            {
                avrg = 3;
            }else
            {
                avrg = (SumRatings * 1.0) / TotalRatings;

            }

            return avrg;

        }

    }
}
