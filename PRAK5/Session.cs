using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAK5
{
    public static class Session
    {
        public static string Login { get; set; }
        public static string PasswordHash { get; set; }
        public static int AccessLevel { get; set; } // Уровень доступа
    }

}
