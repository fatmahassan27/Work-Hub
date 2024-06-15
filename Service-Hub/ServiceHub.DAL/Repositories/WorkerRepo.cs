using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Repositories
{
    public class WorkerRepo : IWorkerRepo
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public WorkerRepo(ApplicationDbContext db , UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<double> getAverageWorkerRating(int workerId)
        {
         
                var TotalRatings = await db.Ratings.Where(x=>x.ToUserId==workerId).CountAsync();
                var SumRatings = await db.Ratings.Where(x => x.ToUserId == workerId).SumAsync(x=>x.Value);
                double avrg= (double)SumRatings/TotalRatings;
                return avrg;
        }
    }
}  
