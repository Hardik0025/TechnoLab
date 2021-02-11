using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Author
    {
        public int author_id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public int phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string email_address { get; set; }
    }
}
