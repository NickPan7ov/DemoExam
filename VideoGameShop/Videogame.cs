using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameShop
{
    public class Videogame
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int provider { get; set; }
        public int category { get; set; }
        public Image picture { get; set; }
        public int cost { get; set; }
        public int quantity { get; set; }
    }
}
