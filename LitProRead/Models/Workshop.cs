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
    
    public partial class Workshop
    {
        public int ID { get; set; }
        public string SessionName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Status { get; set; }
        public string SessionType { get; set; }
        public string Instructors { get; set; }
        public Nullable<float> WorkshopLength { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public string FollowUpDesc { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string LastModifiedBy { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
