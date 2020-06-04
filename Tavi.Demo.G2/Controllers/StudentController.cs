using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tavi.Demo.G2.Helper;
using Tavi.Demo.G2.Models;
using Tavi.Demo.G2.Service;

namespace Tavi.Demo.G2.Controllers
{
    public class StudentController : Controller
    {
        #region Danh sach sien vien
        
        [HttpGet]
        public ActionResult Index(string StudentCode, string FullName, int? DepartmentID, int? PageCurrent)
        {
            StudentService service = new StudentService();
            int pageNumber = PageCurrent ?? 1;
            IPagedList<Student> students = service.GetStudents( StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = PageCurrent;
            return View(students);

        }
        [HttpPost]
        public ActionResult Index(string StudentCode, string FullName, int? DepartmentID)
        {
            StudentService service = new StudentService();
            int pageNumber =   1;
            IPagedList<Student> students = service.GetStudents(StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = pageNumber;
            return View(students);

        }
        [HttpGet]
        public ActionResult Deleted(string StudentCode, string FullName, int? DepartmentID, int? PageCurrent)
        {
            StudentService service = new StudentService();
            int pageNumber = PageCurrent ?? 1;
            IPagedList<Student> students = service.GetDeleted(StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = PageCurrent;
            return View(students);
        }
        [HttpPost]
        public ActionResult Deleted(string StudentCode, string FullName, int? DepartmentID)
        {
            StudentService service = new StudentService();
            int pageNumber = 1;
            IPagedList<Student> students = service.GetStudents(StudentCode
                , FullName
                , DepartmentID
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = StudentCode;
            ViewBag.FullName = FullName;
            ViewBag.PageCurrent = pageNumber;
            return View(students);

        }
        #endregion

        #region Them moi sinh vien
        public ActionResult Add(int? id)
        {
            StudentService service = new StudentService();
            Student student = service.FindByKey(id);
            return View(student);
        }
        public PartialViewResult ListDeparment(int? DeparmentID)
        {
            DeparmentService service = new DeparmentService();

            ViewBag.ListDepartmentID = new SelectList(service.GetAllDepartments(), "DepartmentID", "DepartmentName", DeparmentID);
            return PartialView("_Department");
        }
        public PartialViewResult ListClassRoom(int? DepartmentID, int? ClassRoomID)
        {
            ClassRoomService service = new ClassRoomService();

            ViewBag.ListClassRoomID = new SelectList(service.GetAllClassRoom(DepartmentID), "ClassRoomID", "ClassName", ClassRoomID);
            return PartialView("_ClassRoom");
        }
        [HttpPost]
        public ActionResult Add(int? Id
            , string StudentCode
            , string FullName
            , string Birthday
            , string Address
            , string Phone
            , string Email
            , string Description
            , string ListDepartmentID
            , string ListClassRoomID
            , bool Status
            )
        {
            StudentService studentService = new StudentService();
            Student student = studentService.FindByKey(Id);
            student.StudentCode = StudentCode;
            student.FullName = FullName;
            if (!string.IsNullOrEmpty(Birthday))
                student.Birthday = ConvertEx.ToDate(Birthday);
            student.Address = Address;
            student.Phone = Phone;
            student.Email = Email;
            if (!string.IsNullOrEmpty(ListDepartmentID))
                student.DepartmentID = Convert.ToInt32(ListDepartmentID);
            if (!string.IsNullOrEmpty(ListClassRoomID))
                student.ClassRoomID = Convert.ToInt32(ListClassRoomID);
            student.Description = Description;
            student.Status = Status;
            student.IsDelete = false;
            if (Id.HasValue)
            {
                studentService.Update(student);
                setAlert("Sửa sinh vien thành công", "success");


            }
            else
            {
                studentService.Insert(student);
                setAlert("Thêm sinh vien thành công", "success");

            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Xoa sinh vien
        [HttpPost]
        public ActionResult Delete(int [] cbxItem)
        {
            if (cbxItem.Count() > 0)
            {
                foreach (int item in cbxItem)
                {
                    StudentService studentService = new StudentService();
                    studentService.Delete(item);
                    setAlert("Xóa sinh vien thành công", "success");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Recycle(int? StudentID)
        {
            if (StudentID > 0)
            {               
                    StudentService service = new StudentService();
                    service.DeleteByID(StudentID);               
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region alert
        public void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;

            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";

            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        #endregion
    }
}
