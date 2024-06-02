using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ServiceHub.BL.Interface;
using ServiceHub.DAL.DataBase;
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
        #region const
        public BaseRepository(ApplicationDbContext db) 
        {
            this.db = db;
        }
        #endregion
        #region methods
        public async Task Create(T obj)
        {
            await db.Set<T>().AddAsync(obj);
          //  await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var data = await db.Set<T>().FindAsync(id);
            db.Set<T>().Remove(data);
            //await db.SaveChangesAsync();
        }

        public   async Task Delete(T obj)
        {
            db.Set<T>().Remove(obj);
           // await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var data =await db.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetById(int id)
        {
            var data = await db.Set<T>().FindAsync(id);
            return data;
        }

        public  async Task Update(T obj)
        {
            db.Set<T>().Update(obj);
           // await db.SaveChangesAsync();
        }
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
