using MVC5.manger.App_Start;
using System.Web;
using System.Web.Mvc;

namespace MVC5.manger
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionFilter());
        }
    }
   
}
