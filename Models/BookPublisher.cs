using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class BookPublisher
    {
        public string book_id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int pub_id { get; set; }
        public double price { get; set; }
        public double advance { get; set; }
        public int royalty { get; set; }
        public int ytd_sales { get; set; }
        public string notes { get; set; }
        public DateTime published_date { get; set; }
        public string publisher_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
}
