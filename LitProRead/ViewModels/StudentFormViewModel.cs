using LitProRead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    [Serializable]
    //[Bind(Exclude = "StudentListLastName, StudentListFirstName")]
    //[Bind(Exclude = "LitProReadEntities")]
    public class StudentFormViewModel
    {
        //public LitProReadEntities db = new LitProReadEntities();

        public int Id { get; set; }
        
        public Student CurrentStudent { get; set; }
        public Pair MatchSPair { get; set; }

        public List<StudentChildrenViewModel> StudentChildrenList { get; set; }
        //private ICollection<StudentChildren> _StudentChildrenList;
        //public virtual ICollection<StudentChildren> StudentChildrenList
        //{
        //    get { return _StudentChildrenList ?? (_StudentChildrenList = new HashSet<StudentChildren>()); }
        //    set { _StudentChildrenList = value; }
        //}

         public List<StudentCommentsViewModel> StudentCommentsList { get; set; }

        public List<SelectListItem> SalutationList { get; set; }
        public List<SelectListItem> AreaCodeList { get; private set; }
        public List<SelectListItem> CityList { get; private set; }
        public List<SelectListItem> ContactPrefList { get; private set; }
        public List<SelectListItem> EthnicityList { get; private set; }
        public List<SelectListItem> IncomeList { get; private set; }
        public List<SelectListItem> GenderList { get; private set; }
        public List<SelectListItem> EmployerStatusList { get; private set; }
        public List<SelectListItem> CallLocationList { get; private set; }
        public List<SelectListItem> CountryList { get; private set; }
        public List<SelectListItem> NativeLanguageList { get; private set; }
        public List<SelectListItem> ReadWriteNativeLangList { get; private set; }
        public List<SelectListItem> EducationLevelList { get; private set; }
        public List<SelectListItem> ReferralList { get; private set; }
        public List<SelectListItem> StaffList { get; private set; }
        public List<SelectListItem> SourceList { get; private set; }

        public double StudentAge { get; private set; }

        public List<SelectListItem> StatusList { get; private set; }
        public List<SelectListItem> StudentProgramList { get; private set; }
        public List<SelectListItem> MailCodeList { get; private set; }
        public List<SelectListItem> CategoryList { get; private set; }
        public List<SelectListItem> KeywordList { get; private set; }

        public List<SelectListItem> TransportationList { get; private set; }
        public List<SelectListItem> StudentContactList { get; private set; }
        public List<SelectListItem> LocationPrefList { get; private set; }
        public List<SelectListItem> SmokingPrefList { get; private set; }
        public List<SelectListItem> TutorSmokerList { get; private set; }

        public List<SelectListItem> TutorList { get; private set; }
        public string TutorNameID { get; set; }

        public string StudentLastNameID { get; set; }
        public string StudentFirstNameID { get; set; }
        public IEnumerable<SelectListItem> StudentListLastName { get; set; }
        public IEnumerable<SelectListItem> StudentListFirstName { get; set; }

        //public List<SelectListItem> StudentListLastName { get; set; }
        //public List<SelectListItem> StudentListFirstName { get; private set; }

        //public SelectList StudentListLastName { get; private set; }
        //public SelectList StudentListFirstName { get; private set; }

        public string EditMode { get; set; }

        public StudentFormViewModel()
        {
            Load(-1);
            EditMode = "add";
        }

        public void Load(int id)
        {
            this.Id = id;
            this.CurrentStudent = GetStudent(id);

            this.StudentChildrenList = GetStudentChildren(id);
            this.StudentCommentsList = GetStudentComments(id);

            this.SalutationList = GetSalutationList(CurrentStudent.Salutation);
            this.AreaCodeList = GetAreaCodeList();
            this.CityList = GetCityList();
            this.ContactPrefList = GetContactPrefList();
            this.GenderList = DbHelper.GetGenderList();
            this.EthnicityList = DbHelper.GetEthnicityList();
            this.IncomeList = GetIncomeList();
            this.EmployerStatusList = GetEmployerStatusList();
            this.CallLocationList = GetCallLocationList();
            this.CountryList = GetCountryList();
            this.NativeLanguageList = GetNativeLanguageList();
            this.ReadWriteNativeLangList = GetReadWriteNativeLangList();
            this.EducationLevelList = GetEducationLevelList();
            this.ReferralList = GetReferralList();
            this.StaffList = GetStaffList();
            this.SourceList = GetSourceList();
            //this.StudentListLastName = GetStudentsLastName(id);
            //this.StudentListFirstName = GetStudentsFirstName(id);
            this.StudentAge = GetStudentAge();

            this.TransportationList = GetTransportationList();
            this.StudentContactList = GetStudentContactList();
            this.LocationPrefList = GetLocationPrefList();
            this.SmokingPrefList = GetSmokingPrefList();
            this.TutorSmokerList = GetTutorSmokerList();

            this.StatusList = GetStatusList();
            this.StudentProgramList = GetStudentProgramList();
            this.MailCodeList = DbHelper.GetMailCodeList(null);
            this.CategoryList = DbHelper.GetCategoryList(null);
            this.KeywordList = DbHelper.GetKeywordList(null);
        }

        public Student GetStudent(int id)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Student student = db.Students.Find(id);
                if (student == null)
                    return new Student();
                return student;
            }
        }

        public List<StudentChildrenViewModel> GetStudentChildren(int id)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                return db.GetStudentChildren(id);
            }       
        }

        public List<StudentCommentsViewModel> GetStudentComments(int id)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var query = db.GetStudentComments(id);
                int counter = 0;
                List<StudentCommentsViewModel> list = new List<StudentCommentsViewModel>();
                foreach (var item in query)
                {
                    list.Add(new StudentCommentsViewModel
                        {
                            Index = counter++,
                            New = false,        // not a new Comment record
                            ID = item.ID,
                            CommentDate = item.CommentDate,
                            Comment = item.Comment
                        });
                }
                return list;
                //return db.GetStudentComments(id);
            }
        }

        // SELECT DISTINCTROW Salutation.Salutation FROM Salutation
        public List<SelectListItem> GetSalutationList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var salutations = db.Database.SqlQuery<string>("SELECT Salutation FROM dbo.Salutation").ToList();
                string selectedValue = selected != null ? selected.Trim() : "";
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var sal in salutations)
                {
                    list.Add(new SelectListItem
                    {
                        Text = sal.Trim(),
                        Value = sal.Trim(),
                        //Selected = selectedValue == sal.Trim() ? true : false
                    });

                }
                return list;// new SelectList(list.ToList(), "Value", "Text");
            }
        }

        // SELECT DISTINCTROW City.City FROM City
        public List<SelectListItem> GetCityList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var cities = db.Database.SqlQuery<string>("SELECT City FROM dbo.City").ToList();
                return ParseList(selected, cities);// new SelectList(cities);
            }
        }

        // SELECT DISTINCTROW AreaCodes.AreaCodes FROM Salutation
        public List<SelectListItem> GetAreaCodeList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var codes = db.Database.SqlQuery<string>("SELECT AreaCodes FROM dbo.AreaCodes").ToList();
                return ParseList(selected, codes);
            }
        }

        // SELECT ContactPref.ContactPref FROM ContactPref
        public List<SelectListItem> GetContactPrefList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var prefs = db.Database.SqlQuery<string>("SELECT ContactPref FROM dbo.ContactPref").ToList();
                return ParseList(selected, prefs);
            }
        }

        // SELECT Income.Income FROM Income ORDER BY Income.Income
        public List<SelectListItem> GetIncomeList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var income = db.Database.SqlQuery<string>("SELECT Income FROM dbo.Income ORDER BY Income").ToList();
                return ParseList(selected, income);
            }
        }

        //SELECT DISTINCTROW EmployerStatus.EmployerStatus FROM EmployerStatus ORDER BY EmployerStatus.EmployerStatus; 
        public List<SelectListItem> GetEmployerStatusList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var employerStatus = db.Database.SqlQuery<string>("SELECT EmployerStatus FROM dbo.EmployerStatus ORDER BY EmployerStatus").ToList();
                return ParseList(selected, employerStatus);
            }
        }

        // SELECT CallLocation.CallLocation FROM CallLocation ORDER BY CallLocation.CallLocation; 
        public List<SelectListItem> GetCallLocationList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var locs = db.Database.SqlQuery<string>("SELECT CallLocation FROM dbo.CallLocation ORDER BY CallLocation").ToList();
                return ParseList(selected, locs);
            }
        }

        // SELECT DISTINCTROW Country.Country FROM Country ORDER BY Country.Country; 
        public List<SelectListItem> GetCountryList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var countries = db.Database.SqlQuery<string>("SELECT Country FROM dbo.Country ORDER BY Country").ToList();
                return ParseList(selected, countries);
            }
        }

        // SELECT DISTINCTROW NativeLanguage.NativeLanguage FROM NativeLanguage; 
        public List<SelectListItem> GetNativeLanguageList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var langs = db.Database.SqlQuery<string>("SELECT NativeLanguage FROM dbo.NativeLanguage").ToList();
                return ParseList(selected, langs);
            }
        }

        // "Yes";"No"
        public List<SelectListItem> GetReadWriteNativeLangList(string selected = "")
        {
            List<string> yn = new List<string>();
            yn.Add("Yes");
            yn.Add("No");
            return ParseList(selected, yn);
        }

        // SELECT DISTINCT EducationLevel.EducationLevel FROM EducationLevel ORDER BY EducationLevel.EducationLevel; 
        public List<SelectListItem> GetEducationLevelList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var levels = db.Database.SqlQuery<string>("SELECT EducationLevel FROM dbo.EducationLevel ORDER BY EducationLevel").ToList();
                return ParseList(selected, levels);
            }
        }

        // SELECT Referral.Referral FROM Referral; 
        public List<SelectListItem> GetReferralList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var refs = db.Database.SqlQuery<string>("SELECT Referral FROM dbo.Referral").ToList();
                return ParseList(selected, refs);
            }
        }

        // SELECT Staff.Staff FROM Staff ORDER BY [Staff]
        public List<SelectListItem> GetStaffList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var staff = db.Database.SqlQuery<string>("SELECT Staff FROM dbo.Staff ORDER BY Staff").ToList();
                return ParseList(selected, staff);
            }
        }

        // SELECT Source.Source FROM Source ORDER BY [Source]; 
        public List<SelectListItem> GetSourceList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var source = db.Database.SqlQuery<string>("SELECT Source FROM dbo.Source ORDER BY Source").ToList();
                return ParseList(selected, source);
            }
        }

        //SELECT Status.Status FROM Status; 
        public List<SelectListItem> GetStatusList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var status = db.Database.SqlQuery<string>("SELECT Status FROM dbo.Status").ToList();
                return ParseList(selected, status);
            }
        }

        //SELECT DISTINCTROW StudentProgram.Program FROM StudentProgram ORDER BY StudentProgram.Program; 
        public List<SelectListItem> GetStudentProgramList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var program = db.Database.SqlQuery<string>("SELECT Program FROM dbo.StudentProgram ORDER BY Program").ToList();
                return ParseList(selected, program);
            }
        }

        //// SELECT DISTINCTROW MailCode.MailCode, MailCode.Description FROM MailCode ORDER BY MailCode.MailCode; 
        //public List<SelectListItem> GetMailCodeList(string[] selectedValues)
        //{
        //    using (LitProReadEntities db = new LitProReadEntities())
        //    {
        //        var codes = db.Database.SqlQuery<_MailCode>("SELECT MailCode, Description FROM dbo.MailCode ORDER BY MailCode");
        //        //List<SelectListItem> codeList = new List<SelectListItem>();
        //        //foreach (var code in codes)
        //        //{
        //        //    codeList.Add(new SelectListItem { Value = code.mailcode, Text = code.description });
        //        //}

        //        var codeList = from code in codes
        //                       select new
        //                       {
        //                           MailCode = code.mailcode,
        //                           Description = code.description
        //                       };
        //        List<SelectListItem> list = new List<SelectListItem>();
        //        foreach (var item in codeList)
        //        {
        //            list.Add(new SelectListItem
        //            {
        //                Value = item.MailCode.Trim(),
        //                Text = item.Description.Trim(),
        //                //Selected = selectedValue == item.Trim() ? true : false
        //            });

        //        }
        //        return list;
        //        //return new SelectList(codeList, "MailCode", "Description", selectedValues);
        //    }
        //}

        // SELECT DISTINCTROW Category.Category, Category.Description FROM Category ORDER BY Category.Category; 
        //public List<SelectListItem> GetCategoryList(string selectedValue)
        //{
        //    using (LitProReadEntities db = new LitProReadEntities())
        //    {
        //        var categories = db.Database.SqlQuery<_Category>("SELECT Category, Description  FROM dbo.Category ORDER BY Category").ToList();
        //        var catList = from cat in categories
        //                      select new
        //                      {
        //                          Category = cat.category,
        //                          Description = cat.description
        //                      };
        //        List<SelectListItem> list = new List<SelectListItem>();
        //        foreach (var item in catList)
        //        {
        //            list.Add(new SelectListItem
        //            {
        //                Value = item.Category.Trim(),
        //                Text = item.Description.Trim(),
        //                //Selected = selectedValue == item.Trim() ? true : false
        //            });

        //        }
        //        return list;
        //        //return new SelectList(catList, "Category", "Description", selectedValues);
        //    }
        //}

        // SELECT DISTINCTROW Keyword.Keyword, Keyword.Description FROM Keyword ORDER BY Keyword.Keyword; 
        //public List<SelectListItem> GetKeywordList(string selectedValue)
        //{
        //    using (LitProReadEntities db = new LitProReadEntities())
        //    {
        //        var kws = db.Database.SqlQuery<_Keyword>("SELECT Keyword, Description  FROM dbo.Keyword ORDER BY Keyword").ToList();
        //        var kwList = from kw in kws
        //                     select new
        //                     {
        //                         Keyword = kw.keyword,
        //                         Description = kw.description
        //                     };
        //        List<SelectListItem> list = new List<SelectListItem>();
        //        foreach (var item in kwList)
        //        {
        //            list.Add(new SelectListItem
        //            {
        //                Value = item.Keyword.Trim(),
        //                Text = item.Description.Trim(),
        //                //Selected = selectedValue == item.Trim() ? true : false
        //            });

        //        }
        //        return list;
        //        //return new SelectList(kwList, "Keyword", "Description", selectedValues);
        //    }
        //}

        //SELECT DISTINCTROW Transportation.Transportation FROM Transportation ORDER BY Transportation.Transportation; 
        public List<SelectListItem> GetTransportationList(string selected = "")
        {
                using (LitProReadEntities db = new LitProReadEntities())
                {
            var l = db.Database.SqlQuery<string>("SELECT Transportation FROM dbo.Transportation ORDER BY Transportation").ToList();
            return ParseList(selected, l);
                }
        }

        //SELECT DISTINCT StudentContact.StudentContact FROM StudentContact ORDER BY StudentContact.StudentContact; 
        public List<SelectListItem> GetStudentContactList(string selected = "")
        {
                using (LitProReadEntities db = new LitProReadEntities())
                {
            var c = db.Database.SqlQuery<string>("SELECT StudentContact FROM dbo.StudentContact ORDER BY StudentContact").ToList();
            return ParseList(selected, c);
                }
        }

        //SELECT DISTINCTROW LocationPref.LocationPref FROM LocationPref ORDER BY LocationPref.LocationPref; 
        public List<SelectListItem> GetLocationPrefList(string selected = "")
        {
                using (LitProReadEntities db = new LitProReadEntities())
                {
            var p = db.Database.SqlQuery<string>("SELECT LocationPref FROM dbo.LocationPref ORDER BY LocationPref").ToList();
            return ParseList(selected, p);
                }
        }

        //SmokingPref - "Smoker";"Non-Smoker"
        public List<SelectListItem> GetSmokingPrefList(string selected = "")
        {
            List<string> yn = new List<string>();
            yn.Add("Smoker");
            yn.Add("Non-Smoker");
            return ParseList(selected, yn);
        }

        //TutorSmoker - " ";"Yes";"No"
        public List<SelectListItem> GetTutorSmokerList(string selected = "")
        {
            List<string> yn = new List<string>();
            yn.Add("Yes");
            yn.Add("No");
            return ParseList(selected, yn);
        }

        //SELECT DISTINCTROW qryTutors.LastName, qryTutors.FirstName, qryTutors.ID FROM qryTutors ORDER BY qryTutors.LastName, qryTutors.FirstName; 
        public List<SelectListItem> GetTutorList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var p = db.Database.SqlQuery<string>("SELECT qryTutors.LastName, qryTutors.FirstName, qryTutors.ID FROM qryTutors ORDER BY qryTutors.LastName, qryTutors.FirstName").ToList();
                return ParseList(selected, p);
            }
        }

        public List<SelectListItem> GetStudentsLastName(int Id = -1)
        //public SelectList GetStudentsLastName(int Id = -1)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                //var students = (from student in db.Students
                //               orderby student.LastName
                //               select student)
                //               .AsEnumerable()
                //               .Select( x => new
                //               {
                //                    ID = x.ID,
                //                    LastName = string.Format("{0, -5} {1, -5}", x.LastName, x.FirstName) //student.LastName + ", " + student.FirstName
                //               });
                var students = from student in db.Students
                               orderby student.LastName
                               select new
                               {
                                   ID = student.ID,
                                   LastName = student.LastName + ", " + student.FirstName
                               };
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var student in students)
                {
                    list.Add(new SelectListItem
                    {
                        Value = student.ID.ToString().Trim(),
                        Text = student.LastName.Trim(),
                        Selected = student.ID == Id ? true : false
                    });

                }
                return list;
                //return new SelectList(students, "ID", "LastName");//, selectedValues);
//                return new SelectList(students, "ID", "LastName");//, selectedValues);
            }
        }
        
        // no op
        public void SetStudentsLastName()
        {
            int i = 0;
            i++;
        }

        public List<SelectListItem> GetStudentsFirstName(int Id = -1)
        //public SelectList GetStudentsFirstName(int Id = -1)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var students = from student in db.Students
                               orderby student.FirstName
                               select new
                               {
                                   ID = student.ID,
                                   FirstName = student.FirstName + " " + student.LastName
                               };

                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var student in students)
                {
                    list.Add(new SelectListItem
                    {
                        Value = student.ID.ToString().Trim(),
                        Text = student.FirstName.Trim(),
                        Selected = student.ID == Id ? true : false
                    });

                }
                return list;
                //return new SelectList(students, "ID", "FirstName");
            }
        }

        private double GetStudentAge()
        {
            double age = 0;
            if (CurrentStudent != null && CurrentStudent.DOB != null)
            {
                TimeSpan ageSpan = (TimeSpan)(DateTime.Today - CurrentStudent.DOB);
                double ageInDays = ageSpan.TotalDays;       //total number of days ... also precise
                double daysInYear = 365.2425;               //statistical value for 400 years
                age = ageInDays / daysInYear;               //can be shifted ... not so precise
                return age;            
            }
            return age;
        }

        private DateTime? AssignDate(string dateStr)
        {
            try
            {
                DateTime date = DateTime.Parse(dateStr);
                return date;
                //if (toDate != null)
                //{
                //    if (date != toDate)
                //        return date;
                //}
                //else
                //{
                //    return date;
                //}
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetCurrentActive(string active)
        {
            if (CurrentStudent != null && CurrentStudent.Active != null)
            {
                if (active.Equals("true"))
                    CurrentStudent.Active = true;
                else
                    CurrentStudent.Active = false;
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

        //private readonly SqlConnection sqlConnection;
        //public SqlConnection connection
        //{
        //    get
        //    {
        //        sqlConnection.Open();
        //        return sqlConnection;
        //    }
        //}

        public void Dispose()
        {
            //sqlConnection.Close();
            //sqlConnection.Dispose();
            //db.Dispose();
            int i = 0;
            i++;
        }
    }

       //public StudentViewModel studentVM { get; set; }
}