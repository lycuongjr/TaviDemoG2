using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavi.Demo.G2.Models;

namespace Tavi.Demo.G2.Service
{
    public class StudentService
    {
        TaviDemoG2Db db = null;
        public StudentService()
        {
            db = new TaviDemoG2Db();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentCode">Mã sinh viên </param>
        /// <param name="FullName">Họ tên</param>
        /// <param name="DepartmentID">Khoa đang theo học</param>
        /// <param name="PageCurrent">Trang hiện tại</param>
        /// <param name="PageSize">Số bản ghi hiện tại</param>
        /// <returns></returns>
        public IPagedList<Student> GetStudents(string StudentCode, string FullName, int? DepartmentID, int PageCurrent, int PageSize)
        {
            
            var list = db.Students.Where(x => x.Status == true ).AsEnumerable();
            if (!string.IsNullOrEmpty(StudentCode))
            {
                list = list.Where(x => x.StudentCode.Contains(StudentCode)).AsEnumerable();
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                list = list.Where(x => x.StudentCode.Contains(FullName)).AsEnumerable();
            }
            if (DepartmentID.HasValue)
            {
                list = list.Where(x => x.DepartmentID == DepartmentID);
            }

            return list.OrderByDescending(x => x.StudentID).ToPagedList(PageCurrent, PageSize);
        }
        public IPagedList<Student> GetDeleted(string StudentCode, string FullName, int? DepartmentID, int PageCurrent, int PageSize)
        {

            var list = db.Students.Where(x => x.IsDelete == true).AsEnumerable();
            if (!string.IsNullOrEmpty(StudentCode))
            {
                list = list.Where(x => x.StudentCode.Contains(StudentCode)).AsEnumerable();
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                list = list.Where(x => x.StudentCode.Contains(FullName)).AsEnumerable();
            }
            if (DepartmentID.HasValue)
            {
                list = list.Where(x => x.DepartmentID == DepartmentID);
            }

            return list.OrderByDescending(x => x.StudentID).ToPagedList(PageCurrent, PageSize);
        }
        public Student FindByKey(int? StudentID)
        {
            Student student = new Student();
            if (StudentID.HasValue)
            {
                student = db.Students.Find(StudentID);
            }
            return student;
        }
        public void Insert(Student student)
        {
            student.IsDelete = false;
            db.Students.Add(student);
            db.SaveChanges();
        }
        public void Update(Student student)
        {
            db.Students.AddOrUpdate(student);
            db.SaveChanges();
        }
        public void Delete(int? StudentID)
        {
            Student student = new Student();
            if (StudentID.HasValue)
            {
                student = db.Students.Find(StudentID);
                db.Students.Remove(student);
            }
            db.SaveChanges();
        }
        public void DeleteByID(int? StudentID)
        {
            Student student = new Student();
            if (StudentID.HasValue)
            {
                student = db.Students.Find(student);
                student.IsDelete = true;
                db.Students.Add(student);
            }
            db.SaveChanges();
        }
        public bool ChangeStatus(int? id)
        {
            var student = db.Students.Find(id);
            student.Status = !student.Status;
            db.SaveChanges();
            return true;
        }
    }
}
