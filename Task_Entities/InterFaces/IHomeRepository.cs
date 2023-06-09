﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Entities.Entities;

namespace Task_Entities.InterFaces
{
    public interface IHomeRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public Task<IEnumerable<T>> GetUsers(string sTerm = "");
        public Task<T> GetById(int id);
        public Task Add(T entity);
        public Task UpdateAsync(int id, T entity);
        public Task Delete(int id);

    }
}