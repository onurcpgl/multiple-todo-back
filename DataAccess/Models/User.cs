﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }  
        public string lastName { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public ICollection<Todo>? Todos { get; set; }
        public ICollection<Team>? Teams { get; set; }
        public Team? teamOwner { get; set; }  
        public Media? media { get; set; }
        public string? RefreshToken { get; set; }
        public string? teamImage { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        
    }
}
