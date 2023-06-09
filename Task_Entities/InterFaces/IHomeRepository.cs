using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Entities.Entities;

namespace Task_Entities.InterFaces
{
    public interface IHomeRepository
    {
        public Task<IEnumerable<User>> GetBooks(string sTerm = "");
        
    }
}
