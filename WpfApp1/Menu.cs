using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MenuPage
    {
        public List<MenuItem> Items { get; set; }
        public List<MenuItem> Wines { get; set; }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
