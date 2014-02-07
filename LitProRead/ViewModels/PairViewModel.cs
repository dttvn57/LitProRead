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
    public class PairViewModel
    {
        public int Id { get; set; }
        
        public int TID { get; set; }
        public int SID { get; set; }
        public string TutorFName { get; set; }
        public string TutorLName { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public Nullable<System.DateTime> MatchDate { get; set; }
        public Nullable<System.DateTime> DissolveDate { get; set; }
        public string PairStatus { get; set; }
        public Nullable<System.DateTime> PairStatusDate { get; set; }
        public string PairProgram { get; set; }
        public string Comments { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string LastModifiedBy { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
        public string SStatus { get; set; }
        public string TStatus { get; set; }

        public PairViewModel()
        {
        }

        public void LoadStudent(int id)
        {
        }

        public void LoadTutor(int id)
        {
        }
    }
}