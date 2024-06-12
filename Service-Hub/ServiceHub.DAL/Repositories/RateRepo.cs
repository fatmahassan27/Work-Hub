﻿using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Repositories
{
    public class RateRepo :GenericRepo<Rate> ,IRateRepo
    {
        private readonly ApplicationDbContext db;

        public RateRepo(ApplicationDbContext db) :base(db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Rate>> GetAllRatingByWorkerId(int workerId)
        {
            return  await db.Ratings.Where(a=>a.ToUserId==workerId).ToListAsync();
        }
        public  async Task AddRate(int workerId,int userId,int value, string review)
        {
            Rate rate = new Rate()
            {
                ToUserId = workerId,
                FromUserId= userId,
                Value =value,
                Review=review
            };
            await db.Ratings.AddAsync(rate);
        }
    }
}
