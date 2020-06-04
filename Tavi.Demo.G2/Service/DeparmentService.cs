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
    public class DeparmentService
    {
       
        TaviDemoG2Db db = null;
        public DeparmentService()
        {
            db = new TaviDemoG2Db();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        /// 
       
        public Department FindByKey(int? DeparmentID)
        {
            Department department = new Department();
            if (DeparmentID.HasValue)
            {
                department = db.Departments.Find(DeparmentID);
            }
            return department;
        }
        public void Insert(Department department)
        {
            department.IsDelete = false;
            db.Departments.Add(department);
            db.SaveChanges();
        }
        public void Update(Department department)
        {
            db.Departments.AddOrUpdate(department);
            db.SaveChanges();
        }
        public void Delete(int? DeparmentID)
        {
            Department deparment = new Department();
            if (DeparmentID.HasValue)
            {
                deparment = db.Departments.Find(DeparmentID);
                db.Departments.Remove(deparment);
            }
            db.SaveChanges();
        }
        public IPagedList<Department> GetDepartments(string DepartmentName, int CurrentPage, int PageSize)
        {
            var list = db.Departments.Where(x => x.Status == true && x.IsDelete == false).AsQueryable();
            if (!string.IsNullOrEmpty(DepartmentName))
            {
                list = list.Where(x => x.DepartmentName.Contains(DepartmentName)).AsQueryable();
            }
            return list.OrderByDescending(x => x.DepartmentID).ToPagedList(CurrentPage, PageSize);
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            var list = db.Departments.Where(x => x.IsDelete == false).AsEnumerable();
            return list.OrderByDescending(x => x.DepartmentID);
        }
    }
}
