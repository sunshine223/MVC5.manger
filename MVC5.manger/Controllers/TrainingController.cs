using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5.manger.Models;
using System.Web.Mvc;

namespace MVC5.manger.Controllers
{
   
    public class TrainingController : Controller
    {


        public mangerEntities db = new mangerEntities();
        // GET: Training
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
            List<Training> list = db.Training.Where(u => u.people.Contains(name)).OrderBy(u => u.id).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.Training.Where(u => u.people.Contains(name)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult Del_Training(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Training ids= db.Training.Find(id);
            Training idss = db.Training.Remove(ids);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        //添加培训信息
        public ActionResult Add_trainings()
        {
            return View();
        }

        public ActionResult Add_Training(Training p) {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            db.Training.Add(p);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }
    }
}