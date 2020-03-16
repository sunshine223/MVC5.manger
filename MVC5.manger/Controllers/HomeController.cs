using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5.manger.Models;
using System.Web.Mvc;
using MVC5.manger.App_Start;
using System.Net;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class HomeController : Controller
    {
   
        public ActionResult Index()
        {
            return View();
        }

       [OutputCache(Duration =100,VaryByParam ="*")]//页面缓存
        public ActionResult Check(string name, string pwd)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        using (mangerEntities db = new mangerEntities())
        {
            List<userChecke> list = db.userChecke.Where(u => u.userLog == name && u.userPwd == pwd).ToList();
            dic.Add("res", list.Count > 0);
            Session["name"] = name;
        }
      
        return Json(dic, JsonRequestBehavior.AllowGet);
    }

     
}
}