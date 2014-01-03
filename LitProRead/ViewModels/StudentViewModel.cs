using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitProRead.Controllers;
using LitProRead.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace LitProRead.ViewModels
{
    public class StudentViewModel {
        private LitProReadEntities db = new LitProReadEntities();

        public Student CurrentStudent { get; set; }

        public SelectList SalutationList { get; private set; }

        //public List<string> Students { get; set; }
        public SelectList StudentListLastName { get; private set; }
        public SelectList StudentListFirstName { get; private set; }

        public StudentViewModel()
        {
            this.CurrentStudent = GetStudent(-1);
            this.StudentListLastName = GetStudentsLastName(null);
            this.StudentListFirstName = GetStudentsFirstName(null);
        }

        public Student GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
                return new Student();
            return student;
        }


        // SELECT DISTINCTROW Salutation.Salutation FROM Salutation
        public SelectList GetSalutationList()
        {
            var salutations = db.Database.SqlQuery<string> ("SELECT Name FROM dbo.Salutation").ToList();
            ////List<StateArea> cm = new List<StateArea>();
            //using (LitProReadEntities e = new LitProReadEntities())
            //{
            //    var students = from n in e.Students
            //                   where n.LastName.StartsWith("chi")
            //                   select n;
            //                       //e.Students.ToList();
            return new SelectList(salutations);
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

        //private readonly SqlConnection sqlConnection;
        //public SqlConnection connection
        //{
        //    get
        //    {
        //        sqlConnection.Open();
        //        return sqlConnection;
        //    }
        //}

        public void Dispose()
        {
            //sqlConnection.Close();
            //sqlConnection.Dispose();
            db.Dispose();
        }
    }
}