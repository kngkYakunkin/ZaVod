using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZaVod.Models;

namespace ZaVod.Middlewares
{
    public class SetAdminAccess
    {
        public string error_msg = "У вас нет прав!";
        private const int ADMIN_ROLE_ID = 1;
        public SetAdminAccess()
        {

        }
        public bool isAdmin(Worker worker)
        {
            if (worker.RoleId != ADMIN_ROLE_ID)
            {
                return false;
            }

            return true;
        }
    }
}
