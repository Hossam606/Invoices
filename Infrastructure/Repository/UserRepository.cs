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

    public class UserRepository : IUserRepository
    {
        private readonly DBContext_Invoices _context;
        public UserRepository(DBContext_Invoices context)
        {
            _context = context;
        }
        public async Task Add(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {

            var result = await _context.Set<User>().FindAsync(id);
            _context.Set<User>().Remove(result);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<User> GetAll()
        {
            var result = _context.Set<User>().ToList();
            return result;
        }
        public async Task<User> GetById(int id)
        {
            var result = await _context.Set<User>().FindAsync(id);
            return result;
        }
        public async Task UpdateAsync(int id, User entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        public  IEnumerable<User> GetUsers(string sTerm = "")
        {
            IEnumerable<User> users;
            try
            {
                sTerm = sTerm.ToLower();

                var newusers = _context.Users.ToList();
                // search by username 
               users =  (from user in _context.Users
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
                             ).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }


            return (IEnumerable<User>)users;
        }
        public bool CheckIfAdminTo_SafeUserForFirstTime()
        {
            List<User> users = _context.Users.ToList();
            return users.Count == 0 ? true : false;
        }
    }

}
