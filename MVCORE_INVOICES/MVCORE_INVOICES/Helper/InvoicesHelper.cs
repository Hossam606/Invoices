using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System;
using System.IO;
using DAL.Entities;

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
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static string[] GetUsers(User user)
        {
            string[] userArray = new string[5];
            userArray[0] = user.UserName;
            userArray[1] = user.FullName;
            userArray[2] = user.Email;
            userArray[3] = user.Phone;
            userArray[4] = user.IsAdmin?.ToString();

            return userArray;
        }

    }




}
