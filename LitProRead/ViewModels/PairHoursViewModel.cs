using LitProRead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    //[Serializable]
    //[Bind(Exclude = "StudentListLastName, StudentListFirstName")]
    //[Bind(Exclude = "LitProReadEntities")]
    public class PairHoursViewModel
    {
        public int UniqID { get; set; }

        public Nullable<int> TID { get; set; }
        public Nullable<int> SID { get; set; }
        public Nullable<int> PairHours { get; set; }
        public Nullable<System.DateTime> DateMet { get; set; }
        public Nullable<double> HoursMet { get; set; }
        public int ActivityID { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }

        public PairHoursViewModel()
        {
        }

        public void SetTo(PairHour dest, bool create)
        {
            if (create)
            {
                dest.SID = SID;
                dest.TID = TID;
            }
            dest.PairHours = PairHours;
            dest.DateMet = DateMet;
            dest.HoursMet = HoursMet;
            dest.Activity = LitProReadEntities.GetActivity(ActivityID);
            //dest.SSMA_TimeStamp = DateTime.Now.;
        }
    }
}