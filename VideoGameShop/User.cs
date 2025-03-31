using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameShop
{
    public class User
    {
        public int iduser { get; set; }
        public string username { get; set; }
        public string fio { get; set; }
        public int roleid { get; set; }

        public const int ADMIN = 1;
        public const int MANAGER = 2;
        public const int SELLER = 3;
    }
}
