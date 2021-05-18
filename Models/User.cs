using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationAuthorization.Models
{
    public class User
    {
        // [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
    }
}