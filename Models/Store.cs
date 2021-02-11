using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Store
    {
        public int store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
    }
}
