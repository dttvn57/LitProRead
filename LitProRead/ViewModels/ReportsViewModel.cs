using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    public class ReportsViewModel
    {
        public string SelectedStudentsReport { get; set; }
        public List<SelectListItem> StudentsReport { get; set; }
        //public List<SelectListItem> StudentsReportStatus { get; set; }

        public ReportsViewModel()
        {
        }

        public static List<SelectListItem> GetStudentsReportList(string selected = "")
        {
            // students
            string[] names = ConfigurationManager.AppSettings.AllKeys
                                                            .Where(k => k.StartsWith("StudentReport"))
                                                            .Select(k => ConfigurationManager.AppSettings[k])
                                                            .ToArray();

            string selectedValue = selected != null ? selected.Trim() : "";
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var name in names)
            {
                list.Add(new SelectListItem
                {
                    Text = name.Trim(),
                    Value = name.Trim(),
                    //Selected = selectedValue == sal.Trim() ? true : false
                });

            }
            return list;// new SelectList(list.ToList(), "Value", "Text");
        }

    }
}