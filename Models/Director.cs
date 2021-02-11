using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Director
    {
        public int DirectorID { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DoB { get; set; }
        public DateTime DoD { get; set; }
        public string Gender { get; set; }
    }
}
