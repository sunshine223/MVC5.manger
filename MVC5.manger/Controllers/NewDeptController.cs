using MVC5.manger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
    public class NewDeptController : Controller
    {

        public mangerEntities db = new mangerEntities();

        // GET: NewDept
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add_detpart(staffInfo p)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            db.staffInfo.Add(p);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic);
        }
    }
}