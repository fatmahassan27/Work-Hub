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
    public class JobRepo : GenericRepo<Job>, IJobRepo
    {
        private readonly ApplicationDbContext db;

        public JobRepo(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
 
    }
}
