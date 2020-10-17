using MVC5.manger.App_Start;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class MainController : Controller
    {
        // GET: main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearSession()
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            Session.Clear();
            dic.Add("res", true);
            return Json(dic);
        }
    }
}