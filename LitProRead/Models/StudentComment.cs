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
    
    public partial class StudentComment
    {
        public Nullable<int> ID { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
        public string Comment { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}