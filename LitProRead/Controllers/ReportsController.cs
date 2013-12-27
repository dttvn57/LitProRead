using LitProRead.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public ActionResult CLLS(string reportType = "PDF", string beginDate = "", string endDate = "",
                                 string AdultLearnersFromPriorPeriod = "",
                                 string AdultLearnersBegan = "",
                                 string AdultLearnersReceived = "",
                                 string AdultLearnersLeft = "",
                                 string AdultLearnersRemaining = "",
                                 string CumulativeTotal = "",
                                 string AdultsReferredToOtherPrograms = "",
                                 string AdultLearnersAwaitingInstruction = "",
                                 string AdultLearnersInstructionHours = "",
                                 string BooksGiven = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                DateTime date1 = DefaultBeginDate();
                if (beginDate != "")
                {
                    date1 = DateTime.Parse(beginDate);
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
                    date2 = DateTime.Parse(endDate);
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

        // Students
        public ActionResult Run(string paramsVal)
        {
            string reportType = "";
            string reportName = "";
            string beginDate = "";
            string endDate = "";
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
                string[] date = str[2].Split('=');
                beginDate = date[1];
                date = str[3].Split('=');
                endDate = date[1];

                string[] type = str[4].Split('=');
                statusType = type[1];
            }

            if (reportName == "StudentStatusHistory")
                return StudentStatusHistory(reportType, beginDate, endDate, statusType);
            else
                if (reportName == "StudentWaitTime")
                    return StudentWaitTime(reportType, beginDate, endDate, statusType);
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
                    date1 = DateTime.Parse(beginDate);
                DateTime date2 = new DateTime(2025, 12, 31);
                if (endDate != "")
                    date2 = DateTime.Parse(endDate);
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
