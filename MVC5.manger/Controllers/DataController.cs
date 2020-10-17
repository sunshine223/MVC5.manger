
using MVC5.manger.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class DataController : Controller
    {
        // GET: data
        public mangerEntities db = new mangerEntities();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Check(string eName)
        {
            var info = (from a in db.userChecke
                        join d in db.userinfo on a.userID equals d.userID
                        where a.userLog.Contains(eName)
                        select new
                        {
                            a.userID,
                            a.userName,
                            d.userSex,
                            a.userLog,
                            d.userPhone,
                            d.userNum,
                            d.usermingzu,
                            d.userAderrs,
                            a.userType,
                            d.userPhot
                        }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        //上传用户头像
        public ActionResult UpdetImg(HttpPostedFileBase file, int userID)
        {

            //获取文件名
            string fileName = Path.GetFileName(file.FileName);
            //创建一个路径
            string path = Server.MapPath("~/images/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            file.SaveAs(path + fileName);
            string url = "/style/img/" + fileName;
            userinfo ids = db.userinfo.Find(userID);
            ids.userPhot = url;
            db.SaveChanges();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("res", true);
            return Json(dic);
        }
        public ActionResult Person()
        {
            return View();
        }
        public ActionResult Tables()
        {
            return View();
        }

        public ActionResult Table_check(string dName, int curPage, int pageSize)
        {


            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<staffInfo> list = db.staffInfo.Where(u => u.empName.Contains(dName)).OrderBy(u => u.empId).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.staffInfo.Where(u => u.empName.Contains(dName)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        //删除
        public ActionResult Del(int empId)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            staffInfo id = db.staffInfo.Find(empId);
            staffInfo ids = db.staffInfo.Remove(id);
            db.SaveChanges();
            dic.Add("res", ids != null);
            return Json(dic);
        }



        //根据id查询数据
        public ActionResult SelectDEL(int id)
        {

            var tbs = from a in db.staffInfo
                      where a.empId == id
                      select new
                      {
                          a.empId,
                          a.empName,
                          a.empPosition,
                          a.empHrId,
                          a.empInDate,
                          a.empSalary,
                          a.empBonus,
                          a.deptId,
                      };

            var td = db.staffInfo.Where(u => u.empId.Equals(id)).ToList();
            return Json(td, JsonRequestBehavior.AllowGet);
        }
        //保存修改
        public ActionResult Updet(staffInfo p)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            var info = db.staffInfo.Find(p.empId);
            info.empPosition = p.empPosition;
            info.empHrId = p.empHrId;
            info.empInDate = p.empInDate;
            info.empSalary = p.empSalary;
            info.deptId = p.deptId;
            int num = db.SaveChanges();
            dic.Add("res", num <= 0);
            return Json(dic);
        }
        public ActionResult NoFind()
        {
            return View();
        }
        public ActionResult Magazine()
        {
            return View();
        }

        //修改个人信息
        public ActionResult UpdetProsens()
        {

            return View();
        }
        public ActionResult UpdetProsen(string names)
        {
            var info = (from a in db.userChecke
                        join d in db.userinfo on a.userID equals d.userID
                        where a.userLog.Contains(names)
                        select new
                        {
                            a.userID,
                            userName = a.userName,
                            userSex = d.userSex,
                            userLog = a.userLog,
                            userPhone = d.userPhone,
                            d.userNum,
                            usermingzu = d.usermingzu,
                            userAderrs = d.userAderrs,
                            userType = a.userType,
                        }).ToList();
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdetSave(userinfo b)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            var info = db.userinfo.Find(b.userID);
            info.userSex = b.userSex;
            info.userPhone = b.userPhone;
            info.userAderrs = b.userAderrs;
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? false : true);
            return Json(dic);

        }

        //修改密码
        public ActionResult ChangePwd()
        {
            return View();
        }
        public ActionResult SelectPwds(string names)
        {
            userChecke name = db.userChecke.Where(u => u.userLog.Equals(names)).ToList()[0];
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePwds(userChecke b)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            var info = db.userChecke.Find(b.userID);
            info.userPwd = b.userPwd;
            int num = db.SaveChanges();
            dic.Add("res", num <= 0);
            return Json(dic);
        }
    }
}