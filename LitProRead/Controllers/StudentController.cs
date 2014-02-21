﻿using System;
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

                IEnumerable<PairViewModel> query = db.GetMatchedTutorForStudent(SID, jtPageSize, jtStartIndex);

                var matchSCount = query.Count();
                //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = query, TotalRecordCount = matchSCount });
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

        [HttpPost]
        public JsonResult MatchSPairsList(int SID, int TID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Thread.Sleep(200);
                //SID = 39;
                //TID = 141;
                IEnumerable<PairHoursViewModel> query = db.GetPairHoursForStudentAndTutor(SID, TID, jtPageSize, jtStartIndex);

                var count = query.Count();
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

        //
        // GET: /Student/Delete/5
        //
        // GET: /Student/

        public ActionResult Index(int Id = -1)
        {
            if (Id == -1)
            {
                var vm = new StudentFormViewModel();
                //vm.studentVM = new StudentViewModel();
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
                return View("Index", new StudentFormViewModel());
            }

            var vm = new StudentFormViewModel();
            if (vm == null || vm.CurrentStudent == null)
            {
                return View("Index", new StudentFormViewModel());
            }

            vm.Load(id);
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
                    if (EditMode == "edit")
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
                    else
                    {
                        // "add"
                        try
                        {
                            db.Students.Add(studentFormVm.CurrentStudent);
                            db.SaveChanges();
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

        public ActionResult Delete(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}