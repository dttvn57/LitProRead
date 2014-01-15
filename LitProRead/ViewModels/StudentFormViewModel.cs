using LitProRead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    [Serializable]
    [Bind(Exclude = "AreaCodeList, CityList, ContactPrefList, EthnicityList, IncomeList, GenderList, EmployerStatusList, CallLocationList, CountryList, NativeLanguageList, ReadWriteNativeLangList, EducationLevelList, ReferralList, StaffList SourceList, StatusList, StudentProgramList, MailCodeList, CategoryList, KeywordList, TransportationList, StudentContactList, LocationPrefList, TutorSmokerList, StudentListLastName, StudentListFirstName")]
    public class StudentFormViewModel
    {
        public LitProReadEntities db = new LitProReadEntities();

        public int Id { get; set; }
        
        public Student CurrentStudent { get; set; }

        public IEnumerable<SelectListItem> SalutationList { get; set; }
        public SelectList AreaCodeList { get; private set; }
        public SelectList CityList { get; private set; }
        public SelectList ContactPrefList { get; private set; }
        public SelectList EthnicityList { get; private set; }
        public SelectList IncomeList { get; private set; }
        public SelectList GenderList { get; private set; }
        public SelectList EmployerStatusList { get; private set; }
        public SelectList CallLocationList { get; private set; }
        public SelectList CountryList { get; private set; }
        public SelectList NativeLanguageList { get; private set; }
        public SelectList ReadWriteNativeLangList { get; private set; }
        public SelectList EducationLevelList { get; private set; }
        public SelectList ReferralList { get; private set; }
        public SelectList StaffList { get; private set; }
        public SelectList SourceList { get; private set; }

        public double StudentAge { get; private set; }
        //public bool CurrentStudentActive { get; set; }
        //public bool CurrentStudentInActive { get; set; }

        //public bool CurrentStudentAvailMonAM { get; set; }
        //public bool CurrentStudentAvailTueAM { get; set; }
        //public bool CurrentStudentAvailWedAM { get; set; }
        //public bool CurrentStudentAvailThuAM { get; set; }
        //public bool CurrentStudentAvailFriAM { get; set; }
        //public bool CurrentStudentAvailSatAM { get; set; }
        //public bool CurrentStudentAvailSunAM { get; set; }

        //public bool CurrentStudentAvailMonPM { get; set; }
        //public bool CurrentStudentAvailTuePM { get; set; }
        //public bool CurrentStudentAvailWedPM { get; set; }
        //public bool CurrentStudentAvailThuPM { get; set; }
        //public bool CurrentStudentAvailFriPM { get; set; }
        //public bool CurrentStudentAvailSatPM { get; set; }
        //public bool CurrentStudentAvailSunPM { get; set; }

        //public bool CurrentStudentAvailMonEVE { get; set; }
        //public bool CurrentStudentAvailTueEVE { get; set; }
        //public bool CurrentStudentAvailWedEVE { get; set; }
        //public bool CurrentStudentAvailThuEVE { get; set; }
        //public bool CurrentStudentAvailFriEVE { get; set; }
        //public bool CurrentStudentAvailSatEVE { get; set; }
        //public bool CurrentStudentAvailSunEVE { get; set; }

        public SelectList StatusList { get; private set; }
        public SelectList StudentProgramList { get; private set; }
        public SelectList MailCodeList { get; private set; }
        public SelectList CategoryList { get; private set; }
        public SelectList KeywordList { get; private set; }

        public SelectList TransportationList { get; private set; }
        public SelectList StudentContactList { get; private set; }
        public SelectList LocationPrefList { get; private set; }
        public SelectList SmokingPrefList { get; private set; }
        public SelectList TutorSmokerList { get; private set; }

        //public List<string> Students { get; set; }
        public SelectList StudentListLastName { get; private set; }
        public SelectList StudentListFirstName { get; private set; }

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
            this.StudentListLastName = GetStudentsLastName(null);
            this.StudentListFirstName = GetStudentsFirstName(null);
            this.StudentAge = GetStudentAge();
            //this.CurrentStudentActive = GetCurrentStudentActive();
            //this.CurrentStudentInActive = GetCurrentStudentInActive();

            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailMonAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailTueAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailWedAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailThuAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailFriAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSatAM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSunAM();

            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailMonPM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailTuePM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailWedPM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailThuPM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailFriPM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSatPM();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSunPM();

            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailMonEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailTueEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailWedEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailThuEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailFriEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSatEVE();
            //this.CurrentStudentAvailFriAM = GetCurrentStudentAvailSunEVE();

            this.TransportationList = GetTransportationList();
            this.StudentContactList = GetStudentContactList();
            this.LocationPrefList = GetLocationPrefList();
            this.SmokingPrefList = GetSmokingPrefList();
            this.TutorSmokerList = GetTutorSmokerList();

            this.StatusList = GetStatusList();
            this.StudentProgramList = GetStudentProgramList();
            this.MailCodeList = GetMailCodeList(null);
            this.CategoryList = GetCategoryList(null);
            this.KeywordList = GetKeywordList(null);
        }

        public void Populate(Dictionary<string, string> values)
        {
            Id = Convert.ToInt32(values["Id"]);
            CurrentStudent = GetStudent(Id);
            if (CurrentStudent != null)
            {
                if (CurrentStudent.Salutation != null && CurrentStudent.Salutation.Equals(values["CurrentStudent.Salutation"]) == false)
                    CurrentStudent.Salutation = values["CurrentStudent.Salutation"];
                if (CurrentStudent.Salutation == null && values["CurrentStudent.Salutation"] != "")
                    CurrentStudent.Salutation = values["CurrentStudent.Salutation"];
                if (CurrentStudent.FirstName != null && CurrentStudent.FirstName.Equals(values["CurrentStudent.FirstName"]) == false)
                    CurrentStudent.FirstName = values["CurrentStudent.FirstName"];
                if (CurrentStudent.LastName != null && CurrentStudent.LastName.Equals(values["CurrentStudent.LastName"]) == false)
                    CurrentStudent.LastName = values["CurrentStudent.LastName"];
                if (CurrentStudent.Address1 != null && CurrentStudent.Address1.Equals(values["CurrentStudent.Address1"]) == false)
                    CurrentStudent.Address1 = values["CurrentStudent.Address1"];
                if (CurrentStudent.Address2 != null && CurrentStudent.Address2.Equals(values["CurrentStudent.Address2"]) == false)
                    CurrentStudent.Address2 = values["CurrentStudent.Address2"];
                if (CurrentStudent.City != null && CurrentStudent.City.Equals(values["CurrentStudent.City"]) == false)
                    CurrentStudent.City = values["CurrentStudent.City"];
                if (CurrentStudent.State != null && CurrentStudent.State.Equals(values["CurrentStudent.State"]) == false)
                    CurrentStudent.State = values["CurrentStudent.State"];
                if (CurrentStudent.Zip != null && CurrentStudent.Zip.Equals(values["CurrentStudent.Zip"]) == false)
                    CurrentStudent.Zip = values["CurrentStudent.Zip"];
                if (CurrentStudent.HomeAreaCode != null && CurrentStudent.HomeAreaCode.Equals(values["CurrentStudent.HomeAreaCode"]) == false)
                    CurrentStudent.HomeAreaCode = values["CurrentStudent.HomeAreaCode"];
                if (CurrentStudent.HomePhone != null && CurrentStudent.HomePhone.Equals(values["CurrentStudent.HomePhone"]) == false)
                    CurrentStudent.HomePhone = values["CurrentStudent.HomePhone"];
                if (CurrentStudent.WorkAreaCode != null && CurrentStudent.WorkAreaCode.Equals(values["CurrentStudent.WorkAreaCode"]) == false)
                    CurrentStudent.WorkAreaCode = values["CurrentStudent.WorkAreaCode"];
                if (CurrentStudent.WorkPhone != null && CurrentStudent.WorkPhone.Equals(values["CurrentStudent.WorkPhone"]) == false)
                    CurrentStudent.WorkPhone = values["CurrentStudent.WorkPhone"];
                if (CurrentStudent.WorkPhoneExt != null && CurrentStudent.WorkPhoneExt.Equals(values["CurrentStudent.WorkPhoneExt"]) == false)
                    CurrentStudent.WorkPhoneExt = values["CurrentStudent.WorkPhoneExt"];
                if (CurrentStudent.FAXAreaCode != null && CurrentStudent.FAXAreaCode.Equals(values["CurrentStudent.FAXAreaCode"]) == false)
                    CurrentStudent.FAXAreaCode = values["CurrentStudent.FAXAreaCode"];
                if (CurrentStudent.FAX != null && CurrentStudent.FAX.Equals(values["CurrentStudent.FAX"]) == false)
                    CurrentStudent.FAX = values["CurrentStudent.FAX"];
                if (CurrentStudent.CellAreaCode1 != null && CurrentStudent.CellAreaCode1.Equals(values["CurrentStudent.CellAreaCode1"]) == false)
                    CurrentStudent.CellAreaCode1 = values["CurrentStudent.CellAreaCode1"];
                if (CurrentStudent.CellPhone1 != null && CurrentStudent.CellPhone1.Equals(values["CurrentStudent.CellPhone1"]) == false)
                    CurrentStudent.CellPhone1 = values["CurrentStudent.CellPhone1"];
                if (CurrentStudent.CellAreaCode2 != null && CurrentStudent.CellAreaCode2.Equals(values["CurrentStudent.CellAreaCode2"]) == false)
                    CurrentStudent.CellAreaCode2 = values["CurrentStudent.CellAreaCode2"];
                if (CurrentStudent.CellAreaPhone2 != null && CurrentStudent.CellAreaPhone2.Equals(values["CurrentStudent.CellAreaPhone2"]) == false)
                    CurrentStudent.CellAreaPhone2 = values["CurrentStudent.CellAreaPhone2"];
                if (CurrentStudent.ContactPref != null && CurrentStudent.ContactPref.Equals(values["CurrentStudent.ContactPref"]) == false)
                    CurrentStudent.ContactPref = values["CurrentStudent.ContactPref"];
                if (CurrentStudent.EMail != null && CurrentStudent.EMail.Equals(values["CurrentStudent.EMail"]) == false)
                    CurrentStudent.EMail = values["CurrentStudent.EMail"];
                if (CurrentStudent.Gender != null && CurrentStudent.Gender.Equals(values["CurrentStudent.Gender"]) == false)
                    CurrentStudent.Gender = values["CurrentStudent.Gender"];
                if (CurrentStudent.Ethnicity != null && CurrentStudent.Ethnicity.Equals(values["CurrentStudent.Ethnicity"]) == false)
                    CurrentStudent.Ethnicity = values["CurrentStudent.Ethnicity"];
                if (CurrentStudent.Income != null && CurrentStudent.Income.Equals(values["CurrentStudent.Income"]) == false)
                    CurrentStudent.Income = values["CurrentStudent.Income"];
                if (CurrentStudent.Occupation != null && CurrentStudent.Occupation.Equals(values["CurrentStudent.Occupation"]) == false)
                    CurrentStudent.Occupation = values["CurrentStudent.Occupation"];
                if (CurrentStudent.Employer != null && CurrentStudent.Employer.Equals(values["CurrentStudent.Employer"]) == false)
                    CurrentStudent.Employer = values["CurrentStudent.Employer"];
                if (CurrentStudent.EmployerStatus != null && CurrentStudent.EmployerStatus.Equals(values["CurrentStudent.EmployerStatus"]) == false)
                    CurrentStudent.EmployerStatus = values["CurrentStudent.EmployerStatus"];
                if (CurrentStudent.EmergContact != null && CurrentStudent.EmergContact.Equals(values["CurrentStudent.EmergContact"]) == false)
                    CurrentStudent.EmergContact = values["CurrentStudent.EmergContact"];

                if (CurrentStudent.EmergAreaCode != null && CurrentStudent.EmergAreaCode.Equals(values["CurrentStudent.EmergAreaCode"]) == false)
                    CurrentStudent.EmergAreaCode = values["CurrentStudent.EmergAreaCode"];
                if (CurrentStudent.EmergPhone != null && CurrentStudent.EmergPhone.Equals(values["CurrentStudent.EmergPhone"]) == false)
                    CurrentStudent.EmergPhone = values["CurrentStudent.EmergPhone"];
                if (CurrentStudent.CallLocation != null && CurrentStudent.CallLocation.Equals(values["CurrentStudent.CallLocation"]) == false)
                    CurrentStudent.CallLocation = values["CurrentStudent.CallLocation"];
                CurrentStudent.DOB = AssignDate(values["CurrentStudent.DOB"]);
                //DateTime dob = DateTime.Parse(values["CurrentStudent.DOB"]);
                //if (CurrentStudent.DOB != null)
                //{
                //    if (dob != CurrentStudent.DOB)
                //        CurrentStudent.DOB = dob;
                //}
                //else
                //{
                //    CurrentStudent.DOB = dob;               
                //}
                if (CurrentStudent.Country != null && CurrentStudent.Country.Equals(values["CurrentStudent.Country"]) == false)
                    CurrentStudent.Country = values["CurrentStudent.Country"];
                if (CurrentStudent.NativeLanguage != null && CurrentStudent.NativeLanguage.Equals(values["CurrentStudent.NativeLanguage"]) == false)
                    CurrentStudent.NativeLanguage = values["CurrentStudent.NativeLanguage"];
                if (CurrentStudent.EducationLevel != null && CurrentStudent.EducationLevel.Equals(values["CurrentStudent.EducationLevel"]) == false)
                    CurrentStudent.EducationLevel = values["CurrentStudent.EducationLevel"];
                if (CurrentStudent.ReadWriteNativeLang != null && CurrentStudent.ReadWriteNativeLang.Equals(values["CurrentStudent.ReadWriteNativeLang"]) == false)
                    CurrentStudent.ReadWriteNativeLang = values["CurrentStudent.ReadWriteNativeLang"];
                if (CurrentStudent.Referral != null && CurrentStudent.Referral.Equals(values["CurrentStudent.Referral"]) == false)
                    CurrentStudent.Referral = values["CurrentStudent.Referral"];

                CurrentStudent.FingerPrintDate = AssignDate(values["CurrentStudent.FingerPrintDate"]);

                if (CurrentStudent.Staff != null && CurrentStudent.Staff.Equals(values["CurrentStudent.Staff"]) == false)
                    CurrentStudent.Staff = values["CurrentStudent.Staff"];
                if (CurrentStudent.Source != null && CurrentStudent.Source.Equals(values["CurrentStudent.Source"]) == false)
                    CurrentStudent.Source = values["CurrentStudent.Source"];

                CurrentStudent.IntakeDate = AssignDate(values["CurrentStudent.IntakeDate"]);
                //DateTime intakeDate = DateTime.Parse(values["CurrentStudent.IntakeDate"]);
                //if (CurrentStudent.IntakeDate != null)
                //{
                //    if (intakeDate != CurrentStudent.IntakeDate)
                //        CurrentStudent.IntakeDate = intakeDate;
                //}
                //else
                //{
                //    CurrentStudent.IntakeDate = intakeDate;
                //}

                CurrentStudent.FirstActive = AssignDate(values["CurrentStudent.FirstActive"]);
                //DateTime firstActive = DateTime.Parse(values["CurrentStudent.FirstActive"]);
                //if (CurrentStudent.FirstActive != null)
                //{
                //    if (firstActive != CurrentStudent.FirstActive)
                //        CurrentStudent.FirstActive = firstActive;
                //}
                //else
                //{
                //    CurrentStudent.FirstActive = firstActive;
                //}

                ///SetCurrentActive(values["CurrentStudentActive"]);
                if (CurrentStudent.Active != null)
                {
                    if (values["CurrentStudent.Active"] == "false")
                        CurrentStudent.Active = false;
                    else
                        CurrentStudent.Active = true;
                }
                else
                {
                
                }

                CurrentStudent.ActiveDate = AssignDate(values["CurrentStudent.ActiveDate"]);

                if (CurrentStudent.Status != null && CurrentStudent.Status.Equals(values["CurrentStudent.Status"]) == false)
                    CurrentStudent.Status = values["CurrentStudent.Status"];
                if (values["CurrentStudent.InActive"] == "false")
                    CurrentStudent.InActive = false;
                else
                    CurrentStudent.InActive = true;
                if (CurrentStudent.InActiveReason != null && CurrentStudent.InActiveReason.Equals(values["CurrentStudent.InActiveReason"]) == false)
                    CurrentStudent.InActiveReason = values["CurrentStudent.InActiveReason"];
                if (CurrentStudent.Program != null && CurrentStudent.Program.Equals(values["CurrentStudent.Program"]) == false)
                    CurrentStudent.Program = values["CurrentStudent.Program"];
                if (CurrentStudent.MailCode != null && CurrentStudent.MailCode.Equals(values["CurrentStudent.MailCode"]) == false)
                    CurrentStudent.MailCode = values["CurrentStudent.MailCode"];
                if (CurrentStudent.Category != null && CurrentStudent.Category.Equals(values["CurrentStudent.Category"]) == false)
                    CurrentStudent.Category = values["CurrentStudent.Category"];
                if (CurrentStudent.Keyword != null && CurrentStudent.Keyword.Equals(values["CurrentStudent.Keyword"]) == false)
                    CurrentStudent.Keyword = values["CurrentStudent.Keyword"];

                CurrentStudent.AvailMonAM = values["CurrentStudent.AvailMonAM"] == "false" ? false : true;
            }
        }

        public Student GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
                return new Student();
            return student;
        }

        // SELECT DISTINCTROW Salutation.Salutation FROM Salutation
        public IEnumerable<SelectListItem> GetSalutationList(string selected)
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

        // SELECT DISTINCTROW City.City FROM Salutation
        public SelectList GetCityList()
        {
            var cities = db.Database.SqlQuery<string>("SELECT City FROM dbo.City").ToList();
            return new SelectList(cities);
        }

        // SELECT DISTINCTROW AreaCodes.AreaCodes FROM Salutation
        public SelectList GetAreaCodeList()
        {
            var codes = db.Database.SqlQuery<string>("SELECT AreaCodes FROM dbo.AreaCodes").ToList();
            return new SelectList(codes);
        }

        // SELECT ContactPref.ContactPref FROM ContactPref
        public SelectList GetContactPrefList()
        {
            var prefs = db.Database.SqlQuery<string>("SELECT ContactPref FROM dbo.ContactPref").ToList();
            return new SelectList(prefs);
        }

        // "Female";"Male"
        public SelectList GetGenderList()
        {
            List<string> genders = new List<string>();
            genders.Add("Female");
            genders.Add("Male");
            //List<SelectListItem> gender = new List<SelectListItem>();
            //gender.Add(new SelectListItem { Text = "Female", Value = "0" });
            //gender.Add(new SelectListItem { Text = "Male", Value = "1" });
            return new SelectList(genders);
        }

        // SELECT DISTINCTROW Ethnicity.Ethnicity FROM Ethnicity ORDER BY Ethnicity.Ethnicity
        public SelectList GetEthnicityList()
        {
            var ethnicity = db.Database.SqlQuery<string>("SELECT Ethnicity FROM dbo.Ethnicity ORDER BY Ethnicity").ToList();
            return new SelectList(ethnicity);
        }

        // SELECT Income.Income FROM Income ORDER BY Income.Income
        public SelectList GetIncomeList()
        {
            var income = db.Database.SqlQuery<string>("SELECT Income FROM dbo.Income ORDER BY Income").ToList();
            return new SelectList(income);
        }

        //SELECT DISTINCTROW EmployerStatus.EmployerStatus FROM EmployerStatus ORDER BY EmployerStatus.EmployerStatus; 
        public SelectList GetEmployerStatusList()
        {
            var employerStatus = db.Database.SqlQuery<string>("SELECT EmployerStatus FROM dbo.EmployerStatus ORDER BY EmployerStatus").ToList();
            return new SelectList(employerStatus);
        }

        // SELECT CallLocation.CallLocation FROM CallLocation ORDER BY CallLocation.CallLocation; 
        public SelectList GetCallLocationList()
        {
            var locs = db.Database.SqlQuery<string>("SELECT CallLocation FROM dbo.CallLocation ORDER BY CallLocation").ToList();
            return new SelectList(locs);
        }

        // SELECT DISTINCTROW Country.Country FROM Country ORDER BY Country.Country; 
        public SelectList GetCountryList()
        {
            var countries = db.Database.SqlQuery<string>("SELECT Country FROM dbo.Country ORDER BY Country").ToList();
            return new SelectList(countries);
        }

        // SELECT DISTINCTROW NativeLanguage.NativeLanguage FROM NativeLanguage; 
        public SelectList GetNativeLanguageList()
        {
            var langs = db.Database.SqlQuery<string>("SELECT NativeLanguage FROM dbo.NativeLanguage").ToList();
            return new SelectList(langs);
        }

        // "Yes";"No"
        public SelectList GetReadWriteNativeLangList()
        {
            List<string> yn = new List<string>();
            yn.Add("Yes");
            yn.Add("No");
            return new SelectList(yn);
        }

        // SELECT DISTINCT EducationLevel.EducationLevel FROM EducationLevel ORDER BY EducationLevel.EducationLevel; 
        public SelectList GetEducationLevelList()
        {
            var levels = db.Database.SqlQuery<string>("SELECT EducationLevel FROM dbo.EducationLevel ORDER BY EducationLevel").ToList();
            return new SelectList(levels);
        }

        // SELECT Referral.Referral FROM Referral; 
        public SelectList GetReferralList()
        {
            var refs = db.Database.SqlQuery<string>("SELECT Referral FROM dbo.Referral").ToList();
            return new SelectList(refs);
        }

        // SELECT Staff.Staff FROM Staff ORDER BY [Staff]
        public SelectList GetStaffList()
        {
            var staff = db.Database.SqlQuery<string>("SELECT Staff FROM dbo.Staff ORDER BY Staff").ToList();
            return new SelectList(staff);
        }

        // SELECT Source.Source FROM Source ORDER BY [Source]; 
        public SelectList GetSourceList()
        {
            var source = db.Database.SqlQuery<string>("SELECT Source FROM dbo.Source ORDER BY Source").ToList();
            return new SelectList(source);
        }

        //SELECT Status.Status FROM Status; 
        public SelectList GetStatusList()
        {
            var status = db.Database.SqlQuery<string>("SELECT Status FROM dbo.Status").ToList();
            return new SelectList(status);
        }

        //SELECT DISTINCTROW StudentProgram.Program FROM StudentProgram ORDER BY StudentProgram.Program; 
        public SelectList GetStudentProgramList()
        {
            var program = db.Database.SqlQuery<string>("SELECT Program FROM dbo.StudentProgram ORDER BY Program").ToList();
            return new SelectList(program);
        }

        // SELECT DISTINCTROW MailCode.MailCode, MailCode.Description FROM MailCode ORDER BY MailCode.MailCode; 
        public SelectList GetMailCodeList(string[] selectedValues)
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
            return new SelectList(codeList, "MailCode", "Description", selectedValues);
        }

        // SELECT DISTINCTROW Category.Category, Category.Description FROM Category ORDER BY Category.Category; 
        public SelectList GetCategoryList(string[] selectedValues)
        {
            var categories = db.Database.SqlQuery<_Category>("SELECT Category, Description  FROM dbo.Category ORDER BY Category").ToList();
            var catList = from cat in categories
                           select new
                           {
                               Category = cat.category,
                               Description = cat.description
                           };
            return new SelectList(catList, "Category", "Description", selectedValues);
        }

        // SELECT DISTINCTROW Keyword.Keyword, Keyword.Description FROM Keyword ORDER BY Keyword.Keyword; 
        public SelectList GetKeywordList(string[] selectedValues)
        {
            var kws = db.Database.SqlQuery<_Keyword>("SELECT Keyword, Description  FROM dbo.Keyword ORDER BY Keyword").ToList();
            var kwList = from kw in kws
                          select new
                          {
                              Keyword = kw.keyword,
                              Description = kw.description
                          };
            return new SelectList(kwList, "Keyword", "Description", selectedValues);
        }

        //SELECT DISTINCTROW Transportation.Transportation FROM Transportation ORDER BY Transportation.Transportation; 
        public SelectList GetTransportationList()
        {
            var l = db.Database.SqlQuery<string>("SELECT Transportation FROM dbo.Transportation ORDER BY Transportation").ToList();
            return new SelectList(l);
        }

        //SELECT DISTINCT StudentContact.StudentContact FROM StudentContact ORDER BY StudentContact.StudentContact; 
        public SelectList GetStudentContactList()
        {
            var c = db.Database.SqlQuery<string>("SELECT StudentContact FROM dbo.StudentContact ORDER BY StudentContact").ToList();
            return new SelectList(c);
        }

        //SELECT DISTINCTROW LocationPref.LocationPref FROM LocationPref ORDER BY LocationPref.LocationPref; 
        public SelectList GetLocationPrefList()
        {
            var p = db.Database.SqlQuery<string>("SELECT LocationPref FROM dbo.LocationPref ORDER BY LocationPref").ToList();
            return new SelectList(p);
        }

        //SmokingPref - "Smoker";"Non-Smoker"
        public SelectList GetSmokingPrefList()
        {
            List<string> yn = new List<string>();
            yn.Add("Smoker");
            yn.Add("Non-Smoker");
            return new SelectList(yn);
        }

        //TutorSmoker - " ";"Yes";"No"
        public SelectList GetTutorSmokerList()
        {
            List<string> yn = new List<string>();
            yn.Add("Yes");
            yn.Add("No");
            return new SelectList(yn);
        }

        public SelectList GetStudentsLastName(string[] selectedValues)
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
           return new SelectList(students, "ID", "LastName", selectedValues);
        }

        public SelectList GetStudentsFirstName(string[] selectedValues)
        {
            var students = from student in db.Students
                           orderby student.FirstName
                           select new
                           {
                               ID = student.ID,
                               FirstName = student.FirstName + " " + student.LastName
                           };

            return new SelectList(students, "ID", "FirstName", selectedValues);
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

        //private bool GetCurrentStudentActive()
        //{
        //    bool active = false;
        //    if (CurrentStudent != null && CurrentStudent.Active != null)
        //    {
        //        active = (bool)CurrentStudent.Active;
        //    }
        //    return active;
        //}

        //private bool GetCurrentStudentInActive()
        //{
        //    bool active = false;
        //    if (CurrentStudent != null && CurrentStudent.InActive != null)
        //    {
        //        active = (bool)CurrentStudent.InActive;
        //    }
        //    return active;
        //}

        //private bool GetCurrentStudentAvailMonAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailMonAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailMonAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailTueAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailTueAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailTueAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailWedAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailWedAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailWedAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailThuAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailThuAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailThuAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailFriAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailFriAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailFriAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSatAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSatAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSatAM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSunAM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSunAM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSunAM;
        //    }
        //    return avail;
        //}

        //private bool GetCurrentStudentAvailMonPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailMonPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailMonPM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailTuePM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailTuePM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailTuePM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailWedPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailWedPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailWedPM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailThuPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailThuPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailThuPM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailFriPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailFriPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailFriPM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSatPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSatPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSatPM;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSunPM()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSunPM != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSunPM;
        //    }
        //    return avail;
        //}
        
        //private bool GetCurrentStudentAvailMonEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailMonEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailMonEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailTueEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailTueEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailTueEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailWedEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailWedEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailWedEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailThuEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailThudEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailThudEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailFriEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailFriEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailFriEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSatEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSatEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSatEVE;
        //    }
        //    return avail;
        //}
        //private bool GetCurrentStudentAvailSunEVE()
        //{
        //    bool avail = false;
        //    if (CurrentStudent != null && CurrentStudent.AvailSunEVE != null)
        //    {
        //        avail = (bool)CurrentStudent.AvailSunEVE;
        //    }
        //    return avail;
        //}

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
            db.Dispose();
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
       //public StudentViewModel studentVM { get; set; }
}