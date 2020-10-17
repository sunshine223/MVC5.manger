using HRMS.utils;
using MVC5.manger.Models;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC5.manger.App_Start;

namespace MVC5.manger.Controllers
{
    [ActionFilter(CheckLogin = true)]
    public class EmpsalaryController : Controller
    {

        public mangerEntities db = new mangerEntities();
        // GET: Empsalary
        public ActionResult Empsalary()
        {
            return View();
        }

        public ActionResult CheckSalary(string name, int curPage, int pageSize,int id)
        {


            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<staffInfo> list = db.staffInfo.Where(u => u.empName.Contains(name)).OrderBy(u => u.empId).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.staffInfo.Where(u => u.empName.Contains(name)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectSalary(int id)
        {
            List<staffInfo> list = db.staffInfo.Where(u => u.empId.Equals(id)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdetSalary(staffInfo p)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            var info = db.staffInfo.Find(p.empId);
            info.empBonus = p.empBonus;
            info.empSalary = p.empSalary;
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? false : true);
            return Json(dic);
        }

        public ActionResult Department()
        {
            return View();
        }

        public ActionResult Init(string dName, int curPage, int pageSize,int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //计算起始下标
            int startIndex = (curPage - 1) * pageSize;
            //查询数据
            List<department> list = db.department.Where(u => u.departmentName.Contains(dName) || u.ID.Equals(id)).OrderBy(u => u.ID).Skip(startIndex).Take(pageSize).ToList();
            dic.Add("data", list);
            //查询总条数
            int rows = db.department.Where(u => u.departmentName.Contains(dName)).Count();
            dic.Add("rows", rows);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_Department(department p)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            db.department.Add(p);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Depart_select(int id)
        {

            List<department> list = db.department.Where(u => u.ID == id).ToList();
            return Json(list);
        }
        public ActionResult Update_Department(department p)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var info = db.department.Find(p.ID);
            info.departmentName = p.departmentName;
            info.num = p.num;
            info.states = p.states;
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dele_Department(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            department ids = db.department.Find(id);
            db.department.Remove(ids);
            int num = db.SaveChanges();
            dic.Add("res", num > 0 ? true : false);
            return Json(dic, JsonRequestBehavior.AllowGet);

        }
        //下载文件
        public FileContentResult DownloadExcel()
        {
            //获取数据
            List<department> list = db.department.ToList();
            //根据数据获取excel对象
            string[][] heads = new string[5][]{
                    new string[]{ "ID","ID"},
                    new string[]{ "部门名称","departmentName"},
                    new string[]{ "创建时间","CreateTime"},
                    new string[]{ "部门人数","num"},
                    new string[]{ "备注","states"}, };
            XSSFWorkbook wb = ExportExcelUtil<department>.export(list, heads);
            //excel对象写入流
            MemoryStream ms = new MemoryStream();
            wb.Write(ms);
            byte[] bs = ms.ToArray();
            return File(bs, "application/vnd.ms-excel", System.DateTime.Now.Ticks + ".xls");
        }

 

        //上传
        public void UploadDept(HttpPostedFileBase file)
        {
            string flirname = Path.GetFileName(file.FileName);
            string path = Server.MapPath("~/flies/") + flirname;
            file.SaveAs(path);
            DataTable dt = NPOIHelper.ExcelToDataTable(path, "sheet0", true);
            List<department> list = new List<department>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                department dept = new department
                {
                    departmentName = dt.Rows[i][0].ToString(),
                    CreateTime = Convert.ToDateTime(dt.Rows[i][1].ToString()),
                    num = int.Parse(dt.Rows[i][2].ToString()),
                    states = dt.Rows[i][3].ToString()
                };
                list.Add(dept);
            }
            for (int i = 0; i < list.Count; i++)
            {
                db.department.Add(list[i]);
            }
            db.SaveChanges();


        }

    }
}