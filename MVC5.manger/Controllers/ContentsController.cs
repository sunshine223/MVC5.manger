using System.Web.Mvc;
using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{ 
    [ActionFilter(CheckLogin = true)]
    public class ContentsController : Controller
    {
        // GET: Loging

        public ActionResult Index()
        {
            return View();
        }

    }
}