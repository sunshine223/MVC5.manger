using MVC5.manger.App_Start;
using MVC5.manger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class RegisterController : Controller
    {
        public mangerEntities db = new mangerEntities();
        // GET: Register

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checks(userChecke p)
        {
            bool result = false;
            List<userChecke> list = db.userChecke.Where(u => u.userName == p.userName).ToList();
            if (list.Count > 0)
            {
                result = false;
            }
            else
            {
                db.userChecke.Add(p);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}