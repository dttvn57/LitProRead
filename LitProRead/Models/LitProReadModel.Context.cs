﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LitProRead.Extensions;

namespace LitProRead.Models
{
    using LitProRead.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    //using LitProRead.Extensions;

    
    public partial class LitProReadEntities : DbContext
    {
        public LitProReadEntities()
            : base("name=LitProReadEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public IQueryable<Student> IStudents { get; set; }

        //Return only the results we want
        public List<Student> GetStudents(bool ActiveOnly, string searchTerm, int pageSize, int pageNum, bool byLastName)
        {
            return GetStudentsQuery(ActiveOnly, searchTerm, byLastName)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();
        }

        //And the total count of records
        public int GetStudentsCount(bool ActiveOnly, string searchTerm, int pageSize, int pageNum)
        {
            return GetStudentsQuery(ActiveOnly, searchTerm, true)
                .Count();
        }


        //Our search term
        private IQueryable<Student> GetStudentsQuery(bool ActiveOnly, string searchTerm, bool byLastName)
        {
            searchTerm = searchTerm.ToLower();
            if (byLastName)
            {
                if (ActiveOnly)
                    return Students
                        .Where(
                            a =>
                            a.LastName.StartsWith(searchTerm) && a.Active == true
                        ).OrderBy(a => a.LastName).ThenBy(a => a.FirstName);
                else
                     return Students
                        .Where(
                            a =>
                            a.LastName.StartsWith(searchTerm)
                        ).OrderBy(a => a.LastName).ThenBy(a => a.FirstName);
           }
            else
            {
                if (ActiveOnly)
                    return Students
                    .Where(
                        a =>
                        a.FirstName.StartsWith(searchTerm) && a.Active == true
                    ).OrderBy(a => a.FirstName).ThenBy(a => a.LastName);
                else
                    return Students
                    .Where(
                        a =>
                        a.FirstName.StartsWith(searchTerm)
                    ).OrderBy(a => a.FirstName).ThenBy(a => a.LastName);
            }
        }

        //Return only the results we want
        public List<Tutor> GetTutors(string searchTerm, int pageSize, int pageNum, bool byLastName)
        {
            return GetTutorsQuery(searchTerm, byLastName)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();
        }

        //And the total count of records
        public int GetTutorsCount(string searchTerm, int pageSize, int pageNum)
        {
            return GetTutorsQuery(searchTerm, true)
                .Count();
        }

        public List<PairViewModel> GetMatchedTutorForStudent(int studentID, int pageSize, int pageNum, string sort, ref int matchCount)
        {
            var stuStatus = Students.Find(studentID).Status;
            var pairs = 
                from pair in Pairs
                where pair.SID == studentID
                join tutor in Tutors on pair.TID equals tutor.ID //into pt 
                //from p in pt.DefaultIfEmpty() 
                select new { SID = studentID,
                             TID = pair.TID,
                            TutorLastName = tutor.LastName,
                            TutorFirstName = tutor.FirstName,
                            TStatus = tutor.Status,
                            SStatus = stuStatus,
                            MatchDate = pair.MatchDate,
                            DissolveDate = pair.DissolveDate,
                            PairStatus = pair.PairStatus,
                            PairStatusDate = pair.PairStatusDate,
                            PairProgram = pair.PairProgram,
                            DateCreated = pair.DateCreated,
                            DateModified = pair.DateModified,
                            LastModifiedBy = pair.LastModifiedBy,
                            SSMA_TimeStamp = pair.SSMA_TimeStamp,
                            UniqID = pair.UniqID,
                            Comments = pair.Comments
                };

            matchCount = pairs.Count();

            // get the Total Hours Met for the Tutors.
            var query = pairs.GroupJoin(PairHours,
                                              p => p.UniqID,
                                              ph => ph.PairHours,
                                              (d, t) => new
                                              {
                                                  UniqID = d.UniqID,
                                                  SID = d.SID,
                                                  TID = d.TID,
                                                  TutorLName = d.TutorLastName,
                                                  TutorFName = d.TutorFirstName,
                                                  MatchDate = d.MatchDate,
                                                  PairStatus = d.PairStatus,
                                                  DissolveDate = d.DissolveDate,
                                                  PairStatusDate = d.PairStatusDate,
                                                  PairProgram = d.PairProgram,
                                                  DateCreated = d.DateCreated,
                                                  DateModified = d.DateModified,
                                                  LastModifiedBy = d.LastModifiedBy,
                                                  SSMA_TimeStamp = d.SSMA_TimeStamp,
                                                  TStatus = d.TStatus,
                                                  SStatus = d.SStatus,     
                                                  Comments = d.Comments,
                                                  TotalHoursMet = t.Sum(x => x.HoursMet)
                                              });

            if (string.IsNullOrEmpty(sort) || sort.Equals("DateCreated ASC"))
            {
                query = query.OrderBy(p => p.DateCreated);
            }
            else if (sort.Equals("DateCreated DESC"))
            {
                query = query.OrderByDescending(p => p.DateCreated);
            }

            // consolidate results.
            List<PairViewModel> list = new List<PairViewModel>();
            foreach (var pair in query)
            {
                int pairStatusId = GetStatusId(pair.PairStatus);
                int tStatusId = GetStatusId(pair.TStatus);
                int sStatusId = GetStatusId(pair.SStatus);
                list.Add(new PairViewModel
                {
                    UniqID = pair.UniqID,
                    SID = pair.SID,
                    TID = pair.TID,
                    TutorLName = pair.TutorLName,
                    TutorFName = pair.TutorFName,
                    MatchDate = pair.MatchDate,
                    DissolveDate = pair.DissolveDate,
                    PairStatusID = pairStatusId,
                    PairStatusDate = pair.PairStatusDate,
                    PairProgram = pair.PairProgram,
                    DateCreated = pair.DateCreated,
                    DateModified = pair.DateModified,
                    LastModifiedBy = pair.LastModifiedBy,
                    SSMA_TimeStamp = pair.SSMA_TimeStamp,
                    TStatusID = tStatusId,
                    SStatusID = sStatusId,
                    Comments = pair.Comments,
                    TotalHoursMet = pair.TotalHoursMet
                });
            }

            if (pageSize > 0)
            {
                IEnumerable <PairViewModel> retList = list.AsEnumerable();
                return retList.Skip(pageNum).Take(pageSize).ToList();
            }
            else
                return list;
        }

        public List<StudentChildren> GetStudentChildren(int id)
        {
            var query = from c in StudentChildrens
                        where c.ID == id
                        select c;

            if (query.Count() == 0)
                return new List<StudentChildren>();

            return query.ToList();
        }


        /**
         using (SFA2DBDataContext db = new SFA2DBDataContext())
            {
                var foo = from a in db.KitItems
                          join b in db.PackageKits on a.KitID equals b.KitID
                          join c in db.BundlePackages on b.PackageID equals c.PackageID
                          join d in db.BuilderOverrideBundlePackageKitItems on 
                          new { doo=a.ID, goo=b.ID }
                          equals new { doo=d.KitItemID, goo=d.PackageKitID }
                          select new
                          {
                              a.ItemID,a.KitID,b.PackageID,c.BundleID,
                              Commish = d.BuilderCommission ?? a.Item.BuilderCommissionPercent
                          };
            }
         * 
         *===========================================================
         *
        SELECT PairHours.DateMet, PairHours.HoursMet, 
        Tutors.FirstName & " " & Tutors.LastName AS TutorName, Students.FirstName & " " & Students.Lastname AS StudentName, 
        Pairs.DissolveDate, PairHours.Activity, Pairs.PairStatus, Pairs.MatchDate, 
        IIf(IsNull([DissolveDate]),
        DateDiff("m",[MatchDate],Now()),
        DateDiff("m",[MatchDate],[DissolveDate])) AS MthofSvc, 
        Pairs.PairProgram, Pairs.PairStatusDate

        FROM Students RIGHT JOIN ((Tutors RIGHT JOIN Pairs ON Tutors.ID = Pairs.TID) RIGHT JOIN PairHours ON Pairs.UniqID = PairHours.PairHours) ON Students.ID = Pairs.SID
        WHERE (((PairHours.DateMet) Between [Forms]![frmDateSelectionPairStatus]![BeginDate] And [Forms]![frmDateSelectionPairStatus]![EndDate]));
         */
        public List<PairHoursViewModel> GetPairHoursForStudentAndTutor(int studentID, int tutorID, int pageSize, int pageNum, string sort = null)
        {
            //var stuStatus = Students.Find(studentID).Status;
            //var result = from t in ints1
            //             join x in ints2 on (t + 1) equals x
            //             select t;

            //var pairs =
            //    from student in Students
            //    join
            //        (from tutor in Tutors
            //        join pair in Pairs on tutor.ID equals pair.TID)
            //    on student.ID equals pair.SID
            //select new { pair.DateMet };

            //    where pairHour.SID == studentID && pairHour.TID == tutorID


            var query =
                from pair in Pairs
                where pair.SID == studentID && pair.TID == tutorID
                select pair into pairGrp

                join pairHour in PairHours on pairGrp.UniqID equals pairHour.PairHours into pairPairHours

                from p in pairPairHours.DefaultIfEmpty()

                select p;
                //select new
                //{
                //    p.UniqID,
                //    p.PairHours,
                //    p.DateMet,
                //    p.HoursMet,
                //    p.Activity
                //};

            if (string.IsNullOrEmpty(sort) || sort.Equals("DateMet ASC"))
            {
                query = query.OrderBy(p => p.DateMet);
            }
            else if (sort.Equals("DateCreated DESC"))
            {
                query = query.OrderByDescending(p => p.DateMet);
            }

            List<PairHoursViewModel> list = new List<PairHoursViewModel>();
            if (query.First() != null)
            {
                foreach (var pairHr in query)
                {
                    int activityId = GetActivityId(pairHr.Activity);
                    list.Add(new PairHoursViewModel
                    {
                        UniqID = pairHr.UniqID,
                        PairHours = pairHr.PairHours,
                        DateMet = pairHr.DateMet,
                        HoursMet = pairHr.HoursMet,
                        ActivityID = activityId,   //pair.Activity
                    });
                }
            }
            return list;
        }




        //Our search term
        private IQueryable<Tutor> GetTutorsQuery(string searchTerm, bool byLastName)
        {
            searchTerm = searchTerm.ToLower();
            if (byLastName)
            {
                return Tutors
                    .Where(
                        a =>
                        a.LastName.StartsWith(searchTerm)
                    ).OrderBy(a => a.LastName);
            }
            else
            {
                return Tutors
                    .Where(
                        a =>
                        a.FirstName.StartsWith(searchTerm)
                    ).OrderBy(a => a.FirstName);
            }
        }

        public static string GetActivity(int activityId)
        {
            switch (activityId)
            {
                case 1:
                    return "Travel Time";
                case 2:
                    return "Tutoring";
                case 0:
                default:
                    return "Prep Time";
            }
        }
        private int GetActivityId(string activity)
        {
            if (activity.Equals("Prep Time", StringComparison.OrdinalIgnoreCase))
                return 0;
            else if (activity.Equals("Travel Time", StringComparison.OrdinalIgnoreCase))
                return 1;
            else if (activity.Equals("Tutoring", StringComparison.OrdinalIgnoreCase))
                return 2;
            return 0;
        }

        public static string GetStatus(int statusId)
        {
            switch (statusId)
            {
                case 1:
                    return "Dissolved";
                case 2:
                    return "Inactive";
                case 3:
                    return "Left";
                case 4:
                    return "Prospective";
                case 5:
                    return "Sabbatical";
                case 6:
                    return "Waiting";
                case 0:
                default:
                    return "Active";
            }
        }

        private int GetStatusId(string status)
        {
            if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                return 0;
            else if (status.Equals("Dissolved", StringComparison.OrdinalIgnoreCase))
                return 1;
            else if (status.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
                return 2;
            else if (status.Equals("Left", StringComparison.OrdinalIgnoreCase))
                return 3;
            else if (status.Equals("Prospective", StringComparison.OrdinalIgnoreCase))
                return 4;
            else if (status.Equals("Sabbatical", StringComparison.OrdinalIgnoreCase))
                return 5;
            else if (status.Equals("Waiting", StringComparison.OrdinalIgnoreCase))
                return 6;
           return 0;
        }

        public DbSet<ChildRelationship> ChildRelationships { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Class___save> Class___save { get; set; }
        public DbSet<Class_12_09_Export> Class_12_09_Export { get; set; }
        public DbSet<ClassAttendanceDate> ClassAttendanceDates { get; set; }
        public DbSet<ClassRegistrationHistory> ClassRegistrationHistories { get; set; }
        public DbSet<ClassSignUp> ClassSignUps { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<ComputerHour> ComputerHours { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DonorHistory> DonorHistories { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<EventDonationHistory> EventDonationHistories { get; set; }
        public DbSet<EventRegistrationHistory> EventRegistrationHistories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<PairHour> PairHours { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<Pairs_export_12_09> Pairs_export_12_09 { get; set; }
        public DbSet<RolesAndGoal> RolesAndGoals { get; set; }
        public DbSet<Sheet3> Sheet3 { get; set; }
        public DbSet<SmallGroupBuilding> SmallGroupBuildings { get; set; }
        public DbSet<SmallGroupClassroom> SmallGroupClassrooms { get; set; }
        public DbSet<SmallGroupClassStatu> SmallGroupClassStatus { get; set; }
        public DbSet<SmallGroupKeyword> SmallGroupKeywords { get; set; }
        public DbSet<SmallGroupLocation> SmallGroupLocations { get; set; }
        public DbSet<SmallGroupProgram> SmallGroupPrograms { get; set; }
        public DbSet<SmallGroupRegistrationHistory> SmallGroupRegistrationHistories { get; set; }
        public DbSet<SmallGroupRegistrationHour> SmallGroupRegistrationHours { get; set; }
        public DbSet<SmallGroup> SmallGroups { get; set; }
        public DbSet<SmallGroupSemester> SmallGroupSemesters { get; set; }
        public DbSet<SmallGroupTeacher> SmallGroupTeachers { get; set; }
        public DbSet<SmallGroupTime> SmallGroupTimes { get; set; }
        public DbSet<Student_Export_12_09> Student_Export_12_09 { get; set; }
        public DbSet<StudentAccomplishment> StudentAccomplishments { get; set; }
        public DbSet<StudentChildren> StudentChildrens { get; set; }
        public DbSet<StudentComment> StudentComments { get; set; }
        public DbSet<StudentContact> StudentContacts { get; set; }
        public DbSet<StudentFollowUp> StudentFollowUps { get; set; }
        public DbSet<StudentQuarterlyStartingNum> StudentQuarterlyStartingNums { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<tblAuditTrail> tblAuditTrails { get; set; }
        public DbSet<tblAuditTrailStudentStatu> tblAuditTrailStudentStatus { get; set; }
        public DbSet<tblAuditTrailTutorStatu> tblAuditTrailTutorStatus { get; set; }
        public DbSet<tblStatusHistory> tblStatusHistories { get; set; }
        public DbSet<Tutor_Export_12_09> Tutor_Export_12_09 { get; set; }
        public DbSet<TutorComment> TutorComments { get; set; }
        public DbSet<TutorContact> TutorContacts { get; set; }
        public DbSet<TutorFollowUp> TutorFollowUps { get; set; }
        public DbSet<TutorMatchHistory> TutorMatchHistories { get; set; }
        public DbSet<TutorQuarterlyStartingNum> TutorQuarterlyStartingNums { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<VolunteerHistory> VolunteerHistories { get; set; }
        public DbSet<VolunteerHour> VolunteerHours { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<WorkshopRegistrationHistory> WorkshopRegistrationHistories { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Contacts_Query> Contacts_Queries { get; set; }
        public DbSet<Contacts_Query_Query> Contacts_Query_Queries { get; set; }
        public DbSet<Contacts_Query1> Contacts_Query1 { get; set; }
        public DbSet<Contacts_Query10> Contacts_Query10 { get; set; }
        public DbSet<Contacts_Query11> Contacts_Query11 { get; set; }
        public DbSet<Contacts_Query12> Contacts_Query12 { get; set; }
        public DbSet<Contacts_Query13> Contacts_Query13 { get; set; }
        public DbSet<Contacts_Query14> Contacts_Query14 { get; set; }
        public DbSet<Contacts_Query2> Contacts_Query2 { get; set; }
        public DbSet<Contacts_Query3> Contacts_Query3 { get; set; }
        public DbSet<Contacts_Query4> Contacts_Query4 { get; set; }
        public DbSet<Contacts_Query5> Contacts_Query5 { get; set; }
        public DbSet<Contacts_Query6> Contacts_Query6 { get; set; }
        public DbSet<Contacts_Query7> Contacts_Query7 { get; set; }
        public DbSet<Contacts_Query8> Contacts_Query8 { get; set; }
        public DbSet<Contacts_Query9> Contacts_Query9 { get; set; }
        public DbSet<qry2008Learners> qry2008Learners { get; set; }
        public DbSet<qryActiveFFLPair> qryActiveFFLPairs { get; set; }
        public DbSet<qryActiveFFLStudentReport> qryActiveFFLStudentReports { get; set; }
        public DbSet<qryAssessmentFollowUp> qryAssessmentFollowUps { get; set; }
        public DbSet<qryClass> qryClasses { get; set; }
        public DbSet<qryClassAttendanceDate> qryClassAttendanceDates { get; set; }
        public DbSet<qryClassAttendanceList> qryClassAttendanceLists { get; set; }
        public DbSet<qryClassRegistrationHistory> qryClassRegistrationHistories { get; set; }
        public DbSet<qryClassSignUp> qryClassSignUps { get; set; }
        public DbSet<qryComputer> qryComputers { get; set; }
        public DbSet<qryComputerLab> qryComputerLabs { get; set; }
        public DbSet<qryContactFollowUp> qryContactFollowUps { get; set; }
        public DbSet<qryContactFollowUpReport> qryContactFollowUpReports { get; set; }
        public DbSet<qryContact> qryContacts { get; set; }
        public DbSet<qryContacts_Query> qryContacts_Queries { get; set; }
        public DbSet<qryDonorFollowUp> qryDonorFollowUps { get; set; }
        public DbSet<qryDonorHistory> qryDonorHistories { get; set; }
        public DbSet<qryDonorHistoryFollowUp> qryDonorHistoryFollowUps { get; set; }
        public DbSet<qryDonor> qryDonors { get; set; }
        public DbSet<qryEventDonationHistory> qryEventDonationHistories { get; set; }
        public DbSet<qryEventDonation> qryEventDonations { get; set; }
        public DbSet<qryEventDonationTotal> qryEventDonationTotals { get; set; }
        public DbSet<qryEventRegistrationHistory> qryEventRegistrationHistories { get; set; }
        public DbSet<qryEvent> qryEvents { get; set; }
        public DbSet<qryFriend> qryFriends { get; set; }
        public DbSet<qryFriendsFollowUp> qryFriendsFollowUps { get; set; }
        public DbSet<qryLabel> qryLabels { get; set; }
        public DbSet<qryPair> qryPairs { get; set; }
        public DbSet<qryPairsMetMoreThan12Hours2> qryPairsMetMoreThan12Hours2 { get; set; }
        public DbSet<qryPairsMoreThan12Hours> qryPairsMoreThan12Hours { get; set; }
        public DbSet<qryPairsStudentsMetWithTutorsLessThan12HoursSum> qryPairsStudentsMetWithTutorsLessThan12HoursSum { get; set; }
        public DbSet<qryPairsStudentsMetWithTutorsMoreThan12HoursSum> qryPairsStudentsMetWithTutorsMoreThan12HoursSum { get; set; }
        public DbSet<qryPairsThi> qryPairsThis { get; set; }
        public DbSet<qryPairsTutorsMetWithStudentsLessThan12HoursSum> qryPairsTutorsMetWithStudentsLessThan12HoursSum { get; set; }
        public DbSet<qryPairsTutorsMetWithStudentsMoreThan12HoursSum> qryPairsTutorsMetWithStudentsMoreThan12HoursSum { get; set; }
        public DbSet<qryPortfolioReport> qryPortfolioReports { get; set; }
        public DbSet<qrySmallGroupRegistrationHistory> qrySmallGroupRegistrationHistories { get; set; }
        public DbSet<qrySmallGroup> qrySmallGroups { get; set; }
        public DbSet<qryStudentAccomplishment> qryStudentAccomplishments { get; set; }
        public DbSet<qryStudentBDay> qryStudentBDays { get; set; }
        public DbSet<qryStudentComment> qryStudentComments { get; set; }
        public DbSet<qryStudentFollowUp> qryStudentFollowUps { get; set; }
        public DbSet<qryStudentFollowUpContinuou> qryStudentFollowUpContinuous { get; set; }
        public DbSet<qryStudentFollowUpReport> qryStudentFollowUpReports { get; set; }
        public DbSet<qryStudentFollowUpToday> qryStudentFollowUpTodays { get; set; }
        public DbSet<qryStudentMatchHistoryTutor> qryStudentMatchHistoryTutors { get; set; }
        public DbSet<qryStudent> qryStudents { get; set; }
        public DbSet<qryStudentsActiveMoreThan1Year> qryStudentsActiveMoreThan1Year { get; set; }
        public DbSet<qryStudentsActivePhoneList> qryStudentsActivePhoneLists { get; set; }
        public DbSet<qryStudentsInActivePhoneList> qryStudentsInActivePhoneLists { get; set; }
        public DbSet<qryStudentStatusHistory> qryStudentStatusHistories { get; set; }
        public DbSet<qryStudentStatusHistory_Query> qryStudentStatusHistory_Queries { get; set; }
        public DbSet<qryTutorBDay> qryTutorBDays { get; set; }
        public DbSet<qryTutorComment> qryTutorComments { get; set; }
        public DbSet<qryTutorContactChoice> qryTutorContactChoices { get; set; }
        public DbSet<qryTutorFollowUp> qryTutorFollowUps { get; set; }
        public DbSet<qryTutorFollowUpContinuou> qryTutorFollowUpContinuous { get; set; }
        public DbSet<qryTutorFollowUpReport> qryTutorFollowUpReports { get; set; }
        public DbSet<qryTutorFollowUpToday> qryTutorFollowUpTodays { get; set; }
        public DbSet<qryTutorInActivePhoneList> qryTutorInActivePhoneLists { get; set; }
        public DbSet<qryTutorMatchHistoryStudent> qryTutorMatchHistoryStudents { get; set; }
        public DbSet<qryTutor> qryTutors { get; set; }
        public DbSet<qryTutorsActivePhoneList> qryTutorsActivePhoneLists { get; set; }
        public DbSet<qryTutorStatusHistory> qryTutorStatusHistories { get; set; }
        public DbSet<qryVolunteerFollowUp> qryVolunteerFollowUps { get; set; }
        public DbSet<qryVolunteerHistory> qryVolunteerHistories { get; set; }
        public DbSet<qryVolunteerHour> qryVolunteerHours { get; set; }
        public DbSet<qryVolunteer> qryVolunteers { get; set; }
        public DbSet<qryWkShop> qryWkShops { get; set; }
        public DbSet<qryWkShopsHistory> qryWkShopsHistories { get; set; }
        public DbSet<qryWorkshopRegistrationFollowUp> qryWorkshopRegistrationFollowUps { get; set; }
        public DbSet<qryWorkshopRegistrationHistory> qryWorkshopRegistrationHistories { get; set; }
        public DbSet<qryWorkshopRegistrationHistoryALL> qryWorkshopRegistrationHistoryALLs { get; set; }
        public DbSet<qryWorkshop> qryWorkshops { get; set; }
        public DbSet<qryWorkstopRegistrationHistoryNEW> qryWorkstopRegistrationHistoryNEWs { get; set; }
        public DbSet<Query2> Query2 { get; set; }
        public DbSet<StudentContact_Query> StudentContact_Queries { get; set; }
        public DbSet<StudentsIn94555> StudentsIn94555 { get; set; }
    }
}
