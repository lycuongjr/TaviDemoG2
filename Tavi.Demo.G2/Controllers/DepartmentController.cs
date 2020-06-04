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
    public class DepartmentController : Controller
    {
        #region Danh sach lop hoc
        [HttpGet]
        public ActionResult Index(string DepartmentName, int? CurrentPage)
        {
            DeparmentService deparmentService = new DeparmentService();
            int pageNumber = CurrentPage ?? 1;
            IPagedList<Department> departments = deparmentService.GetDepartments(DepartmentName
                , pageNumber
                , 10
                );
            ViewBag.StudentCode = DepartmentName;
            ViewBag.PageCurrent = CurrentPage;
            return View(departments);

        }
        [HttpPost]
        public ActionResult Index(string DepartmentName)
        {
            DeparmentService service = new DeparmentService();
            int pageNumber = 1;
            IPagedList<Department> departments = service.GetDepartments(DepartmentName
                , pageNumber
                , 10
                );
            ViewBag.DepartmentName = DepartmentName;
            ViewBag.PageCurrent = pageNumber;
            return View(departments);

        }
        #endregion

        #region Them moi khoa
        public ActionResult Add(int? id)
        {
            DeparmentService service = new DeparmentService();
            Department department = service.FindByKey(id);
            return View(department);
        }        
        [HttpPost]
        public ActionResult Add(int? Id
            , string DepartmentName
            , string Description
            , bool Status
            )
        {
            DeparmentService deparmentService = new DeparmentService();
            Department department = deparmentService.FindByKey(Id);
            department.DepartmentName = DepartmentName;
            department.Description = Description;           
            department.Status = Status;          
            if (Id.HasValue)
            {
                deparmentService.Update(department);
                setAlert("Sửa khoa thành công", "success");

            }
            else
            {
                deparmentService.Insert(department);
                setAlert("Thêm khoa thành công", "success");


            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Xoa khoa
        [HttpPost]
        public ActionResult Delete(int[] cbxItem)
        {
            if (cbxItem.Count() > 0)
            {
                foreach (int item in cbxItem)
                {
                    DeparmentService deparmentService = new DeparmentService();
                    deparmentService.Delete(item);
                    setAlert("Xóa khoa thành công", "success");

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
