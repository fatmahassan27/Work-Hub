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
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDbContext db;

        public GenericRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(T obj)
        {
            await db.Set<T>().AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await db.Set<T>().FindAsync(id);
            if (data != null)
            {
                db.Set<T>().Remove(data);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

      

        public async Task<T> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(int id, T obj)
        {
          //  var data = await db.Set<T>().FindAsync(id);
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return obj;

        }
    }
}
