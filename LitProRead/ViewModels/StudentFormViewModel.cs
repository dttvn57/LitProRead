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

        public string StudentLastNameID { get; set; }
        public string StudentFirstNameID { get; set; }
        public IEnumerable<SelectListItem> StudentListLastName { get; set; }
        public IEnumerable<SelectListItem> StudentListFirstName { get; set; }

        //public List<SelectListItem> StudentListLastName { get; set; }
        //public List<SelectListItem> StudentListFirstName { get; private set; }

        //public SelectList StudentListLastName { get; private set; }
        //public SelectList StudentListFirstName { get; private set; }

        public StudentFormViewModel()
        {
            Load(-1);
        }

        public void Load(int id)
        {
            this.Id = id;
            this.CurrentStudent = GetStudent(id);
            this.SalutationList = GetSalutationList(CurrentStudent.Salutation);
            this.AreaCodeList = GetAreaCodeList();
            this.CityList = GetCityList();
            this.ContactPrefList = GetContactPrefList();
            this.GenderList = GetGenderList();
            this.EthnicityList = GetEthnicityList();
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

        //public void Populate(Dictionary<string, string> values)
        //{
        //    Id = Convert.ToInt32(values["Id"]);
        //    CurrentStudent = GetStudent(Id);
        //    if (CurrentStudent != null)
        //    {
        //        if (CurrentStudent.Salutation != null && CurrentStudent.Salutation.Equals(values["CurrentStudent.Salutation"]) == false)
        //            CurrentStudent.Salutation = values["CurrentStudent.Salutation"];
        //        if (CurrentStudent.Salutation == null && values["CurrentStudent.Salutation"] != "")
        //            CurrentStudent.Salutation = values["CurrentStudent.Salutation"];
        //        if (CurrentStudent.FirstName != null && CurrentStudent.FirstName.Equals(values["CurrentStudent.FirstName"]) == false)
        //            CurrentStudent.FirstName = values["CurrentStudent.FirstName"];
        //        if (CurrentStudent.LastName != null && CurrentStudent.LastName.Equals(values["CurrentStudent.LastName"]) == false)
        //            CurrentStudent.LastName = values["CurrentStudent.LastName"];
        //        if (CurrentStudent.Address1 != null && CurrentStudent.Address1.Equals(values["CurrentStudent.Address1"]) == false)
        //            CurrentStudent.Address1 = values["CurrentStudent.Address1"];
        //        if (CurrentStudent.Address2 != null && CurrentStudent.Address2.Equals(values["CurrentStudent.Address2"]) == false)
        //            CurrentStudent.Address2 = values["CurrentStudent.Address2"];
        //        if (CurrentStudent.City != null && CurrentStudent.City.Equals(values["CurrentStudent.City"]) == false)
        //            CurrentStudent.City = values["CurrentStudent.City"];
        //        if (CurrentStudent.State != null && CurrentStudent.State.Equals(values["CurrentStudent.State"]) == false)
        //            CurrentStudent.State = values["CurrentStudent.State"];
        //        if (CurrentStudent.Zip != null && CurrentStudent.Zip.Equals(values["CurrentStudent.Zip"]) == false)
        //            CurrentStudent.Zip = values["CurrentStudent.Zip"];
        //        if (CurrentStudent.HomeAreaCode != null && CurrentStudent.HomeAreaCode.Equals(values["CurrentStudent.HomeAreaCode"]) == false)
        //            CurrentStudent.HomeAreaCode = values["CurrentStudent.HomeAreaCode"];
        //        if (CurrentStudent.HomePhone != null && CurrentStudent.HomePhone.Equals(values["CurrentStudent.HomePhone"]) == false)
        //            CurrentStudent.HomePhone = values["CurrentStudent.HomePhone"];
        //        if (CurrentStudent.WorkAreaCode != null && CurrentStudent.WorkAreaCode.Equals(values["CurrentStudent.WorkAreaCode"]) == false)
        //            CurrentStudent.WorkAreaCode = values["CurrentStudent.WorkAreaCode"];
        //        if (CurrentStudent.WorkPhone != null && CurrentStudent.WorkPhone.Equals(values["CurrentStudent.WorkPhone"]) == false)
        //            CurrentStudent.WorkPhone = values["CurrentStudent.WorkPhone"];
        //        if (CurrentStudent.WorkPhoneExt != null && CurrentStudent.WorkPhoneExt.Equals(values["CurrentStudent.WorkPhoneExt"]) == false)
        //            CurrentStudent.WorkPhoneExt = values["CurrentStudent.WorkPhoneExt"];
        //        if (CurrentStudent.FAXAreaCode != null && CurrentStudent.FAXAreaCode.Equals(values["CurrentStudent.FAXAreaCode"]) == false)
        //            CurrentStudent.FAXAreaCode = values["CurrentStudent.FAXAreaCode"];
        //        if (CurrentStudent.FAX != null && CurrentStudent.FAX.Equals(values["CurrentStudent.FAX"]) == false)
        //            CurrentStudent.FAX = values["CurrentStudent.FAX"];
        //        if (CurrentStudent.CellAreaCode1 != null && CurrentStudent.CellAreaCode1.Equals(values["CurrentStudent.CellAreaCode1"]) == false)
        //            CurrentStudent.CellAreaCode1 = values["CurrentStudent.CellAreaCode1"];
        //        if (CurrentStudent.CellPhone1 != null && CurrentStudent.CellPhone1.Equals(values["CurrentStudent.CellPhone1"]) == false)
        //            CurrentStudent.CellPhone1 = values["CurrentStudent.CellPhone1"];
        //        if (CurrentStudent.CellAreaCode2 != null && CurrentStudent.CellAreaCode2.Equals(values["CurrentStudent.CellAreaCode2"]) == false)
        //            CurrentStudent.CellAreaCode2 = values["CurrentStudent.CellAreaCode2"];
        //        if (CurrentStudent.CellAreaPhone2 != null && CurrentStudent.CellAreaPhone2.Equals(values["CurrentStudent.CellAreaPhone2"]) == false)
        //            CurrentStudent.CellAreaPhone2 = values["CurrentStudent.CellAreaPhone2"];
        //        if (CurrentStudent.ContactPref != null && CurrentStudent.ContactPref.Equals(values["CurrentStudent.ContactPref"]) == false)
        //            CurrentStudent.ContactPref = values["CurrentStudent.ContactPref"];
        //        if (CurrentStudent.EMail != null && CurrentStudent.EMail.Equals(values["CurrentStudent.EMail"]) == false)
        //            CurrentStudent.EMail = values["CurrentStudent.EMail"];
        //        if (CurrentStudent.Gender != null && CurrentStudent.Gender.Equals(values["CurrentStudent.Gender"]) == false)
        //            CurrentStudent.Gender = values["CurrentStudent.Gender"];
        //        if (CurrentStudent.Ethnicity != null && CurrentStudent.Ethnicity.Equals(values["CurrentStudent.Ethnicity"]) == false)
        //            CurrentStudent.Ethnicity = values["CurrentStudent.Ethnicity"];
        //        if (CurrentStudent.Income != null && CurrentStudent.Income.Equals(values["CurrentStudent.Income"]) == false)
        //            CurrentStudent.Income = values["CurrentStudent.Income"];
        //        if (CurrentStudent.Occupation != null && CurrentStudent.Occupation.Equals(values["CurrentStudent.Occupation"]) == false)
        //            CurrentStudent.Occupation = values["CurrentStudent.Occupation"];
        //        if (CurrentStudent.Employer != null && CurrentStudent.Employer.Equals(values["CurrentStudent.Employer"]) == false)
        //            CurrentStudent.Employer = values["CurrentStudent.Employer"];
        //        if (CurrentStudent.EmployerStatus != null && CurrentStudent.EmployerStatus.Equals(values["CurrentStudent.EmployerStatus"]) == false)
        //            CurrentStudent.EmployerStatus = values["CurrentStudent.EmployerStatus"];
        //        if (CurrentStudent.EmergContact != null && CurrentStudent.EmergContact.Equals(values["CurrentStudent.EmergContact"]) == false)
        //            CurrentStudent.EmergContact = values["CurrentStudent.EmergContact"];

        //        if (CurrentStudent.EmergAreaCode != null && CurrentStudent.EmergAreaCode.Equals(values["CurrentStudent.EmergAreaCode"]) == false)
        //            CurrentStudent.EmergAreaCode = values["CurrentStudent.EmergAreaCode"];
        //        if (CurrentStudent.EmergPhone != null && CurrentStudent.EmergPhone.Equals(values["CurrentStudent.EmergPhone"]) == false)
        //            CurrentStudent.EmergPhone = values["CurrentStudent.EmergPhone"];
        //        if (CurrentStudent.CallLocation != null && CurrentStudent.CallLocation.Equals(values["CurrentStudent.CallLocation"]) == false)
        //            CurrentStudent.CallLocation = values["CurrentStudent.CallLocation"];
        //        CurrentStudent.DOB = AssignDate(values["CurrentStudent.DOB"]);
        //        //DateTime dob = DateTime.Parse(values["CurrentStudent.DOB"]);
        //        //if (CurrentStudent.DOB != null)
        //        //{
        //        //    if (dob != CurrentStudent.DOB)
        //        //        CurrentStudent.DOB = dob;
        //        //}
        //        //else
        //        //{
        //        //    CurrentStudent.DOB = dob;               
        //        //}
        //        if (CurrentStudent.Country != null && CurrentStudent.Country.Equals(values["CurrentStudent.Country"]) == false)
        //            CurrentStudent.Country = values["CurrentStudent.Country"];
        //        if (CurrentStudent.NativeLanguage != null && CurrentStudent.NativeLanguage.Equals(values["CurrentStudent.NativeLanguage"]) == false)
        //            CurrentStudent.NativeLanguage = values["CurrentStudent.NativeLanguage"];
        //        if (CurrentStudent.EducationLevel != null && CurrentStudent.EducationLevel.Equals(values["CurrentStudent.EducationLevel"]) == false)
        //            CurrentStudent.EducationLevel = values["CurrentStudent.EducationLevel"];
        //        if (CurrentStudent.ReadWriteNativeLang != null && CurrentStudent.ReadWriteNativeLang.Equals(values["CurrentStudent.ReadWriteNativeLang"]) == false)
        //            CurrentStudent.ReadWriteNativeLang = values["CurrentStudent.ReadWriteNativeLang"];
        //        if (CurrentStudent.Referral != null && CurrentStudent.Referral.Equals(values["CurrentStudent.Referral"]) == false)
        //            CurrentStudent.Referral = values["CurrentStudent.Referral"];

        //        CurrentStudent.FingerPrintDate = AssignDate(values["CurrentStudent.FingerPrintDate"]);

        //        if (CurrentStudent.Staff != null && CurrentStudent.Staff.Equals(values["CurrentStudent.Staff"]) == false)
        //            CurrentStudent.Staff = values["CurrentStudent.Staff"];
        //        if (CurrentStudent.Source != null && CurrentStudent.Source.Equals(values["CurrentStudent.Source"]) == false)
        //            CurrentStudent.Source = values["CurrentStudent.Source"];

        //        CurrentStudent.IntakeDate = AssignDate(values["CurrentStudent.IntakeDate"]);
        //        //DateTime intakeDate = DateTime.Parse(values["CurrentStudent.IntakeDate"]);
        //        //if (CurrentStudent.IntakeDate != null)
        //        //{
        //        //    if (intakeDate != CurrentStudent.IntakeDate)
        //        //        CurrentStudent.IntakeDate = intakeDate;
        //        //}
        //        //else
        //        //{
        //        //    CurrentStudent.IntakeDate = intakeDate;
        //        //}

        //        CurrentStudent.FirstActive = AssignDate(values["CurrentStudent.FirstActive"]);
        //        //DateTime firstActive = DateTime.Parse(values["CurrentStudent.FirstActive"]);
        //        //if (CurrentStudent.FirstActive != null)
        //        //{
        //        //    if (firstActive != CurrentStudent.FirstActive)
        //        //        CurrentStudent.FirstActive = firstActive;
        //        //}
        //        //else
        //        //{
        //        //    CurrentStudent.FirstActive = firstActive;
        //        //}

        //        ///SetCurrentActive(values["CurrentStudentActive"]);
        //        if (CurrentStudent.Active != null)
        //        {
        //            if (values["CurrentStudent.Active"] == "false")
        //                CurrentStudent.Active = false;
        //            else
        //                CurrentStudent.Active = true;
        //        }
        //        else
        //        {
                
        //        }

        //        CurrentStudent.ActiveDate = AssignDate(values["CurrentStudent.ActiveDate"]);

        //        if (CurrentStudent.Status != null && CurrentStudent.Status.Equals(values["CurrentStudent.Status"]) == false)
        //            CurrentStudent.Status = values["CurrentStudent.Status"];
        //        if (values["CurrentStudent.InActive"] == "false")
        //            CurrentStudent.InActive = false;
        //        else
        //            CurrentStudent.InActive = true;
        //        if (CurrentStudent.InActiveReason != null && CurrentStudent.InActiveReason.Equals(values["CurrentStudent.InActiveReason"]) == false)
        //            CurrentStudent.InActiveReason = values["CurrentStudent.InActiveReason"];
        //        if (CurrentStudent.Program != null && CurrentStudent.Program.Equals(values["CurrentStudent.Program"]) == false)
        //            CurrentStudent.Program = values["CurrentStudent.Program"];
        //        if (CurrentStudent.MailCode != null && CurrentStudent.MailCode.Equals(values["CurrentStudent.MailCode"]) == false)
        //            CurrentStudent.MailCode = values["CurrentStudent.MailCode"];
        //        if (CurrentStudent.Category != null && CurrentStudent.Category.Equals(values["CurrentStudent.Category"]) == false)
        //            CurrentStudent.Category = values["CurrentStudent.Category"];
        //        if (CurrentStudent.Keyword != null && CurrentStudent.Keyword.Equals(values["CurrentStudent.Keyword"]) == false)
        //            CurrentStudent.Keyword = values["CurrentStudent.Keyword"];

        //        CurrentStudent.AvailMonAM = values["CurrentStudent.AvailMonAM"] == "false" ? false : true;
        //    }
        //}

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

        // "Female";"Male"
        public List<SelectListItem> GetGenderList(string selected = "")
        {
            List<string> genders = new List<string>();
            genders.Add("Female");
            genders.Add("Male");
            //List<SelectListItem> gender = new List<SelectListItem>();
            //gender.Add(new SelectListItem { Text = "Female", Value = "0" });
            //gender.Add(new SelectListItem { Text = "Male", Value = "1" });
            return ParseList(selected, genders);
        }

        // SELECT DISTINCTROW Ethnicity.Ethnicity FROM Ethnicity ORDER BY Ethnicity.Ethnicity
        public List<SelectListItem> GetEthnicityList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var ethnicity = db.Database.SqlQuery<string>("SELECT Ethnicity FROM dbo.Ethnicity ORDER BY Ethnicity").ToList();
                return ParseList(selected, ethnicity);
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