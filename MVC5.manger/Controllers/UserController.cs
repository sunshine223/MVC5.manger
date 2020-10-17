using MVC5.manger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class UserController : Controller
    {
        public mangerEntities db = new mangerEntities();
        // GET: User
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
            List<userChecke> list = db.userChecke.Where(u => u.userName.Contains(name)).OrderBy(u => u.userID).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.userChecke.Where(u => u.userName.Contains(name)).Count();
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