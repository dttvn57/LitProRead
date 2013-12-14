using LitProRead.Models;
using LitProRead.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            return View(vm);
        }
    }
}
