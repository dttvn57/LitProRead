using LitProRead.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LitProRead.ViewModels
{
    [Serializable]
    public class StudentFormViewModel
    {
        private LitProReadEntities db = new LitProReadEntities();

        public Student CurrentStudent { get; set; }

        public SelectList SalutationList { get; private set; }
        public SelectList AreaCodeList { get; private set; }
        public SelectList CityList { get; private set; }

        //public List<string> Students { get; set; }
        public SelectList StudentListLastName { get; private set; }
        public SelectList StudentListFirstName { get; private set; }

        public StudentFormViewModel(int id = -1)
        {
            this.CurrentStudent = GetStudent(id);
            this.SalutationList = GetSalutationList();
            this.AreaCodeList = GetAreaCodeList();
            this.CityList = GetCityList();
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
            var salutations = db.Database.SqlQuery<string>("SELECT Salutation FROM dbo.Salutation").ToList();
            return new SelectList(salutations);
        }

        // SELECT DISTINCTROW City.City FROM Salutation
        public SelectList GetCityList()
        {
            var cities = db.Database.SqlQuery<string>("SELECT City FROM dbo.City").ToList();
            return new SelectList(cities);
        }

        // SELECT DISTINCTROW AreaCodes.AreaCodes FROM Salutation
        public SelectList GetAreaCodeList()
        {
            var codes = db.Database.SqlQuery<string>("SELECT AreaCodes FROM dbo.AreaCodes").ToList();
            return new SelectList(codes);
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


        //public StudentViewModel studentVM { get; set; }
}