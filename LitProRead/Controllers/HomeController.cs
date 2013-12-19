using LitProRead.Models;
using LitProRead.Reports.DataSets;
using LitProRead.Reports.DataSets.StatusDataSetTableAdapters;
using LitProRead.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Reports()
        {
            // students
            string[] names = ConfigurationManager.AppSettings.AllKeys
                                                            .Where(k => k.StartsWith("StudentReport"))
                                                            .Select(k => ConfigurationManager.AppSettings[k])
                                                            .ToArray();
            var vm = new ReportsViewModel
            {
                StudentReports = names
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

            // Assumes that connection is a valid SqlConnection object.
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=LitProRead;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            string queryString = "SELECT * FROM dbo.Status";
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            DataSet status = new DataSet();
            adapter.Fill(status);//, "Status");
            //List<SelectListItem> list = new List<SelectListItem>();
            
            List<string> statusList = new List<string>();
            foreach (DataRow row in status.Tables[0].Rows)
            {
                statusList.Add((string)row["status"]);
            }
            vm.StudentReportsStatus = statusList;

            StatusDataSet statusDs = new StatusDataSet();
            StatusTableAdapter ad = new StatusTableAdapter();
            DataTable dt = ad.GetData();
                //using (LitProReadEntities db = new LitProReadEntities())
            //{
            //    StudentReportsStatus = from s in db.GetTable<Status>();
            //}

            return View(vm);
        }

        public ActionResult NoRecord()
        {
            TempData["STATUS"] = "No records found";
            return RedirectToAction("Reports");
        }
    }
}
