using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaVod.Models
{
    public class Worker
    {
        public string Firstname { get; set; }
        public string Fathername { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Login{ get; set; }
        public string Password { get; set; }  
        public int RoleId { get; set; }
    }
}
