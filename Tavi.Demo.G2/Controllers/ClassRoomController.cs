using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tavi.Demo.G2.Models;
using Tavi.Demo.G2.Service;

namespace Tavi.Demo.G2.Controllers
{
    public class ClassRoomController : Controller 
    {

        #region Danh sach lop hoc
        [HttpGet]
        public ActionResult Index(string ClassRoomName, int? CurrentPage)
        {
            ClassRoomService service = new ClassRoomService();
            int pageNumber = CurrentPage ?? 1;
            IPagedList<ClassRoom> classRooms = service.GetClassRoom(ClassRoomName              
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = ClassRoomName;          
            ViewBag.PageCurrent = CurrentPage;
            return View(classRooms);

        }
        [HttpPost]
        public ActionResult Index(string ClassName)
        {
            ClassRoomService service = new ClassRoomService();
            int pageNumber = 1;
            IPagedList<ClassRoom> classRooms = service.GetClassRoom(ClassName
                , pageNumber
                , 10
                );
            ViewBag.ClassName = ClassName;
            ViewBag.PageCurrent = pageNumber;
            return View(classRooms);

        }
        #endregion

        #region Them moi lop hoc
        public ActionResult Add(int? id)
        {
            ClassRoomService service = new ClassRoomService();
            ClassRoom classRoom = service.FindByKey(id);
            return View(classRoom);
        }
        public PartialViewResult ListDeparment(int? DeparmentID)
        {
            DeparmentService service = new DeparmentService();

            ViewBag.ListDepartmentID = new SelectList(service.GetAllDepartments(), "DepartmentID", "DepartmentName", DeparmentID);
            return PartialView("_Department");
        }
       
        [HttpPost]
        public ActionResult Add(int? Id
            , string ClassName
            , string Description           
            , string ListDepartmentID           
            , bool Status
            )
        {
            ClassRoomService classRoomService = new ClassRoomService();
            ClassRoom classRoom = classRoomService.FindByKey(Id);
            classRoom.ClassName = ClassName;
            classRoom.Description = Description;                     
            if (!string.IsNullOrEmpty(ListDepartmentID))
                classRoom.DepartmentID = Convert.ToInt32(ListDepartmentID);

            classRoom.Description = Description;
            classRoom.Status = Status;
            classRoom.IsDelete = false;
            if (Id.HasValue)
            {
                classRoomService.Update(classRoom);
                setAlert("Sửa lớp thành công", "success");


            }
            else
            {
                classRoomService.Insert(classRoom);
                setAlert("Thêm lớp thành công", "success");

            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Xoa lop
        [HttpPost]
        public ActionResult Delete(int[] cbxItem)
        {
            if (cbxItem.Count() > 0)
            {
                foreach (int item in cbxItem)
                {
                    DeparmentService deparmentService = new DeparmentService();
                    deparmentService.Delete(item);
                    setAlert("Xóa lớp thành công", "success");

                }
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
