//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LitProRead.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    //public partial class Student
    public class Student
    {
        public Student()
        {
        }
        public int ID { get; set; }
        public Nullable<int> LitProID { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomeAreaCode { get; set; }
        public string HomePhone { get; set; }
        public string WorkAreaCode { get; set; }
        public string WorkPhone { get; set; }
        public string WorkPhoneExt { get; set; }
        public string FAXAreaCode { get; set; }
        public string FAX { get; set; }
        public string CellAreaCode1 { get; set; }
        public string CellPhone1 { get; set; }
        public string CellAreaCode2 { get; set; }
        public string CellAreaPhone2 { get; set; }
        public string ContactPref { get; set; }
        public string EMail { get; set; }
        public Nullable<System.DateTime> FingerPrintDate { get; set; }
        public string Source { get; set; }
        public string Staff { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> ActiveDate { get; set; }
        public string ActiveType { get; set; }
        public bool InActive { get; set; }
        public Nullable<System.DateTime> InActiveDate { get; set; }
        public string InActiveReason { get; set; }
        public Nullable<System.DateTime> FirstActive { get; set; }
        public string MailCode { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Occupation { get; set; }
        public string EmergContact { get; set; }
        public string EmergAreaCode { get; set; }
        public string EmergPhone { get; set; }
        public string Employer { get; set; }
        public string EmployerStatus { get; set; }
        public string Referral { get; set; }
        public string Keyword { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public string FollowUpDesc { get; set; }
        public bool FFL { get; set; }
        public Nullable<System.DateTime> InitialSurveyDate { get; set; }
        public Nullable<System.DateTime> FollowUpSurveyDate { get; set; }
        public string Program { get; set; }
        public bool C1_1Tutoring { get; set; }
        public bool C_1 { get; set; }
        public bool C_2 { get; set; }
        public bool C_3 { get; set; }
        public bool Family_Literacy { get; set; }
        public bool FFL_1 { get; set; }
        public bool FFL_2 { get; set; }
        public bool FFL_3 { get; set; }
        public bool CompLab { get; set; }
        public bool Lab1 { get; set; }
        public bool Lab2 { get; set; }
        public bool Lab3 { get; set; }
        public bool OpenLab { get; set; }
        public bool SmallGroups { get; set; }
        public bool InHouse { get; set; }
        public bool LPT { get; set; }
        public bool C411 { get; set; }
        public bool ESL { get; set; }
        public bool Reading_Sharing { get; set; }
        public bool OffSite { get; set; }
        public bool Project90 { get; set; }
        public bool Jericho { get; set; }
        public bool Bayshore { get; set; }
        public bool Headstart { get; set; }
        public bool AdultSchool { get; set; }
        public string Income { get; set; }
        public string StudentContact { get; set; }
        public string TutorPreference { get; set; }
        public string LocationPref { get; set; }
        public string LocationPref2 { get; set; }
        public string LocationPref3 { get; set; }
        public string Transportation { get; set; }
        public string InterestHobbies { get; set; }
        public string SmokingPref { get; set; }
        public string TutorSmoker { get; set; }
        public string CallLocation { get; set; }
        public string SpecialInstructions { get; set; }
        public string MedicalInfo { get; set; }
        public string NativeLanguage { get; set; }
        public string ReadWriteNativeLang { get; set; }
        public string EducationLevel { get; set; }
        public Nullable<System.DateTime> PreLibraryCard { get; set; }
        public Nullable<System.DateTime> LibraryCard { get; set; }
        public Nullable<System.DateTime> IntakeDate { get; set; }
        public string FirstCalledBy { get; set; }
        public Nullable<System.DateTime> FirstCalledByDate { get; set; }
        public string HowHeard { get; set; }
        public string NeedChildCare { get; set; }
        public Nullable<float> TotalChildren { get; set; }
        public string Child1Name { get; set; }
        public string Child1Gender { get; set; }
        public Nullable<System.DateTime> Child1DateBorn { get; set; }
        public string Child1Ethnicity { get; set; }
        public string Child2Name { get; set; }
        public string Child2Gender { get; set; }
        public Nullable<System.DateTime> Child2DateBorn { get; set; }
        public string Child2Ethnicity { get; set; }
        public string Child3Name { get; set; }
        public string Child3Gender { get; set; }
        public Nullable<System.DateTime> Child3DateBorn { get; set; }
        public string Child3Ethnicity { get; set; }
        public string Child4Name { get; set; }
        public string Child4Gender { get; set; }
        public Nullable<System.DateTime> Child4DateBorn { get; set; }
        public string Child4Ethnicity { get; set; }
        public string Child5Name { get; set; }
        public string Child5Gender { get; set; }
        public Nullable<System.DateTime> Child5DateBorn { get; set; }
        public string Child5Ethnicity { get; set; }
        public string Child6Name { get; set; }
        public string Child6Gender { get; set; }
        public Nullable<System.DateTime> Child6DateBorn { get; set; }
        public string Child6Ethnicity { get; set; }
        public string Child7Name { get; set; }
        public string Child7Gender { get; set; }
        public Nullable<System.DateTime> Child7DateBorn { get; set; }
        public string Child7Ethnicity { get; set; }
        public string Child8Name { get; set; }
        public string Child8Gender { get; set; }
        public Nullable<System.DateTime> Child8DateBorn { get; set; }
        public string Child8Ethnicity { get; set; }
        public string Child9Name { get; set; }
        public string Child9Gender { get; set; }
        public Nullable<System.DateTime> Child9DateBorn { get; set; }
        public string Child9Ethnicity { get; set; }
        public string Child10Name { get; set; }
        public string Child10Gender { get; set; }
        public Nullable<System.DateTime> Child10DateBorn { get; set; }
        public string Child10Ethnicity { get; set; }
        public bool AvailMonAM { get; set; }
        public bool AvailMonPM { get; set; }
        public bool AvailMonEVE { get; set; }
        public bool AvailTueAM { get; set; }
        public bool AvailTuePM { get; set; }
        public bool AvailTueEVE { get; set; }
        public bool AvailWedAM { get; set; }
        public bool AvailWedPM { get; set; }
        public bool AvailWedEVE { get; set; }
        public bool AvailThuAM { get; set; }
        public bool AvailThuPM { get; set; }
        public bool AvailThudEVE { get; set; }
        public bool AvailFriAM { get; set; }
        public bool AvailFriPM { get; set; }
        public bool AvailFriEVE { get; set; }
        public bool AvailSatAM { get; set; }
        public bool AvailSatPM { get; set; }
        public bool AvailSatEVE { get; set; }
        public bool AvailSunAM { get; set; }
        public bool AvailSunPM { get; set; }
        public bool AvailSunEVE { get; set; }
        public Nullable<System.DateTime> AssessIntakeDate { get; set; }
        public string AssessIntel { get; set; }
        public string AssessLevel { get; set; }
        public string AssessWork { get; set; }
        public string AssessFamilyMember { get; set; }
        public string AssessLifeLongLearner { get; set; }
        public string AssessCommunityMember { get; set; }
        public string AssessReadTitle { get; set; }
        public string AssessMiscues { get; set; }
        public Nullable<float> AssessWordRec { get; set; }
        public string Accomplish1 { get; set; }
        public Nullable<System.DateTime> AcDate1 { get; set; }
        public string Accomplish2 { get; set; }
        public Nullable<System.DateTime> AcDate2 { get; set; }
        public string Accomplish3 { get; set; }
        public Nullable<System.DateTime> AcDate3 { get; set; }
        public string Accomplish4 { get; set; }
        public Nullable<System.DateTime> AcDate4 { get; set; }
        public string Accomplish5 { get; set; }
        public Nullable<System.DateTime> AcDate5 { get; set; }
        public string Accomplish6 { get; set; }
        public Nullable<System.DateTime> AcDate6 { get; set; }
        public string Accomplish7 { get; set; }
        public Nullable<System.DateTime> AcDate7 { get; set; }
        public string Accomplish8 { get; set; }
        public Nullable<System.DateTime> AcDate8 { get; set; }
        public string Accomplish9 { get; set; }
        public Nullable<System.DateTime> AcDate9 { get; set; }
        public string Accomplish10 { get; set; }
        public Nullable<System.DateTime> AcDate10 { get; set; }
        public string AccomplishNotes { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string LastModifiedBy { get; set; }
        public string Hyperlink1 { get; set; }
        public string Hyperlink2 { get; set; }
        public string Hyperlink3 { get; set; }
        public string Hyperlink4 { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
