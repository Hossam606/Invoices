using DAL.Context;
using DAL.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DBContext_Invoices _db;
        public InvoiceRepository(DBContext_Invoices db)
        {
            _db = db;
        }
        public IEnumerable<Invoice> GetAll()
        {
            var result = _db.Set<Invoice>().ToList();
            return result;
        }
        public async Task Add(Invoice entity)
        {
            await _db.Set<Invoice>().AddAsync(entity);
            await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<Invoice>> GetUsers(string sTerm = "")
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




            return (IEnumerable<Invoice>)users;

        }
        public async Task<Invoice> GetById(int id)
        {
            var result = await _db.Set<Invoice>().FindAsync(id);
            return result;
        }
        public async Task Delete(int id)
        {

            var result = await _db.Set<Invoice>().FindAsync(id);
            _db.Set<Invoice>().Remove(result);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Invoice entity)
        {
            //EntityEntry entityEntry = _context.Entry<T>(entity);
            //entityEntry.State = EntityState.Modified;
            //var x= entity;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            //return entity;
        }

    }
}
