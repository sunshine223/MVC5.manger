using IService;
using MVC5.manger.App_Start;
using MVC5.manger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class HomeController : Controller
    {
        private readonly IServiceA _service;

        public HomeController(IServiceA service)
        {
            this._service = service;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Check(string name, string pwd)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var C = _service.C();
            using (mangerEntities db = new mangerEntities())
            {
                List<userChecke> list = db.userChecke.Where(u => u.userLog == name && u.userPwd == pwd).ToList();
                dic.Add("res", list.Count > 0);
                dic.Add("res1", C);
                Session["name"] = name;
            }

            return Json(dic, JsonRequestBehavior.AllowGet);
        }


    }
}