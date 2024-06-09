
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDbContext db;

        public GenericRepo(ApplicationDbContext db)
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
        public async Task UpdateAsync(int id, T obj)
        {
            //var data = await db.Set<T>().FindAsync(id);
            db.Entry(obj).State = EntityState.Modified;
            //return obj;
        }

    }
}
