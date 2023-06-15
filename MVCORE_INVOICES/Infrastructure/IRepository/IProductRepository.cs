using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public Task<IEnumerable<Product>> GetUsers(string sTerm = "");
        public Task<Product> GetById(int id);
        public Task Add(Product entity);
        public Task UpdateAsync(int id, Product entity);
        public Task Delete(int id);

    }
}
