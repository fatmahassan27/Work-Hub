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

        public RateRepo(ApplicationDbContext db,UserManager<ApplicationUser> userManager) :base(db)
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

        public async Task<double> getAverageWorkerRating(int workerId)
        {
            double average = await db.Ratings
                           .Where(a => a.ToUserId == workerId)
                           .AverageAsync(a => a.Value);

            var worker =  await userManager.FindByIdAsync(workerId.ToString());
            worker.Rating= (int)average;
            userManager.UpdateAsync(worker);
            return average;
        }


    }
}
