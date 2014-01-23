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
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult GetStudentsName(string searchTerm, int pageSize, int pageNum, bool byLastName)  // true: get by last name
                                                                                                            // false: get by first name
        {
            //Get the paged results and the total count of the results for this query. 
            LitProReadEntities db = new LitProReadEntities();

            List<Student> students = db.GetStudents(searchTerm, pageSize, pageNum, byLastName);  
            int studentCount = db.GetStudentsCount(searchTerm, pageSize, pageNum);

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedStudents = ToSelect2Format(students, studentCount, byLastName);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedStudents,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult Index(int Id = -1)
        {
            if (Id == -1)
            {
                var vm = new StudentFormViewModel();
                //vm.studentVM = new StudentViewModel();
                return View("Forms", vm);
            }
            else
            {
                var vm = new StudentFormViewModel();
                if (vm == null || vm.CurrentStudent == null)
                {
                    return HttpNotFound();
                }
                vm.Load(Id);
                return View("Forms", vm);
            }
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
            //vm.StudentListLastName = vm.GetStudentsLastName(id);  // new SelectList(db.Companies, "CompanyId", "CompanyName");
            //vm.StudentListFirstName = vm.GetStudentsFirstName(id);  // new SelectList(db.Companies, "CompanyId", "CompanyName");

            //MyClass cla = new MyClass { Student = new Student { ID = 1, Name = "Student" }, Teacher = new Teacher { ID = 1, Name = "Teacher" } };
            JsonResult jsonData = new JsonResult();
            jsonData.Data = vm;
            jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonData;

            //return PartialView("_Student-All-View", vm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(StudentFormViewModel studentFormVm)//string dataO)
        {
            if (ModelState.IsValid)
            {
                using (LitProReadEntities db = new LitProReadEntities())
                {
                    db.Configuration.ValidateOnSaveEnabled = true;
                    db.Entry(studentFormVm.CurrentStudent).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        
                        string err = ex.Message;
                        int i = 0;
                        i++;
                    }
                    catch (OptimisticConcurrencyException ex)
                    {
                        string err = ex.Message;
                        int i = 0;
                        i++;
                        //studentFormVm.db.Refresh(RefreshMode.ClientWins, studentFormVm.CurrentStudent);
                        //studentFormVm.db.SaveChanges();
                    }
                    //return View("Forms", studentFormVm);
                    //return RedirectToAction("Index", new { Id = studentFormVm.CurrentStudent.ID });  // PartialView("_Student-General-View", studentFormVm);
                }
            }
            //return View(studentFormVm);
            JsonResult jsonData = new JsonResult();
            jsonData.Data = studentFormVm;
            jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //return jsonData;
            return Json(new { msg = "Successfully saved " + studentFormVm.CurrentStudent.LastName });
        }

        /***
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

/ ****************
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
 *************************** /
        }
******/

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

        private Select2PagedResult ToSelect2Format(List<Student> students, int totalStudents, bool byLastName)
        {
            Select2PagedResult jsonStudents = new Select2PagedResult();
            jsonStudents.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            if (byLastName)
            {
                foreach (Student a in students)
                {
                    jsonStudents.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.LastName + ", " + a.FirstName });
                }
            }
            else
            {
                foreach (Student a in students)
                {
                    jsonStudents.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.FirstName + " " + a.LastName });
                }

            }

            //Set the total count of the results from the query.
            jsonStudents.Total = totalStudents;

            return jsonStudents;
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



    //Extra classes to format the results the way the select2 dropdown wants them
    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }

    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
