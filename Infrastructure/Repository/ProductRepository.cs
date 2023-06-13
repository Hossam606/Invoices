using DAL.Context;
using DAL.Entities;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly DBContext_Invoices _db;
        public ProductRepository(DBContext_Invoices db)
        {
            _db = db;
        }
        public IEnumerable<Product> GetAll()
        {
            var result = _db.Set<Product>().ToList();
            return result;
        }
        public async Task Add(Product entity)
        {
            await _db.Set<Product>().AddAsync(entity);
            await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<Product>> GetUsers(string sTerm = "")
        {
            sTerm = sTerm.ToLower();

            // search by username 

            IEnumerable<User> users = await (from user in _db.Users
                                             where string.IsNullOrWhiteSpace(sTerm) || (user != null && user.UserName.ToLower().StartsWith(sTerm))
                                             select new User
                                             {
                                                 Id = user.Id,
                                                 FullName = user.FullName,
                                                 UserName = user.UserName,
                                                 Email = user.Email,
                                                 Password = user.Password,
                                                 Phone = user.Phone,
                                             }
                         ).ToListAsync();
            return (IEnumerable<Product>)users;

        }
        public async Task<Product> GetById(int id)
        {
            var result = await _db.Set<Product>().FindAsync(id);
            return result;
        }
        public async Task Delete(int id)
        {

            var result = await _db.Set<Product>().FindAsync(id);
            _db.Set<Product>().Remove(result);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Product entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }

    }
}
