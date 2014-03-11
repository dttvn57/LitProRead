using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitProRead.ViewModels
{
    public class ReportsViewModel
    {
        public virtual ICollection<String> ReportStudents { get; set; }
        public virtual ICollection<String> ReportStudentsStatus { get; set; }
    }
}