using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Menu
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public IEnumerable<Item> Items{ get;  set;}
    }
}
