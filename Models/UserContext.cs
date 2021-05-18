using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplicationAuthorization.Models;

namespace WebApplicationAuthorization.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext() : base("DBConnection1")
        {
            
        }
    }
}