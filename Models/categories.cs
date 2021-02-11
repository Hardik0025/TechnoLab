using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        //public byte[] Picture { get; set; }
        //public ICollection<Products> Product { get; set; }
    }
}
