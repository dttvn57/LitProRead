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
    public class TutorFormViewModel
    {
        //public LitProReadEntities db = new LitProReadEntities();

        public int Id { get; set; }
        
        public Tutor CurrentTutor { get; set; }

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

        public double TutorAge { get; private set; }

        public List<SelectListItem> StatusList { get; private set; }
        public List<SelectListItem> TutorProgramList { get; private set; }
        public List<SelectListItem> MailCodeList { get; private set; }
        public List<SelectListItem> CategoryList { get; private set; }
        public List<SelectListItem> KeywordList { get; private set; }

        public List<SelectListItem> TransportationList { get; private set; }
        public List<SelectListItem> TutorContactList { get; private set; }
        public List<SelectListItem> LocationPrefList { get; private set; }
        public List<SelectListItem> SmokingPrefList { get; private set; }
        public List<SelectListItem> StudentSmokerList { get; private set; }

        public string TutorLastNameID { get; set; }
        public string TutorFirstNameID { get; set; }
        public IEnumerable<SelectListItem> TutorListLastName { get; set; }
        public IEnumerable<SelectListItem> TutorListFirstName { get; set; }

        public TutorFormViewModel()
        {
            Load(-1);
        }

        public void Load(int id)
        {
            this.Id = id;
            this.CurrentTutor = GetTutor(id);
            this.SalutationList = GetSalutationList(CurrentTutor.Salutation);
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
            this.EducationLevelList = GetEducationLevelList();
            this.ReferralList = GetReferralList();
            this.StaffList = GetStaffList();
            this.SourceList = GetSourceList();
            this.TutorAge = GetTutorAge();

            this.TransportationList = GetTransportationList();
            this.TutorContactList = GetTutorContactList();
            this.LocationPrefList = GetLocationPrefList();
            this.SmokingPrefList = GetSmokingPrefList();
            this.StudentSmokerList = GetStudentSmokerList();

            this.StatusList = GetStatusList();
            this.TutorProgramList = GetTutorProgramList();
            this.MailCodeList = DbHelper.GetMailCodeList(null);
            this.CategoryList = DbHelper.GetCategoryList(null);
            this.KeywordList = DbHelper.GetKeywordList(null);
        }

        //public void Populate(Dictionary<string, string> values)
        //{
        //    Id = Convert.ToInt32(values["Id"]);
        //    CurrentTutor = GetStudent(Id);
        //    if (CurrentTutor != null)
        //    {
        //        if (CurrentTutor.Salutation != null && CurrentTutor.Salutation.Equals(values["CurrentTutor.Salutation"]) == false)
        //            CurrentTutor.Salutation = values["CurrentTutor.Salutation"];
        //        if (CurrentTutor.Salutation == null && values["CurrentTutor.Salutation"] != "")
        //            CurrentTutor.Salutation = values["CurrentTutor.Salutation"];
        //        if (CurrentTutor.FirstName != null && CurrentTutor.FirstName.Equals(values["CurrentTutor.FirstName"]) == false)
        //            CurrentTutor.FirstName = values["CurrentTutor.FirstName"];
        //        if (CurrentTutor.LastName != null && CurrentTutor.LastName.Equals(values["CurrentTutor.LastName"]) == false)
        //            CurrentTutor.LastName = values["CurrentTutor.LastName"];
        //        if (CurrentTutor.Address1 != null && CurrentTutor.Address1.Equals(values["CurrentTutor.Address1"]) == false)
        //            CurrentTutor.Address1 = values["CurrentTutor.Address1"];
        //        if (CurrentTutor.Address2 != null && CurrentTutor.Address2.Equals(values["CurrentTutor.Address2"]) == false)
        //            CurrentTutor.Address2 = values["CurrentTutor.Address2"];
        //        if (CurrentTutor.City != null && CurrentTutor.City.Equals(values["CurrentTutor.City"]) == false)
        //            CurrentTutor.City = values["CurrentTutor.City"];
        //        if (CurrentTutor.State != null && CurrentTutor.State.Equals(values["CurrentTutor.State"]) == false)
        //            CurrentTutor.State = values["CurrentTutor.State"];
        //        if (CurrentTutor.Zip != null && CurrentTutor.Zip.Equals(values["CurrentTutor.Zip"]) == false)
        //            CurrentTutor.Zip = values["CurrentTutor.Zip"];
        //        if (CurrentTutor.HomeAreaCode != null && CurrentTutor.HomeAreaCode.Equals(values["CurrentTutor.HomeAreaCode"]) == false)
        //            CurrentTutor.HomeAreaCode = values["CurrentTutor.HomeAreaCode"];
        //        if (CurrentTutor.HomePhone != null && CurrentTutor.HomePhone.Equals(values["CurrentTutor.HomePhone"]) == false)
        //            CurrentTutor.HomePhone = values["CurrentTutor.HomePhone"];
        //        if (CurrentTutor.WorkAreaCode != null && CurrentTutor.WorkAreaCode.Equals(values["CurrentTutor.WorkAreaCode"]) == false)
        //            CurrentTutor.WorkAreaCode = values["CurrentTutor.WorkAreaCode"];
        //        if (CurrentTutor.WorkPhone != null && CurrentTutor.WorkPhone.Equals(values["CurrentTutor.WorkPhone"]) == false)
        //            CurrentTutor.WorkPhone = values["CurrentTutor.WorkPhone"];
        //        if (CurrentTutor.WorkPhoneExt != null && CurrentTutor.WorkPhoneExt.Equals(values["CurrentTutor.WorkPhoneExt"]) == false)
        //            CurrentTutor.WorkPhoneExt = values["CurrentTutor.WorkPhoneExt"];
        //        if (CurrentTutor.FAXAreaCode != null && CurrentTutor.FAXAreaCode.Equals(values["CurrentTutor.FAXAreaCode"]) == false)
        //            CurrentTutor.FAXAreaCode = values["CurrentTutor.FAXAreaCode"];
        //        if (CurrentTutor.FAX != null && CurrentTutor.FAX.Equals(values["CurrentTutor.FAX"]) == false)
        //            CurrentTutor.FAX = values["CurrentTutor.FAX"];
        //        if (CurrentTutor.CellAreaCode1 != null && CurrentTutor.CellAreaCode1.Equals(values["CurrentTutor.CellAreaCode1"]) == false)
        //            CurrentTutor.CellAreaCode1 = values["CurrentTutor.CellAreaCode1"];
        //        if (CurrentTutor.CellPhone1 != null && CurrentTutor.CellPhone1.Equals(values["CurrentTutor.CellPhone1"]) == false)
        //            CurrentTutor.CellPhone1 = values["CurrentTutor.CellPhone1"];
        //        if (CurrentTutor.CellAreaCode2 != null && CurrentTutor.CellAreaCode2.Equals(values["CurrentTutor.CellAreaCode2"]) == false)
        //            CurrentTutor.CellAreaCode2 = values["CurrentTutor.CellAreaCode2"];
        //        if (CurrentTutor.CellAreaPhone2 != null && CurrentTutor.CellAreaPhone2.Equals(values["CurrentTutor.CellAreaPhone2"]) == false)
        //            CurrentTutor.CellAreaPhone2 = values["CurrentTutor.CellAreaPhone2"];
        //        if (CurrentTutor.ContactPref != null && CurrentTutor.ContactPref.Equals(values["CurrentTutor.ContactPref"]) == false)
        //            CurrentTutor.ContactPref = values["CurrentTutor.ContactPref"];
        //        if (CurrentTutor.EMail != null && CurrentTutor.EMail.Equals(values["CurrentTutor.EMail"]) == false)
        //            CurrentTutor.EMail = values["CurrentTutor.EMail"];
        //        if (CurrentTutor.Gender != null && CurrentTutor.Gender.Equals(values["CurrentTutor.Gender"]) == false)
        //            CurrentTutor.Gender = values["CurrentTutor.Gender"];
        //        if (CurrentTutor.Ethnicity != null && CurrentTutor.Ethnicity.Equals(values["CurrentTutor.Ethnicity"]) == false)
        //            CurrentTutor.Ethnicity = values["CurrentTutor.Ethnicity"];
        //        if (CurrentTutor.Income != null && CurrentTutor.Income.Equals(values["CurrentTutor.Income"]) == false)
        //            CurrentTutor.Income = values["CurrentTutor.Income"];
        //        if (CurrentTutor.Occupation != null && CurrentTutor.Occupation.Equals(values["CurrentTutor.Occupation"]) == false)
        //            CurrentTutor.Occupation = values["CurrentTutor.Occupation"];
        //        if (CurrentTutor.Employer != null && CurrentTutor.Employer.Equals(values["CurrentTutor.Employer"]) == false)
        //            CurrentTutor.Employer = values["CurrentTutor.Employer"];
        //        if (CurrentTutor.EmployerStatus != null && CurrentTutor.EmployerStatus.Equals(values["CurrentTutor.EmployerStatus"]) == false)
        //            CurrentTutor.EmployerStatus = values["CurrentTutor.EmployerStatus"];
        //        if (CurrentTutor.EmergContact != null && CurrentTutor.EmergContact.Equals(values["CurrentTutor.EmergContact"]) == false)
        //            CurrentTutor.EmergContact = values["CurrentTutor.EmergContact"];

        //        if (CurrentTutor.EmergAreaCode != null && CurrentTutor.EmergAreaCode.Equals(values["CurrentTutor.EmergAreaCode"]) == false)
        //            CurrentTutor.EmergAreaCode = values["CurrentTutor.EmergAreaCode"];
        //        if (CurrentTutor.EmergPhone != null && CurrentTutor.EmergPhone.Equals(values["CurrentTutor.EmergPhone"]) == false)
        //            CurrentTutor.EmergPhone = values["CurrentTutor.EmergPhone"];
        //        if (CurrentTutor.CallLocation != null && CurrentTutor.CallLocation.Equals(values["CurrentTutor.CallLocation"]) == false)
        //            CurrentTutor.CallLocation = values["CurrentTutor.CallLocation"];
        //        CurrentTutor.DOB = AssignDate(values["CurrentTutor.DOB"]);
        //        //DateTime dob = DateTime.Parse(values["CurrentTutor.DOB"]);
        //        //if (CurrentTutor.DOB != null)
        //        //{
        //        //    if (dob != CurrentTutor.DOB)
        //        //        CurrentTutor.DOB = dob;
        //        //}
        //        //else
        //        //{
        //        //    CurrentTutor.DOB = dob;               
        //        //}
        //        if (CurrentTutor.Country != null && CurrentTutor.Country.Equals(values["CurrentTutor.Country"]) == false)
        //            CurrentTutor.Country = values["CurrentTutor.Country"];
        //        if (CurrentTutor.NativeLanguage != null && CurrentTutor.NativeLanguage.Equals(values["CurrentTutor.NativeLanguage"]) == false)
        //            CurrentTutor.NativeLanguage = values["CurrentTutor.NativeLanguage"];
        //        if (CurrentTutor.EducationLevel != null && CurrentTutor.EducationLevel.Equals(values["CurrentTutor.EducationLevel"]) == false)
        //            CurrentTutor.EducationLevel = values["CurrentTutor.EducationLevel"];
        //        if (CurrentTutor.ReadWriteNativeLang != null && CurrentTutor.ReadWriteNativeLang.Equals(values["CurrentTutor.ReadWriteNativeLang"]) == false)
        //            CurrentTutor.ReadWriteNativeLang = values["CurrentTutor.ReadWriteNativeLang"];
        //        if (CurrentTutor.Referral != null && CurrentTutor.Referral.Equals(values["CurrentTutor.Referral"]) == false)
        //            CurrentTutor.Referral = values["CurrentTutor.Referral"];

        //        CurrentTutor.FingerPrintDate = AssignDate(values["CurrentTutor.FingerPrintDate"]);

        //        if (CurrentTutor.Staff != null && CurrentTutor.Staff.Equals(values["CurrentTutor.Staff"]) == false)
        //            CurrentTutor.Staff = values["CurrentTutor.Staff"];
        //        if (CurrentTutor.Source != null && CurrentTutor.Source.Equals(values["CurrentTutor.Source"]) == false)
        //            CurrentTutor.Source = values["CurrentTutor.Source"];

        //        CurrentTutor.IntakeDate = AssignDate(values["CurrentTutor.IntakeDate"]);
        //        //DateTime intakeDate = DateTime.Parse(values["CurrentTutor.IntakeDate"]);
        //        //if (CurrentTutor.IntakeDate != null)
        //        //{
        //        //    if (intakeDate != CurrentTutor.IntakeDate)
        //        //        CurrentTutor.IntakeDate = intakeDate;
        //        //}
        //        //else
        //        //{
        //        //    CurrentTutor.IntakeDate = intakeDate;
        //        //}

        //        CurrentTutor.FirstActive = AssignDate(values["CurrentTutor.FirstActive"]);
        //        //DateTime firstActive = DateTime.Parse(values["CurrentTutor.FirstActive"]);
        //        //if (CurrentTutor.FirstActive != null)
        //        //{
        //        //    if (firstActive != CurrentTutor.FirstActive)
        //        //        CurrentTutor.FirstActive = firstActive;
        //        //}
        //        //else
        //        //{
        //        //    CurrentTutor.FirstActive = firstActive;
        //        //}

        //        ///SetCurrentActive(values["CurrentTutorActive"]);
        //        if (CurrentTutor.Active != null)
        //        {
        //            if (values["CurrentTutor.Active"] == "false")
        //                CurrentTutor.Active = false;
        //            else
        //                CurrentTutor.Active = true;
        //        }
        //        else
        //        {
                
        //        }

        //        CurrentTutor.ActiveDate = AssignDate(values["CurrentTutor.ActiveDate"]);

        //        if (CurrentTutor.Status != null && CurrentTutor.Status.Equals(values["CurrentTutor.Status"]) == false)
        //            CurrentTutor.Status = values["CurrentTutor.Status"];
        //        if (values["CurrentTutor.InActive"] == "false")
        //            CurrentTutor.InActive = false;
        //        else
        //            CurrentTutor.InActive = true;
        //        if (CurrentTutor.InActiveReason != null && CurrentTutor.InActiveReason.Equals(values["CurrentTutor.InActiveReason"]) == false)
        //            CurrentTutor.InActiveReason = values["CurrentTutor.InActiveReason"];
        //        if (CurrentTutor.Program != null && CurrentTutor.Program.Equals(values["CurrentTutor.Program"]) == false)
        //            CurrentTutor.Program = values["CurrentTutor.Program"];
        //        if (CurrentTutor.MailCode != null && CurrentTutor.MailCode.Equals(values["CurrentTutor.MailCode"]) == false)
        //            CurrentTutor.MailCode = values["CurrentTutor.MailCode"];
        //        if (CurrentTutor.Category != null && CurrentTutor.Category.Equals(values["CurrentTutor.Category"]) == false)
        //            CurrentTutor.Category = values["CurrentTutor.Category"];
        //        if (CurrentTutor.Keyword != null && CurrentTutor.Keyword.Equals(values["CurrentTutor.Keyword"]) == false)
        //            CurrentTutor.Keyword = values["CurrentTutor.Keyword"];

        //        CurrentTutor.AvailMonAM = values["CurrentTutor.AvailMonAM"] == "false" ? false : true;
        //    }
        //}

        public Tutor GetTutor(int id)
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                Tutor tutor = db.Tutors.Find(id);
                if (tutor == null)
                    return new Tutor();
                return tutor;
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

        //SELECT DISTINCTROW TutorProgram.Program FROM TutorProgram ORDER BY TutorProgram.Program; 
        public List<SelectListItem> GetTutorProgramList(string selected = "")
        {
            using (LitProReadEntities db = new LitProReadEntities())
            {
                var program = db.Database.SqlQuery<string>("SELECT Program FROM dbo.TutorProgram ORDER BY Program").ToList();
                return ParseList(selected, program);
            }
        }

        // SELECT DISTINCTROW MailCode.MailCode, MailCode.Description FROM MailCode ORDER BY MailCode.MailCode; 
        public List<SelectListItem> GetMailCodeList(string[] selectedValues)
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

        //// SELECT DISTINCTROW Category.Category, Category.Description FROM Category ORDER BY Category.Category; 
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

        //// SELECT DISTINCTROW Keyword.Keyword, Keyword.Description FROM Keyword ORDER BY Keyword.Keyword; 
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

        //SELECT DISTINCT TutorContact.TutorContact FROM TutorContact ORDER BY TutorContact.TutorContact; 
        public List<SelectListItem> GetTutorContactList(string selected = "")
        {
                using (LitProReadEntities db = new LitProReadEntities())
                {
                    var c = db.Database.SqlQuery<string>("SELECT TutorContact FROM dbo.TutorContact ORDER BY TutorContact").ToList();
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
        public List<SelectListItem> GetStudentSmokerList(string selected = "")
        {
            List<string> yn = new List<string>();
            yn.Add("Yes");
            yn.Add("No");
            return ParseList(selected, yn);
        }


        private double GetTutorAge()
        {
            double age = 0;
            if (CurrentTutor != null && CurrentTutor.DOB != null)
            {
                TimeSpan ageSpan = (TimeSpan)(DateTime.Today - CurrentTutor.DOB);
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
            if (CurrentTutor != null && CurrentTutor.Active != null)
            {
                if (active.Equals("true"))
                    CurrentTutor.Active = true;
                else
                    CurrentTutor.Active = false;
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
}