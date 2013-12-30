using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitProRead.Controllers;
using LitProRead.Models;
using System.Data.Entity;

namespace LitProRead.ViewModels
{
    public class StudentViewModel {
        private LitProReadEntities db = new LitProReadEntities();

        //public List<string> Students { get; set; }
        public SelectList StudentListLastName { get; private set; }
        public SelectList StudentListFirstName { get; private set; }

        public StudentViewModel()
        {
            this.StudentListLastName = GetStudentsLastName(null);
            this.StudentListFirstName = GetStudentsFirstName(null);
        }

        public SelectList GetStudentsLastName(string[] selectedValues)
        {
            var students = from student in db.Students
                           orderby student.LastName
                           select new
                           {
                                ID = student.ID,
                                LastName = student.LastName + ", " + student.FirstName
                           };
            return new SelectList(students, "ID", "LastName", selectedValues);
        }

        public SelectList GetStudentsFirstName(string[] selectedValues)
        {
            var students = from student in db.Students
                           orderby student.FirstName
                           select new
                           {
                               ID = student.ID,
                               FirstName = student.FirstName + " " + student.LastName
                           };

            return new SelectList(students, "ID", "FirstName", selectedValues);
        }
    }
}