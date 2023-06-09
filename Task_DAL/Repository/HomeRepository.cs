using Microsoft.EntityFrameworkCore;
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
    public class HomeRepository:IHomeRepository
    {
        private readonly DbTaskContext _db;
        public HomeRepository(DbTaskContext db)
        {
                _db= db;
        }
        public async Task<IEnumerable<User>> GetBooks(string sTerm = "")
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

             
             

            return users;

        }

         
    }
}
