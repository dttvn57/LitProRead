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
        //
        // GET: /Reports/

        //SELECT Students.FirstName, Students.LastName, Students.FirstActive, Date() AS Today, Students.Status, (DateDiff('m',[FirstActive],Now())) AS WaitTime FROM Students WHERE (((Students.FirstActive) Between [Forms]![frmDateSelectionStudentStatus]![BeginDate] And [Forms]![frmDateSelectionStudentStatus]![EndDate]));
        public ActionResult StudentWaitTime(string reportType)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var wait = from student in db.Students
                           //join statusHist in db.tblStatusHistories on student.ID equals statusHist.ID
                           //where statusHist.StudentorTutor.Equals("Student") && (student.LastName.StartsWith("A"))
                           //orderby student.LastName
                           select new { Name = student.LastName + ", " + student.FirstName, student.LastName, student.FirstName, student.FirstActive, student.Status };

                return RunReport(reportType, "StudentWaitTime.rdlc", "StudentWaitTimeDataSet", wait);
            }
        }

        //SELECT [LastName] & "," & [FirstName] AS Name, Students.FirstName, Students.LastName, tblStatusHistory.*
        //FROM Students INNER JOIN tblStatusHistory ON Students.ID=tblStatusHistory.ID
        //WHERE (((tblStatusHistory.StudentorTutor)="Student") And ((tblStatusHistory.StatusDate) Between Forms!frmDateSelection!BeginDate And Forms!frmDateSelection!EndDate));
        public ActionResult StudentStatusHistory(string reportType)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var hist = from student in db.Students
                            join statusHist in db.tblStatusHistories on student.ID equals statusHist.ID
                            where statusHist.StudentorTutor.Equals("Student") && (student.LastName.StartsWith("A"))
                            orderby student.LastName
                           select new { Name = student.LastName + ", " + student.FirstName, student.LastName, student.FirstName, statusHist.StatusDate, statusHist.Status, statusHist.InActiveDate, statusHist.ChangedDateTime, statusHist.ChangedBy };

                return RunReport(reportType, "StudentStatusHistory.rdlc", "StudentStatusHistoryDataSet", hist, 11, 8.5, 0.25, 0.25);
            }

            /**
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports/rdlc"), "StudentStatusHistory.rdlc");
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
                //var students = from s in db.Students
                //               where s.LastName.StartsWith("ch")
                //               select new { s.LastName, s.FirstName, s.Address1, s.City, s.Zip };
                //ReportDataSource rd = new ReportDataSource("StudentDataSet", students);
                var hist = from student in db.Students
                           join statusHist in db.tblStatusHistories on student.ID equals statusHist.ID
                           where statusHist.StudentorTutor.Equals("Student") && (student.LastName.StartsWith("Abad"))
                           orderby student.LastName
                           select new { Name = student.LastName + ", " + student.FirstName, student.LastName, student.FirstName, statusHist.StatusDate, statusHist.Status, statusHist.ChangedDateTime, statusHist.ChangedBy };
                               //where s.StudentorTutor =="Student"
                               //select s;
                ReportDataSource rd = new ReportDataSource("StudentStatusHistoryDataSet", hist);
                lr.DataSources.Add(rd);
                //string reportType = "EXCEL";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" +  reportType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
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
            **/
        }

        public ActionResult Run(string paramsVal)
        {
            string reportType = "";
            string reportName = "";

            if (paramsVal != null)
            {
                char[] sep = { '!' };
                string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                string[] s1 = str[0].Split('=');
                reportType = s1[1];

                string[] s2 = str[1].Split('=');
                reportName = s2[1];
            }

            if (reportName == "StudentStatusHistory")
                return StudentStatusHistory(reportType);
            else
            if (reportName == "StudentWaitTime")
                return StudentWaitTime(reportType);
            else
                return View();
       }

        private ActionResult RunReport(string reportType, string reportName, string dataSetname, object dataSourceValue, double width = -1, double height = -1, double horzMargin = -1, double vertMargin = -1)
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
