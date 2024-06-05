using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ServiceHub.BL.Interface;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Repository
{
    public class BaseRepository<T> : IBaseRepo<T> where T : class
    {
        protected readonly ApplicationDbContext db;

        public BaseRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Create(T obj)
        {
            await db.Set<T>().AddAsync(obj);
        }

        public async Task Delete(int id)
        {
            var data = await db.Set<T>().FindAsync(id);
            if (data != null)
            {
                db.Set<T>().Remove(data);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(int id, T obj)
        {
          //  var data = await db.Set<T>().FindAsync(id);
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return obj;

        }
    }
}
