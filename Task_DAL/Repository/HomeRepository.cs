﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class HomeRepository<T>:IHomeRepository<T> where T : class
    {
        private readonly DbTaskContext _db;
        public HomeRepository(DbTaskContext db)
        {
                _db= db;
        }
        public  IEnumerable<T> GetAll()
        {
            var result = _db.Set<T>().ToList();
            return result;
        }
        public async Task Add(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<T>> GetUsers(string sTerm = "")
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

             
             

            return (IEnumerable<T>)users;

        }
        public async Task<T> GetById(int id)
        {
            var result = await _db.Set<T>().FindAsync(id);
            return result;
        }
        public async Task Delete(int id)
        {

            var result = await _db.Set<T>().FindAsync(id);
            _db.Set<T>().Remove(result);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, T entity)
        {
            //EntityEntry entityEntry = _context.Entry<T>(entity);
            //entityEntry.State = EntityState.Modified;
            //var x= entity;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            //return entity;
        }

        public bool CheckIfAdminTo_SafeUserForFirstTime()
        {
            List<User> users = _db.Users.ToList();
            return users.Count == 0 ? true : false;
        }
    }
}
