using System;
using System.Data.SqlTypes;

namespace TechnoDapperBlazor.Models
{
    public class EmpService
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; } = (DateTime)SqlDateTime.MinValue;
        public string Shift { get; set; }
        public string Manager { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime TerminationDate { get; set; }
        public string Photo { get; set; }

        public string GetEmployeeStatus()
        {
            if (IsDateTimeSet(TerminationDate))
            {
                return "Inactive";
            }
            else
            {
                return "Active";
            }
        }

        public static bool IsDateTimeSet(DateTime dateTime)
        {
            if (dateTime == new DateTime(0001, 1, 1))
            {
                return false;
            }

            return (dateTime == DateTime.MinValue || dateTime == (DateTime)SqlDateTime.MinValue || dateTime == new DateTime(1900, 1, 1)) ? false : true;

        }
    }
}
