using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Dapper_X_Swagger_.Models
{
    public class Users
    {
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime datecreated { get; set; }
        public DateTime dateupdated { get; set; }
    }
}
