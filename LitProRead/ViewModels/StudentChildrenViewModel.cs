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
    public class StudentChildrenViewModel
    {
        public int AutoNum { get; set; }
        public Nullable<int> ID { get; set; }
        public string ChildName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public int GenderID { get; set; }
        public int EthnicityID { get; set; }
        public Nullable<bool> LiveWithAdult { get; set; }
        public int ChildRelationshipID { get; set; }
        public string Comments { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }

        public StudentChildrenViewModel()
        {
        }


        public void SetTo(StudentChildren dest, bool create)
        {
            if (create)
            {
                dest.ID = ID;
            }
            dest.ChildName = ChildName;
            dest.DOB = DOB;
            dest.Gender = LitProReadEntities.GetGender(GenderID);
            dest.Ethnicity = LitProReadEntities.GetEthnicity(EthnicityID);
            dest.LiveWithAdult = LiveWithAdult;
            dest.ChildRelationship = LitProReadEntities.GetChildRelationship(ChildRelationshipID);
            dest.Comments = Comments;
            //dest.SSMA_TimeStamp = DateTime.Now.;
        }
    }
}