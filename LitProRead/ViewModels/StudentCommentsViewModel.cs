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
    public class StudentCommentsViewModel
    {
        public int Index { get; set; }          // used temporarily to keep track track of which comment.
        public bool New { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
        public string Comment { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }

        public StudentCommentsViewModel()
        {
        }


        public void SetTo(StudentComment dest, bool create)
        {
            if (create)
            {
                dest.ID = ID;
            }
            dest.CommentDate = CommentDate;
            dest.Comment = Comment;
            //dest.SSMA_TimeStamp = DateTime.Now.;
        }
    }
}