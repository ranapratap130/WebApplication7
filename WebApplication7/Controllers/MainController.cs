using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Dbopeartions;

namespace WebApplication7.Controllers
{
    public class MainController : Controller
    {
        // GET: Main

        EmployeeEntities db = new EmployeeEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetData()
        {
            List<EmpDetails2> empList = db.EmpDetails2.ToList<EmpDetails2>();
            return Json(new { data = empList },
            JsonRequestBehavior.AllowGet);
        }
       [HttpGet]
        public ActionResult StoreOrEdit(int? id)
        {
            if (id == 0)
                return View(new EmpDetails2());
            else
            {

                    return View(db.EmpDetails2.Where(x => x.EmpId == id).FirstOrDefault<EmpDetails2>());
  
            }
        }
      [HttpPost]
        public ActionResult StoreOrEdit( EmpDetails2 empdata)
        {

            EmpDetails2 emp = new EmpDetails2();

            emp.EmpId = empdata.EmpId;
            emp.Name = empdata.Name;
            emp.Email = empdata.Email;
            emp.Designation = empdata.Designation;
            db.EmpDetails2.Add(emp);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult StoreOrEdit1(int? id)
        {
            if (id == 0)
                return View(new EmpDetails2());
            else
            {

                return View(db.EmpDetails2.Where(x => x.EmpId == id).FirstOrDefault<EmpDetails2>());

            }
        }
        [HttpPost]
        public ActionResult StoreOrEdit1(EmpDetails2 empdata)
        {
            
            var emp = db.EmpDetails2.Where(x => x.EmpId == empdata.EmpId).FirstOrDefault();
            emp.Name = empdata.Name;
            emp.Email = empdata.Email;
            emp.Designation = empdata.Designation;
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            EmpDetails2 emp = db.EmpDetails2.Where(x => x.EmpId == id).FirstOrDefault<EmpDetails2>();
            db.EmpDetails2.Remove(emp);
            db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
        }



    }
}