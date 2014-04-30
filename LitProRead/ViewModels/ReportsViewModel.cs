﻿using LitProRead.Models;
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

        public string SelectedTutorsReport { get; set; }
        public List<SelectListItem> TutorsReport { get; set; }

        public List<SelectListItem> ChosenMonthList { get; set; }
        public List<SelectListItem> ChosenStatusList { get; set; }

        public string SelectedCity1 { get; set; }
        public string SelectedCity2 { get; set; }
        public string SelectedCity3 { get; set; }
        public string SelectedCity4 { get; set; }
        public string SelectedCity5 { get; set; }
        public List<SelectListItem> CityList { get; private set; }

        public ReportsViewModel()
        {
            ChosenMonthList = GetChosenMonthList();
            ChosenStatusList = GetChosenStatusList();
            this.CityList = GetCityList();
        }

        public List<SelectListItem> GetChosenMonthList(string selected = "")
        {
            return DbHelper.GetChosenMonthList();
        }

        public List<SelectListItem> GetChosenStatusList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var status = db.Database.SqlQuery<string>("SELECT Status FROM dbo.Status").ToList();

                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in status)
                {
                    list.Add(new SelectListItem
                    {
                        Text = item.Trim(),
                        Value = item.Trim(),
                        //Selected = selectedValue == item.Trim() ? true : false
                    });
                }
                return list;
            }
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


        public static List<SelectListItem> GetTutorsReportList(string selected = "")
        {
            // students
            string[] names = ConfigurationManager.AppSettings.AllKeys
                                                            .Where(k => k.StartsWith("TutorReport"))
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

        public List<SelectListItem> GetCityList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var cities = db.Database.SqlQuery<string>("SELECT City FROM dbo.City").ToList();
                return ParseList(selected, cities);// new SelectList(cities);
            }
        }


        private List<SelectListItem> ParseList(string selected, List<string> Items)
        {
            string selectedValue = selected != null ? selected.Trim() : "";
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in Items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Trim(),
                    Value = item.Trim(),
                    //Selected = selectedValue == item.Trim() ? true : false
                });

            }
            return list;
        }
    }
}