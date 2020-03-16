using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{
    public class ContentsController : Controller
    {
        // GET: Loging
      
        public ActionResult Index()
        {
            return View();
        }
    }
}