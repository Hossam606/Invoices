using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_DAL.Data;
using Task_Entities.Entities;
using Task_Entities.InterFaces;

namespace Task_DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbTaskContext _context;
        public Repository(DbTaskContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }


        public async Task Delete(int id)
        {

            var result = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(result);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            var result = _context.Set<T>().ToList();
            return result;
        }

        public async Task<T> GetById(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }
      

        public async Task UpdateAsync(int id, T entity)
        {
            //EntityEntry entityEntry = _context.Entry<T>(entity);
            //entityEntry.State = EntityState.Modified;
            //var x= entity;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            //return entity;
        }
    }
}
