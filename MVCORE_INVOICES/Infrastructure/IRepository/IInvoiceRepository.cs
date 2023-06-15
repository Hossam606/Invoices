using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IInvoiceRepository 
    {
        public IEnumerable<Invoice> GetAll();
        public Task<IEnumerable<Invoice>> GetUsers(string sTerm = "");
        public Task<Invoice> GetById(int id);
        public Task Add(Invoice entity);
        public Task UpdateAsync(int id, Invoice entity);
        public Task Delete(int id);
    }
}
