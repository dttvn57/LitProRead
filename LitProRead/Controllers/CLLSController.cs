using LitProRead.Helpers;
using LitProRead.Models;
using LitProRead.Reports.DataSets;
using LitProRead.Reports.DataSets.StatusDataSetTableAdapters;
using LitProRead.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LitProRead.Controllers
{
    public class CLLSController : Controller
    {
        public ActionResult Index()
        {
            //string[] names = ConfigurationManager.AppSettings.AllKeys
            //                                                .Where(k => k.StartsWith("StudentReport"))
            //                                                .Select(k => ConfigurationManager.AppSettings[k])
            //                                                .ToArray();
            //var vm = new ReportsViewModel
            //{
            //    ReportStudents = names
            //};

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
            //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=LitProRead 2-26-2014;User ID=tdangdb;Password=tdangdb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            //string queryString = "SELECT * FROM dbo.Status";
            //System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            //DataSet status = new DataSet();
            //adapter.Fill(status);//, "Status");
            ////List<SelectListItem> list = new List<SelectListItem>();
            
            //List<string> statusList = new List<string>();
            //foreach (DataRow row in status.Tables[0].Rows)
            //{
            //    statusList.Add((string)row["status"]);
            //}
            //vm.ReportStudentsStatus = statusList;

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

            return View();//vm);
        }

    }
}
