using LitProRead.Models;
using LitProRead.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Controllers
{
    public class TutorController : Controller
    {
        private LitProReadEntities db = new LitProReadEntities();

        //
        // GET: /Student/

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

    }
}
