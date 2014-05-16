using LitProRead.Helpers;
using LitProRead.Models;
using LitProRead.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Controllers
{
    public class AllPairsController : Controller
    {
        //private LitProReadEntities db = new LitProReadEntities();


        [HttpGet]
        //public ActionResult GetAllPairsTutor(string searchTerm, int pageSize, int pageNum)
        //{
        //    LitProReadEntities db = new LitProReadEntities();
        //    List<PairViewModel> allPairs = db.GetAllPairs();

        //    List<Tutor> tutors = db.GetAllPairsTutors(searchTerm, pageSize, pageNum);
        //    int count = db.GetAllPairsCount(searchTerm, pageSize, pageNum);

        //    return GetTutorsQuery(ActiveOnly, searchTerm, byLastName)
        //        .Skip(pageSize * (pageNum - 1))
        //        .Take(pageSize)
        //        .ToList();


        //    //Translate the attendees into a format the select2 dropdown expects
        //    Select2PagedResult pagedAllPairs = ToSelect2Format(allPairs, count);

        //    //Return the data as a jsonp result
        //    return new JsonpResult
        //    {
        //        Data = pagedAllPairs,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}

        // GET: /AllPairs/

        public ActionResult Index(int Id = -1)
        {
            if (Id == -1)
            {
                var vm = new AllPairsFormViewModel();
                return View("Index", vm);
            }
            else
            {
                var vm = new AllPairsFormViewModel();
                if (vm == null || vm.CurrentPair == null)
                {
                    return HttpNotFound();
                }
                vm.Load(Id);
                return View("Index", vm);
            }
        }

        [HttpPost]
        public JsonResult PairHours(int UniqID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                try
                {
                    Thread.Sleep(200);

                    int count = 0;
                    IEnumerable<PairHoursViewModel> query = db.GetPairHours(UniqID, jtPageSize, jtStartIndex, jtSorting, ref count);

                    //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
                    return Json(new { Result = "OK", Records = query, TotalRecordCount = count });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
        }

        //[HttpPost]
        //public JsonResult CreateMatchT(PairViewModel pairVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Matched Student form is not valid! Please correct it and try again." });
        //        }

        //        var addedPair = new Pair();
        //        pairVm.SetTo(addedPair, true);
        //        db.Pairs.Add(addedPair);
        //        db.SaveChanges();
        //        return Json(new { Result = "OK", Record = pairVm });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message + " (maybe the same Student?" });
        //    }
        //}

        //[HttpPost]
        //public JsonResult UpdateMatchT(PairViewModel pairVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Matched Student form is not valid! Please correct it and try again." });
        //        }

        //        var editPair = db.Pairs.FirstOrDefault(p => p.UniqID == pairVm.UniqID);
        //        if (editPair == null)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Can't locate the Matched Student record (" + pairVm.UniqID.ToString() + ")" });
        //        }

        //        pairVm.SetTo(editPair, false);

        //        db.Configuration.ValidateOnSaveEnabled = true;
        //        db.Entry(editPair).State = EntityState.Modified;

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            return Json(new { Result = "ERROR", Message = ex.Message });
        //        }
        //        catch (OptimisticConcurrencyException ex)
        //        {
        //            return Json(new { Result = "ERROR", Message = ex.Message });
        //        }

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteMatchT(int UniqID)
        //{
        //    try
        //    {
        //        Thread.Sleep(50);
        //        Pair pair = db.Pairs.FirstOrDefault(p => p.UniqID == UniqID);
        //        if (pair == null)
        //            return Json(new { Result = "ERROR", Message = "can't delete Matched Student " + UniqID.ToString() });

        //        db.Pairs.Remove(pair);
        //        db.SaveChanges();
        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        ////*** Match-S ***
        //[HttpPost]
        //public JsonResult MatchTPairsList(int SID, int TID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        Thread.Sleep(200);

        //        int count = 0;
        //        IEnumerable<PairHoursViewModel> query = db.GetPairHoursForStudentAndTutor(SID, TID, jtPageSize, jtStartIndex, jtSorting, ref count);

        //        //var matchSes = query.Where(p => p.SID == SID);//"TRUNG", jtPageSize, jtStartIndex, true);// db.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);
        //        return Json(new { Result = "OK", Records = query, TotalRecordCount = count });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreateMatchTPairs(PairHoursViewModel pairHourVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Pair Activity form is not valid! Please correct it and try again." });
        //        }

        //        var addedPairHour = new PairHour();
        //        pairHourVm.SetTo(addedPairHour, true);
        //        db.PairHours.Add(addedPairHour);
        //        db.SaveChanges();
        //        return Json(new { Result = "OK", Record = pairHourVm });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult UpdateMatchTPairs(PairHoursViewModel pairHourVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Pair Activity form is not valid! Please correct it and try again." });
        //        }

        //        var editPairHour = db.PairHours.FirstOrDefault(p => p.UniqID == pairHourVm.UniqID);
        //        if (editPairHour == null)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Can't locate the Pair Activity record (" + pairHourVm.UniqID.ToString() + ")" });
        //        }

        //        pairHourVm.SetTo(editPairHour, false);

        //        db.Configuration.ValidateOnSaveEnabled = true;
        //        db.Entry(editPairHour).State = EntityState.Modified;

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            return Json(new { Result = "ERROR", Message = ex.Message });
        //        }
        //        catch (OptimisticConcurrencyException ex)
        //        {
        //            return Json(new { Result = "ERROR", Message = ex.Message });
        //        }

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteMatchTPairs(int UniqID)
        //{
        //    try
        //    {
        //        Thread.Sleep(50);
        //        PairHour pair = db.PairHours.FirstOrDefault(p => p.UniqID == UniqID);
        //        if (pair == null)
        //            return Json(new { Result = "ERROR", Message = "can't delete Pair Activity " + UniqID.ToString() });

        //        db.PairHours.Remove(pair);
        //        db.SaveChanges();
        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        ////*** Tutor Comments ***
        //[HttpPost]
        //public JsonResult GetTutorComments(int tutorId)
        //{
        //    try
        //    {
        //        Thread.Sleep(200);
        //        if (tutorId > 0)
        //        {
        //            var query = Session["TutorCommentsList"] as List<TutorCommentsViewModel>;
        //            var sortedQuery = query.OrderByDescending(c => c.CommentDate).ToList();
        //            var count = sortedQuery.Count();
        //            return Json(new { Result = "OK", Records = sortedQuery, TotalRecordCount = count });
        //        }
        //        else
        //        {
        //            List<TutorCommentsViewModel> list = null;
        //            return Json(new { Result = "OK", Records = list, TotalRecordCount = 0 });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreateTutorComments(TutorCommentsViewModel tutorCommentsVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Tutor Comments form is not valid! Please correct it and try again." });
        //        }

        //        // add to the in-memory list
        //        var query = Session["TutorCommentsList"] as List<TutorCommentsViewModel>;
        //        if (query == null)
        //        {
        //            query = new List<TutorCommentsViewModel>();
        //            Session["TutorCommentsList"] = query;
        //        }
        //        tutorCommentsVm.Index = query.Count();
        //        tutorCommentsVm.New = true;
        //        query.Add(tutorCommentsVm);
        //        return Json(new { Result = "OK", Record = tutorCommentsVm });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult UpdateTutorComments(TutorCommentsViewModel tutorCommentsVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Tutor Comment form is not valid! Please correct it and try again." });
        //        }

        //        var query = Session["TutorCommentsList"] as List<TutorCommentsViewModel>;
        //        foreach (var item in query)
        //        {
        //            if (item.Index == tutorCommentsVm.Index)
        //            {
        //                item.Comment = tutorCommentsVm.Comment;
        //                item.CommentDate = tutorCommentsVm.CommentDate;
        //                break;
        //            }
        //        }
        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //// NOT being used
        //[HttpPost]
        //public JsonResult DeleteTutorComments(int ID)
        //{
        //    try
        //    {
        //        Thread.Sleep(50);
        //        TutorComment item = db.TutorComments.FirstOrDefault(p => p.ID == ID);
        //        if (item == null)
        //            return Json(new { Result = "ERROR", Message = "can't delete Tutor Comment " + ID.ToString() });

        //        db.TutorComments.Remove(item);
        //        db.SaveChanges();
        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //// the table TutorComment has no keys so it's not easy to Insert/Update using LINQ.
        //// the work-around here is to delete all exsiting records and insert the new ones.
        //private void SaveTutorComments(List<TutorCommentsViewModel> comments)
        //{
        //    if (comments == null || comments.Count() == 0)
        //        return;

        //    using (var scope = new TransactionScope())
        //    {
        //        int tutorId = (int)comments.FirstOrDefault().ID;

        //        // delete
        //        string delCmd = "delete from dbo.TutorComments where ID = " + tutorId.ToString();
        //        db.Database.ExecuteSqlCommand(delCmd);

        //        // insert
        //        foreach (var item in comments)
        //        {
        //            string insertCmd = "INSERT INTO TutorComments (ID, CommentDate, Comment) VALUES (" +
        //                                "'" + item.ID + "'," +
        //                                "'" + item.CommentDate + "'," +
        //                                "'" + item.Comment + "')";
        //            db.Database.ExecuteSqlCommand(insertCmd);
        //        }

        //        //transaction completed successfully, both calls succeeded
        //        scope.Complete();
        //    }
        //}


        ////*** Tutor FollowUp ***
        //[HttpPost]
        //public JsonResult GetTutorFollowUps(int tutorId, string jtSorting = null)
        //{
        //    try
        //    {
        //        Thread.Sleep(200);

        //        if (tutorId > 0)
        //        {
        //            var query = Session["TutorFollowUpsList"] as List<TutorFollowUpViewModel>;
        //            var sortedQuery = query.OrderByDescending(f => f.FollowUpDate).ToList();
        //            var count = sortedQuery.Count();
        //            return Json(new { Result = "OK", Records = sortedQuery, TotalRecordCount = count });
        //        }
        //        else
        //        {
        //            List<TutorCommentsViewModel> list = null;
        //            return Json(new { Result = "OK", Records = list, TotalRecordCount = 0 });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult CreateTutorFollowUp(TutorFollowUpViewModel tutorFollowUpVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Tutor Follow Up form is not valid! Please correct it and try again." });
        //        }

        //        // add to the in-memory list
        //        var query = Session["TutorFollowUpsList"] as List<TutorFollowUpViewModel>;
        //        if (query == null)
        //        {
        //            query = new List<TutorFollowUpViewModel>();
        //            Session["TutorFollowUpsList"] = query;
        //        }
        //        tutorFollowUpVm.New = true;
        //        query.Add(tutorFollowUpVm);
        //        return Json(new { Result = "OK", Record = tutorFollowUpVm });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult UpdateTutorFollowUp(TutorFollowUpViewModel tutorFollowUpVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Result = "ERROR", Message = "Tutor Follow Up form is not valid! Please correct it and try again." });
        //        }

        //        var query = Session["TutorFollowUpsList"] as List<TutorFollowUpViewModel>;
        //        foreach (var item in query)
        //        {
        //            if (item.UniqID == tutorFollowUpVm.UniqID)
        //            {
        //                item.ID = tutorFollowUpVm.ID;
        //                item.FollowUpDate = tutorFollowUpVm.FollowUpDate;
        //                item.FollowUpDesc = tutorFollowUpVm.FollowUpDesc;
        //                item.DateCreated = tutorFollowUpVm.DateCreated;
        //                item.DateModified = tutorFollowUpVm.DateModified;
        //                item.LastModifiedBy = tutorFollowUpVm.LastModifiedBy;
        //                break;
        //            }
        //        }
        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        //// go thru the list and either Add or Update based on record's filed "New" 
        //private void SaveTutorFollowUps(List<TutorFollowUpViewModel> followups)
        //{
        //    if (followups == null || followups.Count() == 0)
        //        return;

        //    int tutorId = (int)followups.FirstOrDefault().ID;
        //    try
        //    {
        //        foreach (var item in followups)
        //        {
        //            var followUp = new TutorFollowUp();
        //            item.SetTo(followUp, item.New);

        //            if (item.New)
        //            {
        //                db.TutorFollowUps.Add(followUp);
        //            }
        //            else
        //            {
        //                db.Configuration.ValidateOnSaveEnabled = true;
        //                db.Entry(followUp).State = EntityState.Modified;
        //            }
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        ////********************************************************************************************
        ////
        //// GET: /Tutor/

        //public ActionResult Index(int Id = -1)
        //{
        //    if (Id == -1)
        //    {
        //        var vm = new TutorFormViewModel();
        //        Session["TutorCommentsList"] = vm.TutorCommentsList;
        //        Session["TutorFollowUpsList"] = vm.TutorFollowUpsList;
        //        return View("Index", vm);
        //    }
        //    else
        //    {
        //        var vm = new TutorFormViewModel();
        //        if (vm == null || vm.CurrentTutor == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        vm.Load(Id);
        //        return View("Index", vm);
        //    }
        //}

        //
        // GET: /AllPairs/
        public ActionResult Edit(int id)
        {
            if (id == -1)
            {
                AllPairsFormViewModel newVm = new AllPairsFormViewModel();
                return View("Index", newVm);
            }

            var vm = new AllPairsFormViewModel();
            if (vm == null || vm.CurrentPair == null)
            {
                return View("Index", new AllPairsFormViewModel());
            }

            vm.Load(id);

            if (vm.CurrentPair != null && vm.CurrentPair.UniqID <= 0)
            {
                return View("Index", new TutorFormViewModel());
            }
            //vm.CurrentRecordIndex = recIndex;

            //Session["TutorCommentsList"] = vm.TutorCommentsList;
            //Session["TutorFollowUpsList"] = vm.TutorFollowUpsList;

            JsonResult jsonData = new JsonResult();
            jsonData.Data = vm;
            jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonData;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(AllPairsFormViewModel allPairsFormVm)
        {
            if (ModelState.IsValid)
            {
                using (LitProReadEntities db = new LitProReadEntities())
                {
                    if (allPairsFormVm.EditMode == "edit")
                    {
                        db.Configuration.ValidateOnSaveEnabled = true;

                        // copy from PairViewModel to a Pair in order to save this Pair
                        Pair pairToSave = new Pair();
                        allPairsFormVm.CurrentPair.CopyTo(pairToSave);

                        db.Entry(pairToSave).State = EntityState.Modified;
//                        db.Entry(allPairsFormVm.CurrentPair).State = EntityState.Modified;

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
                        }
                    }
                    //else if (allPairsFormVm.EditMode == "add")
                    //{
                    //    // "add"

                    //    // check for dup here (using name + dob)
                    //    Tutor tutor = db.Tutors.FirstOrDefault(p => p.LastName == allPairsFormVm.CurrentTutor.LastName &&
                    //                                                      p.FirstName == allPairsFormVm.CurrentTutor.FirstName &&
                    //                                                      p.DOB == allPairsFormVm.CurrentTutor.DOB);
                    //    if (tutor != null)
                    //    {
                    //        TempData["SaveEror"] = "Cannot Save: Duplicate Tutor";
                    //        return View("Index", allPairsFormVm);
                    //    }


                    //    try
                    //    {
                    //        db.Tutors.Add(allPairsFormVm.CurrentTutor);
                    //        db.SaveChanges();
                    //        return RedirectToAction("Index", new { Id = allPairsFormVm.CurrentPair.UniqID });
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        string err = ex.Message;
                    //        int i = 0;
                    //        i++;
                    //        //studentFormVm.db.Refresh(RefreshMode.ClientWins, studentFormVm.CurrentStudent);
                    //        //studentFormVm.db.SaveChanges();
                    //    }
                    //}
                    else
                    {
                        return View("Index", allPairsFormVm);
                    }
                }
            }
            return View("Index", allPairsFormVm);
        }

        // passed-in recIndex is 1-based => convert to 0-based first before using.
        public ActionResult PairByRecIndex(int recIndex)
        {
            int Id = 0;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Id = db.PairIdByRecIndex(--recIndex);
            }
            return Edit(Id);
        }

        public ActionResult FirstPair(int currPairId)
        {
            int recIndex = 0;
            int Id = currPairId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Id = db.FirstPairId(currPairId, ref recIndex);
            }
            return Edit(Id);
            //            return Edit(TutorId, recIndex, activeOnly);
        }

        public ActionResult LastPair(int currPairId)
        {
            int recIndex = 0;
            int Id = currPairId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Id = db.LastPairId(currPairId, ref recIndex);
            }
            return Edit(Id);
            //return Edit(TutorId, recIndex, activeOnly);
        }

        public ActionResult PrevPair(int currPairId)
        {
            int recIndex = 0;
            int Id = currPairId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Id = db.PrevPairId(currPairId, ref recIndex);
            }
            return Edit(Id);
            //return Edit(prevTutorId, recIndex, activeOnly);
        }

        public ActionResult NextPair(int currPairId)
        {
            int recIndex = 0;
            int Id = currPairId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Id = db.NextPairId(currPairId, ref recIndex);
            }
            return Edit(Id);
            //return Edit(nextTutorId, recIndex, activeOnly);
        }

        //[HttpPost]
        //public ActionResult Delete(int id = 0)
        //{
        //    Tutor tutor = db.Tutors.Find(id);
        //    if (tutor == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    try
        //    {
        //        db.Tutors.Remove(tutor);
        //        db.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        int i = 0;
        //        i++;
        //        //return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //    return View(tutor);
        //}
    }
}
