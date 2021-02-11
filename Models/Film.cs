using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Film
    {
        public int FilmID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Review { get; set; }
        public int RunTimeMinutes { get; set; }
        public int BudgetDollars { get; set; }
        public int BoxOfficeDollars { get; set; }
        public int OscarNominations { get; set; }
        public int OscarWins { get; set; }
    }
}
