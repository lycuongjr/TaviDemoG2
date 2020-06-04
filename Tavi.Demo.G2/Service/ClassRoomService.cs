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
   public class ClassRoomService
    {
        TaviDemoG2Db db = null;
        public ClassRoomService()
        {
            db = new TaviDemoG2Db();
        }
        public ClassRoom FindByKey(int? ClassRoomID)
        {
            ClassRoom classRoom = new ClassRoom();
            if (ClassRoomID.HasValue)
            {
                classRoom = db.ClassRooms.Find(ClassRoomID);
            }
            return classRoom;
        }
        public void Insert(ClassRoom classRoom)
        {
            classRoom.IsDelete = false;
            db.ClassRooms.Add(classRoom);
            db.SaveChanges();
        }
        public void Update(ClassRoom classRoom)
        {
            db.ClassRooms.AddOrUpdate(classRoom);
            db.SaveChanges();
        }
        public void Delete(int? ClassRoomID)
        {
            ClassRoom classRoom = new ClassRoom();
            if (ClassRoomID.HasValue)
            {
                classRoom = db.ClassRooms.Find(ClassRoomID);
                db.ClassRooms.Remove(classRoom);
            }
            db.SaveChanges();
        }
        public IPagedList<ClassRoom> GetClassRoom(string ClassName, int CurrentPage, int PageSize)
        {
            var list = db.ClassRooms.Where(x => x.Status == true && x.IsDelete == false).AsQueryable();
            if (!string.IsNullOrEmpty(ClassName))
            {
                list = list.Where(x => x.ClassName.Contains(ClassName)).AsQueryable();
            }
            return list.OrderByDescending(x => x.ClassRoomID).ToPagedList(CurrentPage, PageSize);
        }
        public IEnumerable<ClassRoom> GetAllClassRoom(int? DepartmentID)
        {
            var list = db.ClassRooms.Where(x => x.IsDelete == false).AsQueryable();
            if (DepartmentID.HasValue)
            {
                list = list.Where(m => m.DepartmentID == DepartmentID).AsQueryable();
            }
            return list.OrderBy(x => x.SortOrder);
        }
    }
}
