﻿using LitProRead.Models;
using LitProRead.Reports.DataSets;
using LitProRead.Reports.DataSets.StatusDataSetTableAdapters;
using LitProRead.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LitProRead.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new StudentFormViewModel();
            //vm.studentVM = new StudentViewModel();
            return View("Forms", vm);
        }

        //
        // GET: /Student/Edit/5
        public ActionResult Edit(int id = -1)      //)JsonResult --- string studentvm
        {
            //StudentFormViewModel s = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<StudentFormViewModel>(studentvm);
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //var response = new Response(true, "Contact Successfully Submitted");
            //return Json(response);

            var vm = new StudentFormViewModel();
            if (vm == null || vm.CurrentStudent == null)
            {
                return HttpNotFound();
            }
            vm.Load(id);
            return PartialView("_Student-General-View", vm);
        }

        //
        // POST: /Student/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(string studentData)//string dataO)
        {
            // Demonstrate deserialization from a raw string
            //Dictionary<string, Object> values0 = JsonConvert.DeserializeObject<Dictionary<string, Object>>(studentData);
           // Dictionary<string, object> values = deserializeToDictionary(studentData);
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(studentData);
            //string addr1 = values.First("CurrentStudent.Address1");
            
            //var studentVM = JObject.Parse(studentData);
           // var addr = studentVM["CurrentStudent.Address1"];

            StudentFormViewModel studentVm = new StudentFormViewModel();
            studentVm.Populate(values);

            if (ModelState.IsValid)
            {
                //db.Entry(student).State = EntityState.Modified;
                //db.SaveChanges();
                return PartialView("_Student-General-View", studentVm);
                //return RedirectToAction("Index");
            }
            return PartialView(studentData);

/****************
            //var objects = JArray.Parse(dataO); // parse as array  
            //foreach (JObject root in objects)
            //{
            //    foreach (KeyValuePair<String, JToken> app in root)
            //    {
            //        int id = 1;
            //        id++;
            //        //var appName = app.Key;
            //        //var description = (String)app.Value["Description"];
            //       // var value = (String)app.Value["Value"];
            //    }
            //}


            //JArray jsonVal = JArray.Parse(dataO) as JArray;
            //dynamic albums = jsonVal;

            //foreach (dynamic album in albums)
            //{
            //    int id = album.Id ;
            //    foreach (dynamic song in album.CurrentStudent)
            //    {
            //        string addr = (string) song.Address1;
            //        string name = song.FirstName;
            //        int i = id;
            //    }
            //}            
            //======================
            string test = "";
            JsonTextReader reader = new JsonTextReader(new StringReader(dataO));
            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //    {
            //        if ((string)reader.Value == "CurrentStudent.Address1")
            //            test = (string)reader.Value;
            //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //    }
            //    else
            //        Console.WriteLine("Token: {0}", reader.TokenType);
            //}
            //var ser = JsonSerializer.Create(null);
            //var jReader = new JsonTextReader(new StringReader(dataO));
            //var grp = ser.Deserialize<StudentFormViewModel>(jReader);
            //Student st = grp.CurrentStudent;
            //string addr1 = st.Address1;

            //var o = JObject.Parse(dataO);
            //var add = o["CurrentStudent.Address1"];//["Address1"];

            ////string CurrentStudent_Address1 = (string)o.SelectToken("CurrentStudent.Address1");

            ////dynamic d = JsonConvert.DeserializeObject<dynamic>(dataO);
            //CurrentStudent_Address1 = d.CurrentStudent.Address1;
            //CurrentStudent_Address1 = d.CurrentStudent_Address1;

            var myVM = JsonConvert.DeserializeObject<StudentFormViewModel>(dataO);

            string CurrentStudent_Address1 = myVM.CurrentStudent.Address1;

            var log = JsonConvert.DeserializeObject<StudentFormViewModel>(dataO);

            StudentFormViewModel s = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<StudentFormViewModel>(dataO);
            if (ModelState.IsValid)
            {
                //db.Entry(student).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(dataO);
 ***************************/
        }

        public ActionResult Forms()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Student-General-View");//, model);
            }
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

        private Dictionary<string, object> deserializeToDictionary(string jo)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                {
                    values2.Add(d.Key, deserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }
    }
}
