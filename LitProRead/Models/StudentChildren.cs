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
    
    public partial class StudentChildren
    {
        public int AutoNum { get; set; }
        public Nullable<int> ID { get; set; }
        public string ChildName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public Nullable<bool> LiveWithAdult { get; set; }
        public string ChildRelationship { get; set; }
        public string Comments { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}