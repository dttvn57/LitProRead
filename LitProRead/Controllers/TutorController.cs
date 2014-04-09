using LitProRead.Models;
using LitProRead.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Controllers
{
    public class TutorController : Controller
    {
        private LitProReadEntities db = new LitProReadEntities();

        //
        // GET: /Tutor/

        public ActionResult Index(int Id = -1)
        {
            if (Id == -1)
            {
                var vm = new TutorFormViewModel();
                //vm.TutorVM = new TutorViewModel();
                return View("Index", vm);
            }
            else
            {
                var vm = new TutorFormViewModel();
                if (vm == null || vm.CurrentTutor == null)
                {
                    return HttpNotFound();
                }
                vm.Load(Id);
                return View("Index", vm);
            }
        }

        //
        // GET: /Tutor/Edit/5
        public ActionResult Edit(int id, bool activeOnly, bool byLastName)
        {
            if (id == -1)
            {
                TutorFormViewModel newVm = new TutorFormViewModel();
                return View("Index", newVm);
            }

            var vm = new TutorFormViewModel();
            if (vm == null || vm.CurrentTutor == null)
            {
                return View("Index", new TutorFormViewModel());
            }

            vm.Load(id, activeOnly, byLastName);

            if (vm.CurrentTutor != null && vm.CurrentTutor.ID <= 0)
            {
                return View("Index", new TutorFormViewModel());
            }
            //vm.CurrentRecordIndex = recIndex;

//            Session["TutorCommentsList"] = vm.TutorCommentsList;
//            Session["TutorFollowUpsList"] = vm.TutorFollowUpsList;

            JsonResult jsonData = new JsonResult();
            jsonData.Data = vm;
            jsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonData;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(TutorFormViewModel tutorFormVm)
        {
            if (ModelState.IsValid)
            {
                using (LitProReadEntities db = new LitProReadEntities())
                {
                    db.Configuration.ValidateOnSaveEnabled = true;
                    db.Entry(tutorFormVm.CurrentTutor).State = EntityState.Modified;

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
            }
            return View("Index", tutorFormVm);
        }

        // passed-in recIndex is 1-based => convert to 0-based first before using.
        public ActionResult TutorByRecIndex(int recIndex, bool activeOnly, bool byLastName)
        {
            int TutorId = 0;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                TutorId = db.TutorIdByRecIndex(--recIndex, activeOnly, byLastName);
            }
            return Edit(TutorId, activeOnly, byLastName);
        }

        public ActionResult FirstTutor(int currTutorId, bool activeOnly, bool byLastName)
        {
            int recIndex = 0;
            int TutorId = currTutorId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                TutorId = db.FirstTutorId(currTutorId, activeOnly, byLastName, ref recIndex);
            }
            return Edit(TutorId, activeOnly, byLastName);
            //            return Edit(TutorId, recIndex, activeOnly);
        }

        public ActionResult LastTutor(int currTutorId, bool activeOnly, bool byLastName)
        {
            int recIndex = 0;
            int TutorId = currTutorId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                TutorId = db.LastTutorId(currTutorId, activeOnly, byLastName, ref recIndex);
            }
            return Edit(TutorId, activeOnly, byLastName);
            //return Edit(TutorId, recIndex, activeOnly);
        }

        public ActionResult PrevTutor(int currTutorId, bool activeOnly, bool byLastName)
        {
            int recIndex = 0;
            int prevTutorId = currTutorId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                prevTutorId = db.PrevTutorId(currTutorId, activeOnly, byLastName, ref recIndex);
            }
            return Edit(prevTutorId, activeOnly, byLastName);
            //return Edit(prevTutorId, recIndex, activeOnly);
        }

        public ActionResult NextTutor(int currTutorId, bool activeOnly, bool byLastName)
        {
            int recIndex = 0;
            int nextTutorId = currTutorId;
            using (LitProReadEntities db = new LitProReadEntities())
            {
                nextTutorId = db.NextTutorId(currTutorId, activeOnly, byLastName, ref recIndex);
            }
            return Edit(nextTutorId, activeOnly, byLastName);
            //return Edit(nextTutorId, recIndex, activeOnly);
        }

    }
}
