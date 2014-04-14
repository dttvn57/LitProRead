using LitProRead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    public class TutorFollowUpViewModel
    {
        public bool New { get; set; }
        public int UniqID { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public string FollowUpDesc { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string LastModifiedBy { get; set; }

        public TutorFollowUpViewModel()
        {
        }


        public void SetTo(TutorFollowUp dest, bool create)
        {
            if (create == false)        // modify a record
            {
                dest.UniqID = UniqID;
            }
            dest.ID = ID;
            dest.FollowUpDate = FollowUpDate;
            dest.FollowUpDesc = FollowUpDesc;
            dest.DateCreated = DateCreated;
            dest.DateModified = DateModified;
            dest.LastModifiedBy = LastModifiedBy;
        }
    }
}