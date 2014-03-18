using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitProRead.Models;
using LitProRead.ViewModels;
using System.Web.Script.Serialization;
using System.Collections;
using System.Data.Entity.Validation;
using System.Threading;
using System.Configuration;

namespace LitProRead.Controllers
{
    public class StudentController : Controller
    {
        private LitProReadEntities db = new LitProReadEntities();

        [HttpPost]
        public JsonResult MatchSList(int SID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);

                int matchSCount = 0;
                IEnumerable<PairViewModel> query = null;
                if (SID > 0)
                {
                    query = db.GetMatchedTutorForStudent(SID, jtPageSize, jtStartIndex, jtSorting, ref matchSCount);

                    //var matchSCount = query.Count();
                    //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
                    return Json(new { Result = "OK", Records = query, TotalRecordCount = matchSCount });
                }
                else
                {
                    return Json(new { Result = "OK", Records = query, TotalRecordCount = matchSCount });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateMatchS(PairViewModel pairVm)
        {
            //try
            //{
            //    Thread.Sleep(200);

            //    //IEnumerable<PairViewModel> query = db.GetMatchedTutorForStudent(SID, jtPageSize, jtStartIndex);

            //    //var matchSCount = query.Count();
            //    //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
            //    return Json(new { Result = "OK", Record = pairVm });    //new PairViewModel() });
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Matched Tutor form is not valid! Please correct it and try again." });
                }

                var addedPair = new Pair();
                pairVm.SetTo(addedPair, true);
                db.Pairs.Add(addedPair);
                db.SaveChanges();
                //var addedPairHour = pairVm; // _repository.StudentRepository.AddStudent(student);
                return Json(new { Result = "OK", Record = pairVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message + " (maybe the same Tutor?" });
            }
        }

        [HttpPost]
        public JsonResult UpdateMatchS(PairViewModel pairVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Match Tutor form is not valid! Please correct it and try again." });
                }

                var editPair = db.Pairs.FirstOrDefault(p => p.UniqID == pairVm.UniqID);
                if (editPair == null)
                {
                    return Json(new { Result = "ERROR", Message = "Can't locate the Matched Tutor record (" + pairVm.UniqID.ToString() + ")" });
                }

                pairVm.SetTo(editPair, false);

                db.Configuration.ValidateOnSaveEnabled = true;
                db.Entry(editPair).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
                catch (OptimisticConcurrencyException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteMatchS(int UniqID)
        {
            try
            {
                Thread.Sleep(50);
                Pair pair = db.Pairs.FirstOrDefault(p => p.UniqID == UniqID);
                if (pair == null)
                    return Json(new { Result = "ERROR", Message = "can't delete Match Tutor " + UniqID.ToString() });

                db.Pairs.Remove(pair);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //
        // GET: /Student/Delete/5

        //*** Match-S ***
        [HttpPost]
        public JsonResult MatchSPairsList(int SID, int TID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);

                int count = 0;
                IEnumerable<PairHoursViewModel> query = db.GetPairHoursForStudentAndTutor(SID, TID, jtPageSize, jtStartIndex, jtSorting, ref count);

                //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = query, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateMatchSPairs(PairHoursViewModel pairHourVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Pair Activity form is not valid! Please correct it and try again." });
                }

                var addedPairHour = new PairHour();
                pairHourVm.SetTo(addedPairHour, true);
                db.PairHours.Add(addedPairHour);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = pairHourVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateMatchSPairs(PairHoursViewModel pairHourVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Pair Activity form is not valid! Please correct it and try again." });
                }

                var editPairHour = db.PairHours.FirstOrDefault(p => p.UniqID == pairHourVm.UniqID);
                if (editPairHour == null)
                {
                    return Json(new { Result = "ERROR", Message = "Can't locate the Pair Activity record (" + pairHourVm.UniqID.ToString() + ")" });
                }

                pairHourVm.SetTo(editPairHour, false);

                db.Configuration.ValidateOnSaveEnabled = true;
                db.Entry(editPairHour).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
                catch (OptimisticConcurrencyException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteMatchSPairs(int UniqID)
        {
            try
            {
                Thread.Sleep(50);
                PairHour pair = db.PairHours.FirstOrDefault(p => p.UniqID == UniqID);
                if (pair == null)
                    return Json(new { Result = "ERROR", Message = "can't delete Pair Activity " + UniqID.ToString() });

                db.PairHours.Remove(pair);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //*** Student Children ***
        [HttpPost]
        public JsonResult GetStudentChildren(int studentId)
        {
            try
            {
                Thread.Sleep(200);

                if (studentId > 0)
                {
                    IEnumerable<StudentChildrenViewModel> query = db.GetStudentChildren(studentId);
                    var count = query.Count();
                    return Json(new { Result = "OK", Records = query, TotalRecordCount = count });
                }
                else
                {
                    List<StudentCommentsViewModel> list = null;
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = 0 });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStudentChildren(StudentChildrenViewModel studentChildrenVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Children form is not valid! Please correct it and try again." });
                }

                var addedStudentChildren = new StudentChildren();
                studentChildrenVm.SetTo(addedStudentChildren, true);
                db.StudentChildrens.Add(addedStudentChildren);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = studentChildrenVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message + " (maybe the same Tutor?" });
            }
        }

        [HttpPost]
        public JsonResult UpdateStudentChildren(StudentChildrenViewModel studentChildrenVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Children form is not valid! Please correct it and try again." });
                }

                var editStudentChildren = db.StudentChildrens.FirstOrDefault(p => p.AutoNum == studentChildrenVm.AutoNum);
                if (editStudentChildren == null)
                {
                    return Json(new { Result = "ERROR", Message = "Can't locate the Student Children record (" + studentChildrenVm.AutoNum.ToString() + ")" });
                }

                studentChildrenVm.SetTo(editStudentChildren, false);

                db.Configuration.ValidateOnSaveEnabled = true;
                db.Entry(editStudentChildren).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
                catch (OptimisticConcurrencyException ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteStudentChildren(int AutoNum)
        {
            try
            {
                Thread.Sleep(50);
                StudentChildren item = db.StudentChildrens.FirstOrDefault(p => p.AutoNum == AutoNum);
                if (item == null)
                    return Json(new { Result = "ERROR", Message = "can't delete Student Children " + AutoNum.ToString() });

                db.StudentChildrens.Remove(item);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //*** Student Comments ***
        [HttpPost]
        public JsonResult GetStudentComments(int studentId)
        {
            try
            {
                Thread.Sleep(200);
                if (studentId > 0)
                {
                    var query = Session["StudentCommentsList"] as List<StudentCommentsViewModel>;
                    var sortedQuery = query.OrderByDescending(c => c.CommentDate).ToList();
                    var count = sortedQuery.Count();
                    return Json(new { Result = "OK", Records = sortedQuery, TotalRecordCount = count });
                }
                else
                {
                    List<StudentCommentsViewModel> list = null;
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = 0 });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStudentComments(StudentCommentsViewModel studentCommentsVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Comments form is not valid! Please correct it and try again." });
                }

                //var addedStudentComments = new StudentComment();
                //studentCommentsVm.SetTo(addedStudentComments, true);

                //string insertCmd = "INSERT INTO StudentComments (ID, CommentDate, Comment) VALUES (" +
                //                    "'" + addedStudentComments.ID + "'," +
                //                    "'" + addedStudentComments.CommentDate + "'," +
                //                    "'" + addedStudentComments.Comment + "')";  // +                                    "'" + addedStudentComments.SSMA_TimeStamp + "')";
                //db.Database.ExecuteSqlCommand(insertCmd);



                //db.StudentComments.Add(addedStudentComments);
                //db.SaveChanges();

                // add to the in-memory list
                var query = Session["StudentCommentsList"] as List<StudentCommentsViewModel>;
                if (query == null)
                {
                    query = new List<StudentCommentsViewModel>();
                    Session["StudentCommentsList"] = query;
                }
                studentCommentsVm.Index = query.Count();
                studentCommentsVm.New = true;
                query.Add(studentCommentsVm);
                return Json(new { Result = "OK", Record = studentCommentsVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message});
            }
        }

        [HttpPost]
        public JsonResult UpdateStudentComments(StudentCommentsViewModel studentCommentsVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Comment form is not valid! Please correct it and try again." });
                }

                //var editStudentComment = db.StudentComments.FirstOrDefault(p => p.ID == studentCommentsVm.ID);
                //if (editStudentComment == null)
                //{
                //    return Json(new { Result = "ERROR", Message = "Can't locate the Student Comment record (" + studentCommentsVm.ID.ToString() + ")" });
                //}

                //studentCommentsVm.SetTo(editStudentComment, false);

                //db.Configuration.ValidateOnSaveEnabled = true;
                //db.Entry(editStudentComment).State = EntityState.Modified;

                //try
                //{
                //    //string updateCmd = "UPDATE Products SET UnitPrice = UnitPrice + 1.00";
                //    //string updateCmd = "UPDATE StudentComments SET (CommentDate, Comment) VALUES (" +
                //    //                    "'" + editStudentComment.ID + "'," +
                //    //                    "'" + editStudentComment.CommentDate + "'," +
                //    //                    "'" + editStudentComment.Comment + "')";  // +                                    "'" + addedStudentComments.SSMA_TimeStamp + "')";
                //    //db.Database.ExecuteSqlCommand(updateCmd);
                //    db.SaveChanges();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    return Json(new { Result = "ERROR", Message = ex.Message });
                //}
                //catch (OptimisticConcurrencyException ex)
                //{
                //    return Json(new { Result = "ERROR", Message = ex.Message });
                //}

                var query = Session["StudentCommentsList"] as List<StudentCommentsViewModel>;
                foreach (var item in query)
                {
                    if (item.Index == studentCommentsVm.Index)
                    {
                        item.Comment = studentCommentsVm.Comment;
                        item.CommentDate = studentCommentsVm.CommentDate;
                        break;
                    }
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // NOT being used
        [HttpPost]
        public JsonResult DeleteStudentComments(int ID)
        {
            try
            {
                Thread.Sleep(50);
                StudentComment item = db.StudentComments.FirstOrDefault(p => p.ID == ID);
                if (item == null)
                    return Json(new { Result = "ERROR", Message = "can't delete Student Comment " + ID.ToString() });

                db.StudentComments.Remove(item);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // the table StudentComment has no keys so it's not easy to Insert/Update using LINQ.
        // the work-around here is to delete all exsiting records and insert the new ones.
        private void SaveStudentComments(List<StudentCommentsViewModel> comments)
        {
            if (comments == null || comments.Count() == 0)
                return;

            int studentId = (int)comments.FirstOrDefault().ID;
            try
            {
                // delete
                string delCmd = "delete from dbo.StudentComments where ID = " + studentId.ToString();
                db.Database.ExecuteSqlCommand(delCmd);   
       
                // insert
                foreach (var item in comments)
                {
                    string insertCmd = "INSERT INTO StudentComments (ID, CommentDate, Comment) VALUES (" +
                                        "'" + item.ID + "'," +
                                        "'" + item.CommentDate + "'," +
                                        "'" + item.Comment + "')";
                    db.Database.ExecuteSqlCommand(insertCmd);
                }
            }
            catch (Exception ex)
            {
            
            }
        }

        //*** Student FollowUp ***
        [HttpPost]
        public JsonResult GetStudentFollowUps(int studentId, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);

                if (studentId > 0)
                {
                    var query = Session["StudentFollowUpsList"] as List<StudentFollowUpViewModel>;
                    var sortedQuery = query.OrderByDescending(f => f.FollowUpDate).ToList();
                    var count = sortedQuery.Count();
                    return Json(new { Result = "OK", Records = sortedQuery, TotalRecordCount = count });
                }
                else
                {
                    List<StudentCommentsViewModel> list = null;
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = 0 });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStudentFollowUp(StudentFollowUpViewModel studentFollowUpVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Follow Up form is not valid! Please correct it and try again." });
                }

                // add to the in-memory list
                var query = Session["StudentFollowUpsList"] as List<StudentFollowUpViewModel>;
                if (query == null)
                {
                    query = new List<StudentFollowUpViewModel>();
                    Session["StudentFollowUpsList"] = query;
                }
                studentFollowUpVm.New = true;
                query.Add(studentFollowUpVm);
                return Json(new { Result = "OK", Record = studentFollowUpVm });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateStudentFollowUp(StudentFollowUpViewModel studentFollowUpVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Student Follow Up form is not valid! Please correct it and try again." });
                }

                var query = Session["StudentFollowUpsList"] as List<StudentFollowUpViewModel>;
                foreach (var item in query)
                {
                    if (item.UniqID == studentFollowUpVm.UniqID)
                    {
                        item.ID = studentFollowUpVm.ID;
                        item.FollowUpDate = studentFollowUpVm.FollowUpDate;
                        item.FollowUpDesc = studentFollowUpVm.FollowUpDesc;
                        item.DateCreated = studentFollowUpVm.DateCreated;
                        item.DateModified = studentFollowUpVm.DateModified;
                        item.LastModifiedBy = studentFollowUpVm.LastModifiedBy;
                        break;
                    }
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // go thru the list and either Add or Update based on record's filed "New" 
        private void SaveStudentFollowUps(List<StudentFollowUpViewModel> followups)
        {
            if (followups == null || followups.Count() == 0)
                return;

            int studentId = (int)followups.FirstOrDefault().ID;
            try
            {
                foreach (var item in followups)
                {
                    var followUp = new StudentFollowUp();
                    item.SetTo(followUp, item.New);

                    if (item.New)
                    {
                        db.StudentFollowUps.Add(followUp);
                    }
                    else
                    {
                        db.Configuration.ValidateOnSaveEnabled = true;
                        db.Entry(followUp).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            
            }
        }

        //********************************************************************************************
        //
        // GET: /Student/Delete/5
        //
        // GET: /Student/

        public ActionResult Index(int Id = -1)
        {
            if (Id == -1)
            {
                var vm = new StudentFormViewModel();
                Session["StudentCommentsList"] = vm.StudentCommentsList;
                Session["StudentFollowUpsList"] = vm.StudentFollowUpsList;
                return View("Index", vm);
            }
            else
            {
                var vm = new StudentFormViewModel();
                if (vm == null || vm.CurrentStudent == null)
                {
                    return HttpNotFound();
                }
                vm.Load(Id);
                return View("Index", vm);
            }
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Student/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public JsonResult Add(IList studentvm)//int id = 0)
        {
//            StudentFormViewModel s = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<StudentFormViewModel>(studentvm);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Student/Edit/5
        public ActionResult Edit(int id = -1)      //)JsonResult --- string studentvm
        {
            //StudentFormViewModel s = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<StudentFormViewModel>(studentvm);
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //var response = new Response(true, "Contact Successfully Submitted");
            //return Json(response);
            //if (id == 0 || id == -1)
            
            if (id == -1)
            {
                StudentFormViewModel newVm = new StudentFormViewModel();
                return View("Index", newVm);
            }

            var vm = new StudentFormViewModel();
            if (vm == null || vm.CurrentStudent == null)
            {
                return View("Index", new StudentFormViewModel());
            }

            vm.Load(id);
            if (vm.CurrentStudent != null && vm.CurrentStudent.ID <= 0)
            {
                return View("Index", new StudentFormViewModel());
            }

            Session["StudentCommentsList"] = vm.StudentCommentsList;
            Session["StudentFollowUpsList"] = vm.StudentFollowUpsList;

            JsonResult jsonData = new JsonResult();
            jsonData.Data = vm;
            jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonData;
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(StudentFormViewModel studentFormVm, string EditMode)//string dataO)
        {
            //string editMode = studentFormVm.
            if (ModelState.IsValid)
            {
                using (LitProReadEntities db = new LitProReadEntities())
                {
                    if (studentFormVm.EditMode == "edit")
                    {
                        db.Configuration.ValidateOnSaveEnabled = true;
                        db.Entry(studentFormVm.CurrentStudent).State = EntityState.Modified;

                        try
                        {
                            db.SaveChanges();

                            // student comments
                            var comments = Session["StudentCommentsList"] as List<StudentCommentsViewModel>;
                            SaveStudentComments(comments);

                            // student followups
                            var followups = Session["StudentFollowUpsList"] as List<StudentFollowUpViewModel>;
                            SaveStudentFollowUps(followups);
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
                    else
                    {
                        // "add"

                        // check for dup here (using name + dob)
                        Student student = db.Students.FirstOrDefault(p => p.LastName == studentFormVm.CurrentStudent.LastName &&
                                                                          p.FirstName == studentFormVm.CurrentStudent.FirstName &&
                                                                          p.DOB == studentFormVm.CurrentStudent.DOB);
                        if (student != null)
                        {
                            //ModelState.AddModelError("CustomError", "Duplicate Student");
                            TempData["SaveEror"] = "Cannot Save: Duplicate Student";
                            return View("Index", studentFormVm);
                        }

 
                        try
                        {
                            db.Students.Add(studentFormVm.CurrentStudent);
                            db.SaveChanges();

                            // student comments
                            var comments = Session["StudentCommentsList"] as List<StudentCommentsViewModel>;
                            SaveStudentComments(comments);

                            // student followups
                            var followups = Session["StudentFollowUpsList"] as List<StudentFollowUpViewModel>;
                            SaveStudentFollowUps(followups);

                            // change from "add" to "edit"
                            //studentFormVm.EditMode = "edit";
                            return RedirectToAction("Index", new { Id = studentFormVm.CurrentStudent.ID });
                        }
                        catch (Exception ex)
                        {
                            string err = ex.Message;
                            int i = 0;
                            i++;
                            //studentFormVm.db.Refresh(RefreshMode.ClientWins, studentFormVm.CurrentStudent);
                            //studentFormVm.db.SaveChanges();
                        }
                   }
                }
            }
            return View("Index", studentFormVm);

            //JsonResult jsonData = new JsonResult();
            //jsonData.Data = studentFormVm;
            //jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            ////return jsonData;
            //return Json(new { msg = "Successfully saved " + studentFormVm.CurrentStudent.LastName });
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            try {
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                int i = 0;
                i++;
                //return Json(new { Result = "ERROR", Message = ex.Message });
            }
            return View(student);
        }


        //
        // POST: /Student/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Student student = db.Students.Find(id);
        //    db.Students.Remove(student);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}