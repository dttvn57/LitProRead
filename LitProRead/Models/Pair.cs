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
    
    public partial class Pair
    {
        public int UniqID { get; set; }
        public int TID { get; set; }
        public int SID { get; set; }
        public Nullable<int> LitProID { get; set; }
        public Nullable<int> LitProIDTID { get; set; }
        public Nullable<int> LitProIDSID { get; set; }
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
    }
}
