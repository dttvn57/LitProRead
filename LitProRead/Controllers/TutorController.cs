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
                //vm.studentVM = new StudentViewModel();
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
        public ActionResult Edit(int id = -1)
        {
            var vm = new TutorFormViewModel();
            if (vm == null || vm.CurrentTutor == null)
            {
                return HttpNotFound();
            }
            vm.Load(id);
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
    }
}
