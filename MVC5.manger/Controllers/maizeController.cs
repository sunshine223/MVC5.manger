using MVC5.manger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
    public class maizeController : Controller
    {

        public mangerEntities db = new mangerEntities();
        // GET: maize
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Init(string name, int curPage, int pageSize)
        {


            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<mar> list = db.mars.Where(u => u.Name.Contains(name)).OrderBy(u => u.Id).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.mars.Where(u => u.Name.Contains(name)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Del_User(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            userChecke ids = db.userChecke.Find(id);
            _ = db.userChecke.Remove(ids);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }
    }
}