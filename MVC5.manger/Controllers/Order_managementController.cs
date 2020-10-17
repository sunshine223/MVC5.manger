using MVC5.manger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class Order_managementController : Controller
    {
        // GET: Order_management

        public mangerEntities db = new mangerEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList(string dName,string danhao,string time, int curPage, int pageSize)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<Checking> list = db.Checkings.Where(u => u.name.Contains(dName)&&u.danhao.Contains(danhao) && u.style.Contains(time)).OrderBy(u => u.id).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.Checkings.Where(u => u.name.Contains(dName)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult GetInfo(string dName, string danhao, string time, int curPage, int pageSize)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<Checking> list = db.Checkings.Where(u => u.name.Contains(dName) && u.danhao.Contains(danhao) && u.style.Contains(time)).OrderBy(u => u.id).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.Checkings.Where(u => u.name.Contains(dName)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }
    }
}