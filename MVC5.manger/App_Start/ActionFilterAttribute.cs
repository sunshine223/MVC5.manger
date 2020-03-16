using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.manger.App_Start
{
    public class ActionFilter : ActionFilterAttribute
    {
        public bool CheckLogin { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var target = filterContext.HttpContext.Session["name"];
           
            if (CheckLogin == false && target == null)
            {

                filterContext.Result = new RedirectResult("/Home/Index");

            }
            return;
        }
    }
}