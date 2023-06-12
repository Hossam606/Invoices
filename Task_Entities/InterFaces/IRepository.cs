using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Entities.Entities;

namespace Task_Entities.InterFaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public Task<T> GetById(int id);
        public Task Add(T entity);
        public Task UpdateAsync(int id, T entity);
        public Task Delete(int id);
    }
}
