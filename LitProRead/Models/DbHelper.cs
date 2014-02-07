﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.Models
{
    public static class DbHelper
    {
        // SELECT DISTINCTROW MailCode.MailCode, MailCode.Description FROM MailCode ORDER BY MailCode.MailCode; 
        public static List<SelectListItem> GetMailCodeList(string[] selectedValues)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var codes = db.Database.SqlQuery<_MailCode>("SELECT MailCode, Description FROM dbo.MailCode ORDER BY MailCode");
                //List<SelectListItem> codeList = new List<SelectListItem>();
                //foreach (var code in codes)
                //{
                //    codeList.Add(new SelectListItem { Value = code.mailcode, Text = code.description });
                //}

                var codeList = from code in codes
                               select new
                               {
                                   MailCode = code.mailcode,
                                   Description = code.description
                               };
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in codeList)
                {
                    list.Add(new SelectListItem
                    {
                        Value = item.MailCode.Trim(),
                        Text = item.Description.Trim(),
                        //Selected = selectedValue == item.Trim() ? true : false
                    });

                }
                return list;
                //return new SelectList(codeList, "MailCode", "Description", selectedValues);
            }
        }

        // SELECT DISTINCTROW Category.Category, Category.Description FROM Category ORDER BY Category.Category; 
        public static List<SelectListItem> GetCategoryList(string selectedValue)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var categories = db.Database.SqlQuery<_Category>("SELECT Category, Description  FROM dbo.Category ORDER BY Category").ToList();
                var catList = from cat in categories
                              select new
                              {
                                  Category = cat.category,
                                  Description = cat.description
                              };
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in catList)
                {
                    list.Add(new SelectListItem
                    {
                        Value = item.Category.Trim(),
                        Text = item.Description.Trim(),
                        //Selected = selectedValue == item.Trim() ? true : false
                    });

                }
                return list;
                //return new SelectList(catList, "Category", "Description", selectedValues);
            }
        }

        // SELECT DISTINCTROW Keyword.Keyword, Keyword.Description FROM Keyword ORDER BY Keyword.Keyword; 
        public static List<SelectListItem> GetKeywordList(string selectedValue)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var kws = db.Database.SqlQuery<_Keyword>("SELECT Keyword, Description  FROM dbo.Keyword ORDER BY Keyword").ToList();
                var kwList = from kw in kws
                             select new
                             {
                                 Keyword = kw.keyword,
                                 Description = kw.description
                             };
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in kwList)
                {
                    list.Add(new SelectListItem
                    {
                        Value = item.Keyword.Trim(),
                        Text = item.Description.Trim(),
                        //Selected = selectedValue == item.Trim() ? true : false
                    });

                }
                return list;
                //return new SelectList(kwList, "Keyword", "Description", selectedValues);
            }
        }

    }

    public class _MailCode
    {
        public string mailcode { get; private set; }
        public string description { get; private set; }
    }

    public class _Category
    {
        public string category { get; private set; }
        public string description { get; private set; }
    }

    public class _Keyword
    {
        public string keyword { get; private set; }
        public string description { get; private set; }
    }
}