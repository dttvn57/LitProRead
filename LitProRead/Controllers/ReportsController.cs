using LitProRead.BusinessObjects;
using LitProRead.Models;
using LitProRead.Reports.DataSets;
using LitProRead.Reports.DataSets.StudentsMatchHistorybyDateRangeDataSetTableAdapters;
using LitProRead.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Controllers
{
    public class ReportsController : Controller
    {
        private static DateTime DefaultBeginDate()
        {
            return new DateTime(DateTime.Now.Year, 1, 01);
        }
        private static DateTime DefaultEndDate()
        {
            return new DateTime(DateTime.Now.Year, 12, 31);
        }


        Predicate<Nullable<System.DateTime>> Count40 = delegate(Nullable<System.DateTime> dob)
        {
            if (DateTime.Now.Year - ((DateTime)dob).Year >= 40)
                return true;
            else
                return false;
        };

        //
        // GET: /Reports/
        // CLLS
        public ActionResult CLLS(string paramsVal)
        {
            string reportType = "PDF"; 
            string beginDate = ""; 
            string endDate = "";
            string AdultLearnersFromPriorPeriod = "";
            string AdultLearnersBegan = "";
            string AdultLearnersReceived = "";
            string AdultLearnersLeft = "";
            string AdultLearnersRemaining = "";
            string CumulativeTotal = "";
            string AdultsReferredToOtherPrograms = "";
            string AdultLearnersAwaitingInstruction = "";
            string AdultLearnersInstructionHours = "";
            string BooksGiven = "";

            if (paramsVal != null)
            {
                char[] sep = { '!' };
                string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                // 1st
                string[] s1 = str[0].Split('=');
                reportType = s1[1];

                // 2nd
                if (str.Count() > 1)        // not all reports have a start date
                {
                    string[] date = str[1].Split('=');
                    beginDate = date[1];
                }

                // 3rd
                if (str.Count() > 2)        // not all reports have an end date
                {
                    string[] date = str[2].Split('=');
                    endDate = date[1];
                }

                // 4th
                if (str.Count() > 3)        
                {
                    s1 = str[3].Split('=');
                    AdultLearnersFromPriorPeriod = s1[1];
                }

                // 5th
                if (str.Count() > 4)
                {
                    s1 = str[4].Split('=');
                    AdultLearnersBegan = s1[1];
                }

                // 6th
                if (str.Count() > 5)
                {
                    s1 = str[5].Split('=');
                    AdultLearnersReceived = s1[1];
                }

                // 7th
                if (str.Count() > 6)
                {
                    s1 = str[6].Split('=');
                    AdultLearnersLeft = s1[1];
                }

                // 8th
                if (str.Count() > 7)
                {
                    s1 = str[7].Split('=');
                    AdultLearnersRemaining = s1[1];
                }

                // 9th
                if (str.Count() > 8)
                {
                    s1 = str[8].Split('=');
                    CumulativeTotal = s1[1];
                }

                // 10th
                if (str.Count() > 9)
                {
                    s1 = str[9].Split('=');
                    AdultsReferredToOtherPrograms = s1[1];
                }

                // 11th
                if (str.Count() > 10)
                {
                    s1 = str[10].Split('=');
                    AdultLearnersAwaitingInstruction = s1[1];
                }

                // 12th
                if (str.Count() > 11)
                {
                    s1 = str[11].Split('=');
                    AdultLearnersInstructionHours = s1[1];
                }

                // 13th
                if (str.Count() > 12)
                {
                    s1 = str[12].Split('=');
                    BooksGiven = s1[1];
                }
            }

            using (LitProReadEntities db = new LitProReadEntities())
            {
                DateTime date1 = DefaultBeginDate();
                if (beginDate != "")
                {
                    date1 = DateTime.ParseExact(beginDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(beginDate);
                }
                else
                {
                    if (DateTime.Now.Month > 6)
                        date1 = new DateTime(DateTime.Now.Year, 7, 1);
                    else
                        date1 = new DateTime(DateTime.Now.Year, 1, 1);
                }

                DateTime date2 = DefaultEndDate();
                if (endDate != "")
                {
                    date2 = DateTime.ParseExact(endDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(endDate);
                }
                else
                {
                    if (DateTime.Now.Month > 6)
                        date2 = new DateTime(DateTime.Now.Year, 12, 31);
                    else
                        date2 = new DateTime(DateTime.Now.Year, 6, 30);
                }

                DateTime today = DateTime.Today;        //12/24/2013

                int ubUnknown = 15;
                int lbUnknown = 1;
                DateTime minUnknown = today.AddYears(-ubUnknown);
                DateTime maxUnknown = today.AddYears(-lbUnknown);

                int ub10 = 19;
                int lb10 = 16;
                DateTime min10 = today.AddYears(-ub10); 
                DateTime max10 = today.AddYears(-lb10);

                int ub20 = 29;
                int lb20 = 20;
                DateTime min20 = today.AddYears(-ub20);
                DateTime max20 = today.AddYears(-lb20);

                int ub30 = 39;
                int lb30 = 30;
                DateTime min30 = today.AddYears(-ub30);
                DateTime max30 = today.AddYears(-lb30);

                int ub40 = 49;
                int lb40 = 40;
                DateTime min40 = today.AddYears(-ub40); //12/24/1964
                DateTime max40 = today.AddYears(-lb40); //12/24/1973

                int ub50 = 59;
                int lb50 = 50;
                DateTime min50 = today.AddYears(-ub50);
                DateTime max50 = today.AddYears(-lb50);

                int ub60 = 69;
                int lb60 = 60;
                DateTime min60 = today.AddYears(-ub60);
                DateTime max60 = today.AddYears(-lb60);

                int ub70 = 999;
                int lb70 = 70;
                DateTime min70 = today.AddYears(-ub70);
                DateTime max70 = today.AddYears(-lb70);

                var clls = from student in db.Students
                           //where student.FirstActive >= date1 && student.FirstActive <= date2
                          // group student by new { name1 = student.DOB != null ? System.Data.Objects.SqlClient.SqlFunctions.DateDiff("day", student.DOB, DateTime.Now) >= 365 * 70 
                           //                                                        && student.FirstActive >= date1 && student.FirstActive <= date2 && student.Status == "Active" 
                           //                                                   : false
                           //                     } into Count_70_Group
                           select new
                           {
                               Count_Asian = db.Students.Count(n => n.Ethnicity == "Asian" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Black = db.Students.Count(n => n.Ethnicity == "Black" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Latino = db.Students.Count(n => n.Ethnicity == "Hispanic" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_NativeAmerican = db.Students.Count(n => n.Ethnicity == "Native American" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_PacificIslander = db.Students.Count(n => n.Ethnicity == "Pacific Islander" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_White = db.Students.Count(n => n.Ethnicity == "White" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Other = db.Students.Count(n => n.Ethnicity == "Other" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Unknown = db.Students.Count(n => n.Ethnicity == "Unknown" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),

                               Count_Age_Unknown = db.Students.Count(n => n.DOB != null && n.DOB >= minUnknown && n.DOB <= maxUnknown
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_10 = db.Students.Count(n => n.DOB != null && n.DOB >= min10 && n.DOB <= max10
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_20 = db.Students.Count(n => n.DOB != null && n.DOB >= min20 && n.DOB <= max20
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_30 = db.Students.Count(n => n.DOB != null && n.DOB >= min30 && n.DOB <= max30
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_40 = db.Students.Count(n => n.DOB != null && n.DOB >= min40 && n.DOB <= max40
                                                                && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_50 = db.Students.Count(n => n.DOB != null && n.DOB >= min50 && n.DOB <= max50
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_60 = db.Students.Count(n => n.DOB != null && n.DOB >= min60 && n.DOB <= max60
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_70 = db.Students.Count(n => n.DOB != null && n.DOB >= min70 && n.DOB <= max70
                                                                 && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),

                               Count_Male = db.Students.Count(n => n.Gender == "Male" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Female = db.Students.Count(n => n.Gender == "Female" && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                               Count_Gender_Unknown = db.Students.Count(n => n.Gender == null && n.FirstActive >= date1 && n.FirstActive <= date2 && n.Status == "Active"),
                           };
                if (clls.Count() == 0)
                {
                    return RedirectToAction("NoRecord", "Home");
                }
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("BeginDate", beginDate));
                paramList.Add(new ReportParameter("EndDate", endDate));

                paramList.Add(new ReportParameter("AdultLearnersFromPriorPeriod", AdultLearnersFromPriorPeriod == "" ? "0" : AdultLearnersFromPriorPeriod));
                paramList.Add(new ReportParameter("AdultLearnersBegan", AdultLearnersBegan == "" ? "0" : AdultLearnersBegan));
                paramList.Add(new ReportParameter("AdultLearnersReceived", AdultLearnersReceived == "" ? "0" : AdultLearnersReceived));
                paramList.Add(new ReportParameter("AdultLearnersLeft", AdultLearnersLeft == "" ? "0" : AdultLearnersLeft));
                paramList.Add(new ReportParameter("AdultLearnersRemaining", AdultLearnersRemaining == "" ? "0" : AdultLearnersRemaining));
                paramList.Add(new ReportParameter("CumulativeTotal", CumulativeTotal == "" ? "0" : CumulativeTotal));

                //paramList.Add(new ReportParameter("Count_Asian", clls.First().Count_Asian.ToString()));
                //paramList.Add(new ReportParameter("Count_Black", clls.First().Count_Black.ToString()));
                //paramList.Add(new ReportParameter("Count_Latino", clls.First().Count_Latino.ToString()));
                //paramList.Add(new ReportParameter("Count_NativeAmerican", clls.First().Count_NativeAmerican.ToString()));
                //paramList.Add(new ReportParameter("Count_PacificIslander", clls.First().Count_PacificIslander.ToString()));
                //paramList.Add(new ReportParameter("Count_White", clls.First().Count_White.ToString()));
                //paramList.Add(new ReportParameter("Count_Other", clls.First().Count_Other.ToString()));
                //paramList.Add(new ReportParameter("Count_Unknown", clls.First().Count_Unknown.ToString()));

                var Sum_Ethnicity = clls.First().Count_Asian + clls.First().Count_Black + clls.First().Count_Latino + clls.First().Count_NativeAmerican + clls.First().Count_PacificIslander + clls.First().Count_White + clls.First().Count_Other + clls.First().Count_Unknown;
                paramList.Add(new ReportParameter("Sum_Ethnicity", Sum_Ethnicity.ToString()));

                //paramList.Add(new ReportParameter("Count_10", clls.First().Count_10.ToString()));
                //paramList.Add(new ReportParameter("Count_20", clls.First().Count_20.ToString()));
                //paramList.Add(new ReportParameter("Count_30", clls.First().Count_30.ToString()));
                //paramList.Add(new ReportParameter("Count_40", clls.First().Count_40.ToString()));
                //paramList.Add(new ReportParameter("Count_50", clls.First().Count_50.ToString()));
                //paramList.Add(new ReportParameter("Count_60", clls.First().Count_60.ToString()));
                //paramList.Add(new ReportParameter("Count_70", clls.First().Count_70.ToString()));
                //paramList.Add(new ReportParameter("Count_Age_Unknown", clls.First().Count_Age_Unknown.ToString()));

                var Sum_Age = clls.First().Count_10 + clls.First().Count_20 + clls.First().Count_30 + clls.First().Count_40 + clls.First().Count_50 + clls.First().Count_60 + clls.First().Count_70 + clls.First().Count_Age_Unknown;
                paramList.Add(new ReportParameter("Sum_Age", Sum_Age.ToString()));

                //paramList.Add(new ReportParameter("Count_Male", clls.First().Count_Male.ToString()));
                //paramList.Add(new ReportParameter("Count_Female", clls.First().Count_Female.ToString()));
                //paramList.Add(new ReportParameter("Count_Gender_Unknown", clls.First().Count_Gender_Unknown.ToString()));

                var Sum_Gender = clls.First().Count_Male + clls.First().Count_Female + clls.First().Count_Gender_Unknown;
                paramList.Add(new ReportParameter("Sum_Gender", Sum_Gender.ToString()));

                paramList.Add(new ReportParameter("AdultsReferredToOtherPrograms", AdultsReferredToOtherPrograms == "" ? "0" : AdultsReferredToOtherPrograms));
                paramList.Add(new ReportParameter("AdultLearnersAwaitingInstruction", AdultLearnersAwaitingInstruction == "" ? "0" : AdultLearnersAwaitingInstruction));
                paramList.Add(new ReportParameter("AdultLearnersInstructionHours", AdultLearnersInstructionHours == "" ? "0" : AdultLearnersInstructionHours));
                paramList.Add(new ReportParameter("BooksGiven", BooksGiven == "" ? "0" : BooksGiven));

                return RunReport(reportType, "CLLS.rdlc", "CLLSDataSet", clls, paramList, -1, -1, 0.25, 0.25);
            }
        }

        public ActionResult Students()
        {
            // students
            //string[] names = ConfigurationManager.AppSettings.AllKeys
            //                                                .Where(k => k.StartsWith("StudentReport"))
            //                                                .Select(k => ConfigurationManager.AppSettings[k])
            //                                                .ToArray();

            var vm = new ReportsViewModel         
            {
                StudentsReport = ReportsViewModel.GetStudentsReportList() //names
            };

            //vm.StudentReports = new ICollection<string>();
            //foreach (var name in names)
            //{
            //    vm.StudentReports.Add(name);
            //}

            //            OleDbConnection connection = new OleDbConnection(
            //               "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Inventar.accdb");
            //            DataSet DS = new DataSet();
            //            connection.Open();
            //            string query =
            //                @"SELECT Status.*
            //                    FROM Status";
            //            OleDbDataAdapter DBAdapter = new OleDbDataAdapter();
            //            DBAdapter.SelectCommand = new OleDbCommand(query, connection);
            //            DBAdapter.Fill(DS);

            ////// Assumes that connection is a valid SqlConnection object.
            ////string connectionString = ConfigurationManager.ConnectionStrings["LitProReadEntities"].ConnectionString;

            ////// parse out the "data source" sub string
            ////int index = connectionString.IndexOf("data source");
            ////connectionString = connectionString.Substring(index, connectionString.Length - index);
            ////System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);     
            
            //////"Data Source=lib01acldb01.aclibrary.org;Initial Catalog=LitProRead 2-26-2014;User ID=Ulibproread;Password=@libproread;MultipleActiveResultSets=True;Application Name=EntityFramework");
            //////"data source=lib01acldb01.aclibrary.org;initial catalog=LitProRead 2-26-2014;User ID=Ulibproread;Password=@libproread; MultipleActiveResultSets=True;App=EntityFramework\""

            ////string queryString = "SELECT * FROM dbo.Status";
            ////System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            ////DataSet status = new DataSet();
            ////adapter.Fill(status);//, "Status");
            //////List<SelectListItem> list = new List<SelectListItem>();

            ////List<string> statusList = new List<string>();
            ////foreach (DataRow row in status.Tables[0].Rows)
            ////{
            ////    statusList.Add((string)row["status"]);
            ////}
            //vm.StudentsReportStatus = statusList;

            /* 2/25/14 - trung
             * What is this block code for: it doesn't look like the CLLS needs it
             * 
            StatusDataSet statusDs = new StatusDataSet();
            StatusTableAdapter ad = new StatusTableAdapter();
            DataTable dt = ad.GetData();
                //using (LitProReadEntities db = new LitProReadEntities())
            //{
            //    StudentReportsStatus = from s in db.GetTable<Status>();
            //}
            */

            return View(vm);
        }

        public ActionResult Run(string paramsVal)
        {
            string reportType = "";
            string reportName = "";
            string beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            string endDate = DateTime.Now.ToShortDateString();
            string statusType = "";

            if (paramsVal != null)
            {
                char[] sep = { '!' };
                string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                // 1st: report type (PDF, EXCEL, WORD, or IMAGE)
                string[] s1 = str[0].Split('=');
                reportType = s1[1];

                // 2nd: report name
                string[] s2 = str[1].Split('=');
                reportName = s2[1];


                // 3rd and above: the criteria.
                if (str.Count() > 2)        // not all reports have a start date
                {
                    string[] date = str[2].Split('=');
                    beginDate = date[1];
                }


                if (str.Count() > 3)        // not all reports have an end date
                {
                    string[] date = str[3].Split('=');
                    endDate = date[1];
                }

                if (str.Count() > 4)        // not all reports have a status type
                {
                    string[] type = str[4].Split('=');
                    statusType = type[1];
                }
            }

            DateTime date1;
            if (beginDate != "")
            {
                date1 = DateTime.ParseExact(beginDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(beginDate);
            }
            else
            {
                date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            DateTime date2 = DateTime.Now;
            if (endDate != "")
            {
                date2 = DateTime.ParseExact(endDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(endDate);
            }

            //if (reportName == "StudentBday")
            //{
            //    string chosenMonth = "Last Month";
            //    string chosenStatus = "Active";
            //    return StudentBDay(reportType, chosenMonth, chosenStatus);
            //}
            //else
            if (reportName == "StudentPortfolioReport")
                return StudentPortfolioReport(reportType);
            else
            if (reportName == "StudentActivePhoneList")
                return StudentActivePhoneList(reportType, true);
            else
            if (reportName == "StudentInActivePhoneList")
                return StudentActivePhoneList(reportType, false);
            else
            if (reportName == "StudentStatusHistory")
                return StudentStatusHistory(reportType, beginDate, endDate, statusType);
            else
            if (reportName == "StudentWaitTime")
                return StudentWaitTime(reportType, beginDate, endDate, statusType);
            else
                if (reportName == "StudentAccomplishmentsByActiveDateGreaterThan1Year")
                return StudentAccomplishmentsByActiveDateGreaterThan1Year(reportType, date1, date2, statusType);
            else
                return View();
        }

        //SELECT Students.FirstName, Students.LastName, Students.FirstActive, Date() AS Today, Students.Status, (DateDiff('m',[FirstActive],Now())) AS WaitTime FROM Students 
        //      WHERE (((Students.FirstActive) Between [Forms]![frmDateSelectionStudentStatus]![BeginDate] And [Forms]![frmDateSelectionStudentStatus]![EndDate]));
        public ActionResult StudentWaitTime(string reportType, string beginDate, string endDate, string statusType)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                DateTime date1 = new DateTime(1900, 01, 01);
                if (beginDate != "")
                    date1 = DateTime.ParseExact(beginDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(beginDate);
                DateTime date2 = new DateTime(2025, 12, 31);
                if (endDate != "")
                    date2 = DateTime.ParseExact(endDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(endDate);
                var wait = from student in db.Students
                           where statusType != "" ? (student.FirstActive >= date1 && student.FirstActive <= date2) && (student.Status.Equals(statusType)) : (student.FirstActive >= date1 && student.FirstActive <= date2)
                           select new { Name = student.LastName + ", " + student.FirstName, student.LastName, student.FirstName, student.FirstActive, student.Status };
                if (wait.Count() == 0)
                {
                    return RedirectToAction("NoRecord", "Home");
                }
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("BeginDate", date1.ToShortDateString()));   //startDate.ToShortDateString()));
                paramList.Add(new ReportParameter("EndDate", date2.ToShortDateString())); //endDate.ToShortDateString()));
                paramList.Add(new ReportParameter("StatusType", statusType));
                return RunReport(reportType, "StudentWaitTime.rdlc", "StudentWaitTimeDataSet", wait, paramList, - 1, -1, 0.25, 0.25);
            }
        }

        //SELECT [LastName] & "," & [FirstName] AS Name, Students.FirstName, Students.LastName, tblStatusHistory.*
        //FROM Students INNER JOIN tblStatusHistory ON Students.ID=tblStatusHistory.ID
        //WHERE (((tblStatusHistory.StudentorTutor)="Student") And ((tblStatusHistory.StatusDate) Between Forms!frmDateSelection!BeginDate And Forms!frmDateSelection!EndDate));
        public ActionResult StudentStatusHistory(string reportType, string beginDate, string endDate, string statusType)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var hist = from student in db.Students
                            join statusHist in db.tblStatusHistories on student.ID equals statusHist.ID
                            where statusHist.StudentorTutor.Equals("Student") && (student.LastName.StartsWith("A"))
                            orderby student.LastName
                           select new { Name = student.LastName + ", " + student.FirstName, student.LastName, student.FirstName, statusHist.StatusDate, statusHist.Status, statusHist.InActiveDate, statusHist.ChangedDateTime, statusHist.ChangedBy };

                return RunReport(reportType, "StudentStatusHistory.rdlc", "StudentStatusHistoryDataSet", hist, null, 11, 8.5, 0.25, 0.25);
            }
        }

        //SELECT Students.ID, [FirstName] & " " & [LastName] AS MyName, Students.FirstName, Students.LastName, Students.Status, Students.Active, Students.ActiveDate, 
        //      DateDiff("d",[ActiveDate],Now()) AS DaysofSvc, 
        //      YMD([ActiveDate],Now()) AS MyDate, 
        //      StudentAccomplishments.AccomplishDate, StudentAccomplishments.Accomplishment, StudentAccomplishments.Comment
        //FROM Students INNER JOIN StudentAccomplishments ON Students.ID = StudentAccomplishments.ID
        //WHERE (((Students.Active)=True) 
        //  AND ((Students.ActiveDate) Between [Forms]![frmDateSelection]![BeginDate] And [Forms]![frmDateSelection]![EndDate]) 
        //  AND ((DateDiff("d",[ActiveDate],Now()))>365) AND ((StudentAccomplishments.AccomplishDate) Is Not Null));
        //
        public ActionResult StudentAccomplishmentsByActiveDateGreaterThan1Year(string reportType, DateTime beginDate, DateTime endDate, string statusType)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var datasource = from student in db.Students
                                 join studentAccomplishments in db.StudentAccomplishments on student.ID equals studentAccomplishments.ID
                                 where student.Active == true &&
                                       student.ActiveDate >= beginDate &&
                                       student.ActiveDate <= endDate &&
                                       System.Data.Objects.SqlClient.SqlFunctions.DateDiff("day", student.ActiveDate, DateTime.Now) > 365 &&
                                       studentAccomplishments.AccomplishDate != null
                                 //let datespan = student.ActiveDate != null ? ConvertToTimeSpanString(student.ActiveDate) : "0 days"
                                 //group new { student, studentAccomplishments } by new { student.ID } into gp
                                 //let s = gp.FirstOrDefault().student
                                 //let sa = gp.FirstOrDefault().studentAccomplishments
                                 select new
                                 {
                                     StudentID = student.ID,
                                     StudentName = student.FirstName + " " + student.LastName,
                                     StudentLastName = student.LastName,
                                     StudentFirstName = student.FirstName,
                                     StudentStatus = student.Status,
                                     StudentActive = student.Active,
                                     StudentActiveDate = student.ActiveDate,
                                     DaysofSvc = SqlFunctions.DateDiff("day", student.ActiveDate, DateTime.Now),
                                     AccomplishDate = studentAccomplishments.AccomplishDate,
                                     Accomplishment = studentAccomplishments.Accomplishment,
                                     Comment = studentAccomplishments.Comment
                                 };

                List<StudentAccomplishmentBO> list = new List<StudentAccomplishmentBO>();
                foreach (var item in datasource)
                {
                    string datespan = item.StudentActiveDate != null ? ConvertToTimeSpanString(item.StudentActiveDate) : "0 days";
                    list.Add(new StudentAccomplishmentBO
                            {
                                StudentID = item.StudentID,
                                StudentName = item.StudentName,
                                StudentLastName = item.StudentLastName,
                                StudentFirstName = item.StudentFirstName,
                                StudentStatus = item.StudentStatus,
                                StudentActive = item.StudentActive,
                                StudentActiveDate = item.StudentActiveDate,
                                DaysofSvc = item.DaysofSvc,
                                YMD = datespan,
                                AccomplishDate = item.AccomplishDate,
                                Accomplishment = item.Accomplishment,
                                Comment = item.Comment
                            });
                }

                // get # of accomplishments that > 1
                int percentageWithMoreThan1Accomplishment = 0;
                if (list.Count() > 0)
                {
                    var grp = list.GroupBy(item => item.StudentID);
                    percentageWithMoreThan1Accomplishment = grp.Count(item => item.Count() > 1);   
                }
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("BeginDate", beginDate.ToShortDateString()));
                paramList.Add(new ReportParameter("EndDate", endDate.ToShortDateString()));
                paramList.Add(new ReportParameter("PercentageWithMoreThan1Accomplishment", percentageWithMoreThan1Accomplishment.ToString()));

                return RunReport(reportType, "StudentAccomplishmentsByActiveDateGreaterThan1Year.rdlc", "StudentAccomplishmentsByActiveDateGreaterThan1YearDataSet", list, paramList, 11, 8.5, 0.25, 0.25);
            }
        }

        private string ConvertToTimeSpanString(DateTime? dt)
        {
            if (dt == null)
                return "0 days";

            DateTime date = (DateTime)dt;
            DateTime oldDate;

            DateTime.TryParse(date.ToShortDateString(), out oldDate);
            DateTime currentDate = DateTime.Now;

            TimeSpan difference = currentDate.Subtract(oldDate);

            // This is to convert the timespan to datetime object
            DateTime DateTimeDifferene = DateTime.MinValue + difference;

            // Min value is 01/01/0001
            // subtract our addition or 1 on all components to get the 
            //actual date.

            int InYears = DateTimeDifferene.Year - 1;
            int InMonths = DateTimeDifferene.Month - 1;
            int InDays = DateTimeDifferene.Day - 1;


            return InYears.ToString() + " Years " + InMonths.ToString() + " Months " + InDays.ToString() + " Days";
        }

        //SELECT tblAuditTrail.*, [FirstName] & " " & [LastName] AS MyName
        //FROM tblAuditTrail
        //WHERE (((tblAuditTrail.ID)="S" & [Forms]![frmMain]![frmStudents].[Form]![ID]))
        //ORDER BY tblAuditTrail.DateChanged DESC , tblAuditTrail.TimeChanged DESC;
        public ActionResult AuditTrailThisStudent(string id = "")
        {
            //int studentId = Convert.ToInt32(id);
            string reportType = "PDF";
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var datasource = from item in db.tblAuditTrails.ToList()
                                 where item.ID.Equals("S" + id)
                                 orderby item.DateChanged descending, item.TimeChanged descending
                                 select new
                                 {
                                     MyName = item.FirstName + " " + item.LastName,
                                     DateChanged = item.DateChanged.GetValueOrDefault().ToShortDateString(),
                                     TimeChanged = item.TimeChanged.GetValueOrDefault().ToShortTimeString(),
                                     FieldName = item.FieldName,
                                     OldValue = item.OldValue,
                                     NewValue = item.NewValue,
                                     Computer = item.Computer,
                                     Employee = item.Employee,
                                     ID = item.ID
                                 };
                //if (datasource.FirstOrDefault() == null)
                //    return 
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("MyName", datasource.FirstOrDefault().MyName));

                return RunReport(reportType, "AuditTrailThisStudent.rdlc", "AuditTrailThisStudentDataSet", datasource, paramList, 11, 8.5, 0.25, 0.25);
            }
        }

        //SELECT Pairs.*, 
        //  IIf(IsNull([DissolveDate]), DateDiff("m",[MatchDate] ,Now()), DateDiff("m",[MatchDate],[DissolveDate])) AS MthofSvc, 
        //  students.HomeAreaCode & " " & Students.HomePhone AS StudentHome, 
        //  Students.WorkAreaCode & " " & Students.WorkPhone AS StudentWork, 
        //  Tutors.HomeAreaCode & " " & Tutors.HomePhone AS TutorHome, 
        //  Tutors.WorkAreaCode & " " & Tutors.WorkPhone AS TutorWork, 
        //  Tutors.TutorContact, 
        //  Students.Status, 
        //  Tutors.Status
        //FROM Students INNER JOIN (Tutors INNER JOIN Pairs ON Tutors.ID = Pairs.TID) ON Students.ID = Pairs.SID
        //WHERE (((Pairs.MatchDate) Between [Forms]![frmStudentMatchHistorybyDateRange]![BeginDate] And [Forms]![frmStudentMatchHistorybyDateRange]![EndDate]));
        public ActionResult StudentsMatchHistorybyDateRange(string paramsVal)   //string studentId, string beginDate, string endDate)
        {
            string id = "0";
            string beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            string endDate = DateTime.Now.ToShortDateString();

            if (paramsVal != null)
            {
                char[] sep = { '!' };
                string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                // 1st: student Id
                string[] s1 = str[0].Split('=');
                id = s1[1];

                // 2nd: begin date
                if (str.Count() > 1)        // not all reports have a start date
                {
                    string[] date = str[1].Split('=');
                    beginDate = date[1];
                }

                // 3rd: end date
                if (str.Count() > 2)        // not all reports have an end date
                {
                    string[] date = str[2].Split('=');
                    endDate = date[1];
                }
            }

            int studentID = Convert.ToInt32(id);

            DateTime date1;
            if (beginDate != "")
            {
                date1 = DateTime.ParseExact(beginDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(beginDate);
            }
            else
            {
                date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            DateTime date2 = DateTime.Now;
            if (endDate != "")
            {
                date2 = DateTime.ParseExact(endDate, @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);       //Parse(endDate);
            }

            using (LitProReadEntities db = new LitProReadEntities())
            {
                var student = db.Students.Find(studentID);
                var stuStatus = student.Status;
                var dataSource =
                    from pair in db.Pairs
                    where pair.SID == studentID && pair.MatchDate >= date1 && pair.MatchDate <= date2
                    join tutor in db.Tutors on pair.TID equals tutor.ID //into pt 
                    //from p in pt.DefaultIfEmpty() 
                    select new
                    {
                        UniqID = pair.UniqID,
                        TID = pair.TID,
                        SID = studentID,
                        MatchDate = pair.MatchDate,
                        DissolveDate = pair.DissolveDate,
                        Comments = pair.Comments,
                        //TotalHoursMet = t.Sum(x => x.HoursMet)

                        StudentID = studentID,
                        StudentName = student.FirstName + " " + student.LastName,
                        StudentHome = student.HomeAreaCode + " " + student.HomePhone,
                        StudentWork = student.WorkAreaCode + " " + student.WorkPhone,
                        StudentStatus = student.Status,
                        
                        TutorID = tutor.ID,
                        TutorName = tutor.FirstName + " " + tutor.LastName,
                        TutorHome = tutor.HomeAreaCode + " " + tutor.HomePhone,
                        TutorWork = tutor.WorkAreaCode + " " + tutor.WorkPhone,
                        TutorStatus = tutor.Status,
                        TutorContact = tutor.TutorContact
                    };

                //if (datasource.FirstOrDefault() == null)
                //    return 
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("BeginDate", date1.ToShortDateString()));
                paramList.Add(new ReportParameter("EndDate", date2.ToShortDateString()));

                return RunReport("PDF", "StudentsMatchHistorybyDateRange.rdlc", "StudentsMatchHistorybyDateRangeDataSet", dataSource, paramList, 11, 8.5, 0.25, 0.25);
            }
        }

        public ActionResult StudentActivePhoneList(string reportType, bool showActive)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var datasource = from item in db.Students
                                 where showActive == false ? item.Active == false : item.Active == true || item.Active == false  
                                 orderby item.LastName
                                 select new
                                 {
                                     Name = item.LastName + ", " + item.FirstName,
                                     Address = item.Address1,
                                     CityStateZip = item.City + ", " + item.State + " " + item.Zip,
                                     WorkPhone = item.WorkAreaCode + " " + item.WorkPhone,
                                     HomePhone = item.HomeAreaCode + " " + item.HomePhone,
                                     Active = item.Active,
                                     ContactPref = item.ContactPref
                                 };
                //if (datasource.FirstOrDefault() == null)
                //    return 
                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("ShowActive", showActive ? "true" : "false"));
                return RunReport(reportType, "StudentActivePhoneList.rdlc", "StudentActivePhoneListDataSet", datasource, paramList, 11, 8.5, 0.25, 0.25);
            }
        }

        //SELECT Students.FirstName, Students.LastName, [Address1] & " " & [Address2] AS Address, [City] & ", " & [State] & "  " & [Zip] AS CitZip, [HomeAreaCode] & " " & [HomePhone] AS Home, [WorkAreaCode] & " " & [WorkPhone] AS [Work], StudentAssessmentGoals.AssessNextReview, Pairs.TutorFName, Pairs.TutorLName, Pairs.MatchDate, Pairs.DissolveDate
        //FROM (Students INNER JOIN Pairs ON Students.ID = Pairs.SID) LEFT JOIN StudentAssessmentGoals ON Students.ID = StudentAssessmentGoals.ID
        //WHERE (((Pairs.DissolveDate) Is Null) AND ((Year([AssessNextReview]))=Year(Now())) AND ((Month([AssessNextReview]))=Month(Now())))
        //ORDER BY Students.LastName;

        //    // LINQ: simulate a LEFT JOIN
        //using (LitProReadEntities db = new LitProReadEntities())
        //{
        //    var dataSource = from student in db.Students
        //                        join pair in db.Pairs on student.ID equals pair.SID into grpJoin
        //                        from studentpair in grpJoin.DefaultIfEmpty()
        //                        select new { student.FirstName, PetName = (studentpair == null ? String.Empty : studentpair.Name) };
        //}

        public ActionResult StudentPortfolioReport(string reportType)
        {
            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;

            using (LitProReadEntities db = new LitProReadEntities())
            {
                var list = db.Database.SqlQuery<StudentAssessmentGoalsBO>("SELECT * FROM dbo.StudentAssessmentGoals");
                List<StudentAssessmentGoalsBO> assessmentGoals = new List<StudentAssessmentGoalsBO>();
                foreach (var a in  list)
                {
                    if (a.AssessNextReview != null && a.AssessNextReview.Value.Year == currYear &&  a.AssessNextReview.Value.Month == currMonth)
                    {
                        StudentAssessmentGoalsBO aBO = new StudentAssessmentGoalsBO();
                        aBO.ID = a.ID;
                        aBO.AssessDate = a.AssessDate;
                        aBO.AssessNextReview = a.AssessNextReview;

                        aBO.AssessRoal = a.AssessRoal;
                        aBO.AssessProgress = a.AssessProgress;
                        aBO.AssessProof = a.AssessProof;
                        aBO.AssessSkill = a.AssessSkill;
                        aBO.AssessFollowUp = a.AssessFollowUp;

                        aBO.DateCreated = a.DateCreated;
                        aBO.DateModified = a.DateModified;
                        aBO.LastModifiedBy = a.LastModifiedBy;

                        assessmentGoals.Add(aBO);
                    }
                }
                 //          where AssessNextReview != null ? x.AssessNextReview.Value.Date.Month == currMonth : false
                   //        select a;
                    //assessmentGoals.Where(x => x.AssessNextReview != null ? x.AssessNextReview.Value.Date.Month == currMonth : false &&
                      //                                x.AssessNextReview.Value.Date.Year == currYear);
//                var temp = assessmentGoals.Where(x => SqlFunctions.DatePart("m", x.AssessNextReview) == DateTime.Now.Month);

                var dataSource = from student in db.Students
                                 join pair in db.Pairs on student.ID equals pair.SID into studentPairGrp
                                 from studentpair in studentPairGrp
                                 where studentpair.DissolveDate != null
                                 join tutor in db.Tutors on studentpair.TID equals tutor.ID
                                 select new
                                 {
                                     SID = studentpair.SID,
                                     StudentName = student.LastName + ", " + student.FirstName,
                                     StudentAddress = student.Address1 + " " + student.Address2,
                                     StudentCityStateZip = student.City + ", " + student.State + " " + student.Zip,
                                     StudentWorkPhone = student.WorkAreaCode + " " + student.WorkPhone,
                                     StudentHomePhone = student.HomeAreaCode + " " + student.HomePhone,

                                     TutorName = tutor.LastName + ", " + tutor.FirstName,
                                     MatchDate = studentpair.MatchDate,
                                     DissolveDate = studentpair.DissolveDate
                                 };

                var moddataSource = from assesessment in assessmentGoals
                                    join item in dataSource on assesessment.ID equals item.SID
                                    orderby (item.StudentName)



                                    select new {
                                        StudentName = item.StudentName,
                                        StudentAddress = item.StudentAddress,
                                        StudentCityStateZip = item.StudentCityStateZip,
                                        StudentWorkPhone = item.StudentWorkPhone,
                                        StudentHomePhone = item.StudentHomePhone,
                                        TutorName = item.TutorName,
                                        MatchDate = item.MatchDate,
                                        DissolveDate = item.DissolveDate,
                                        AssessNextReview = assesessment.AssessNextReview
                                    };
                var portfolio = moddataSource.GroupBy(a => a.StudentName).Select(g => g.First());

                                 //where studentpair.DissolveDate != null;
                List<ReportParameter> paramList = new List<ReportParameter>();
                //paramList.Add(new ReportParameter("ShowActive", showActive ? "true" : "false"));
                return RunReport(reportType, "StudentPortfolioReport.rdlc", "StudentPortfolioReportDataSet", portfolio, paramList, 11, 8.5, 0.25, 0.25);
            }
        }

        public ActionResult StudentBDay(string paramsVal)
        {
            string reportType = "";
            string chosenMonth = "";
            string chosenStatus = "";
            if (paramsVal != null)
            {
                char[] sep = { '!' };
                string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                // 1st: report type (PDF, EXCEL, WORD, or IMAGE)
                string[] s1 = str[0].Split('=');
                reportType = s1[1];

                // 2nd: report name
                string[] s2 = str[1].Split('=');
                chosenMonth = s2[1];

                // 2nd: report name
                string[] s3 = str[2].Split('=');
                chosenStatus = s3[1];
                if (chosenStatus.Length == 0)
                {
                    StringBuilder temp = new StringBuilder();
                    var vm = new ReportsViewModel();
                    var statusList = vm.ChosenStatusList;
                    int cnt = 0;
                    int listCnt = statusList.Count-1;
                    foreach (var s in statusList)
                    {
                        string tempS = s.Text.ToString();
                        if (tempS != "")
                        {
                            temp.Append("'");
                            temp.Append(tempS);
                            temp.Append("'");
                            if (cnt++ < listCnt)
                                temp.Append(",");
                        }
                    }
                    chosenStatus = temp.ToString();
                }
            }

            int currMonth = DateTime.Now.Month;
            string currMonthStr = DateTime.Now.ToString("MMM");
            if (chosenMonth.Equals("Last Month", StringComparison.OrdinalIgnoreCase))
            {
                currMonth = currMonth <= 1 ? 1 : currMonth - 1;
                DateTime temp = new DateTime(DateTime.Now.Year, currMonth, 1);
                currMonthStr = temp.ToString("MMM");
            }
            else
            if (chosenMonth.Equals("Next Month", StringComparison.OrdinalIgnoreCase))
            {
                currMonth = currMonth >= 12 ? 12 : currMonth + 1;
                DateTime temp = new DateTime(DateTime.Now.Year, currMonth, 1);
                currMonthStr = temp.ToString("MMM");
            }
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var dataSource = from student in db.Students
                                 where chosenStatus.Contains(student.Status) && student.DOB.Value.Month == currMonth
                                 select new {
                                     DOB = student.DOB,
                                     Age = System.Data.Objects.SqlClient.SqlFunctions.DateDiff("month", student.DOB, DateTime.Now) / 12,
                                     //Age = Convert.ToString(System.Data.Objects.SqlClient.SqlFunctions.DateDiff("month", student.DOB, DateTime.Now) / 12),
                                     Name = student.LastName + ", " + student.FirstName,
                                     Address = student.Address1,
                                     City = student.City,
                                     State = student.State,
                                     Zip = student.Zip,
                                     WorkPhone = student.WorkAreaCode + " " + student.WorkPhone,
                                     HomePhone = student.HomeAreaCode + " " + student.HomePhone,
                                     Status = student.Status
                                 };

                List<ReportParameter> paramList = new List<ReportParameter>();
                paramList.Add(new ReportParameter("ChosenMonth", currMonthStr));
                paramList.Add(new ReportParameter("ChosenStatus", chosenStatus));
                return RunReport(reportType, "StudentBday.rdlc", "StudentBdayDataSet", dataSource, paramList, 11, 8.5, 0.25, 0.25);
           }
        }

        private ActionResult RunReport(string reportType, string reportName, string dataSetname, object dataSourceValue, List<ReportParameter> paramList, double width = -1, double height = -1, double horzMargin = -1, double vertMargin = -1)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports/rdlc"), reportName);
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            //List<StateArea> cm = new List<StateArea>();
            using (LitProReadEntities db = new LitProReadEntities())
            {
                ReportDataSource rd = new ReportDataSource();
                rd.Name = dataSetname;
                rd.Value = dataSourceValue;
                lr.DataSources.Add(rd);

                if (paramList != null)
                    lr.SetParameters(paramList);

                string mimeType;
                string encoding;
                string fileNameExtension;

                string w = (width == -1 ? 8.5 : width).ToString();
                string h = (height == -1 ? 11 : height).ToString();
                string hm = (horzMargin == -1 ? 1 : horzMargin).ToString();
                string vm = (vertMargin == -1 ? 1 : vertMargin).ToString();
                string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>" + reportType + "</OutputFormat>" +
                    "  <PageWidth>" + w + "in</PageWidth>" +
                    "  <PageHeight>" + h + "in</PageHeight>" +
                    "  <MarginTop>" + vm + "in</MarginTop>" +
                    "  <MarginLeft>" + hm + "in</MarginLeft>" +
                    "  <MarginRight>" + hm + "in</MarginRight>" +
                    "  <MarginBottom>" + vm + "in</MarginBottom>" +
                    "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
        }
    }
}
