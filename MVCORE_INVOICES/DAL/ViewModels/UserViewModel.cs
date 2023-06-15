using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<User>? Users { get; set; }
        public string STerm { get; set; } = "";
    }
}
