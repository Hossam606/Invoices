using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MVCORE_INVOICES.Helper
{
    public class InvoicesHelper
    {
        public static DbContextOptions<DBContext_Invoices> GetDbContextOptions()
        {
            IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
            string _connectionString = _configuration.GetConnectionString("DatabaseConnection"); ;
            DbContextOptionsBuilder<DBContext_Invoices> _optionsBuilder = new DbContextOptionsBuilder<DBContext_Invoices>();
            _optionsBuilder.UseSqlServer(_connectionString);
            return _optionsBuilder.Options;
        }
    }
}
