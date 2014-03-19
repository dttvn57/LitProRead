using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitProRead.BusinessObjects
{
    public class StudentAccomplishmentBO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentStatus { get; set; }
        public bool StudentActive { get; set; }
        public DateTime? StudentActiveDate { get; set; }
        public int? DaysofSvc { get; set; }
        public string YMD { get; set; }
        public DateTime? AccomplishDate { get; set; }
        public string Accomplishment { get; set; }
        public string Comment { get; set; }
    }
}