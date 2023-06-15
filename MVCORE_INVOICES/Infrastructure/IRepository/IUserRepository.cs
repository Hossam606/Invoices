using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public Task<User> GetById(int id);
        public Task Add(User entity);
        public Task UpdateAsync(int id, User entity);
        public Task Delete(int id);
        public IEnumerable<User>? GetUsers(string sTerm = "");
        public bool CheckIfAdminTo_SafeUserForFirstTime();

    }
}
