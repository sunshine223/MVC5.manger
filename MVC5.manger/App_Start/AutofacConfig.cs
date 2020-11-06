using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC5.manger.App_Start
{
    public class AutofacConfig
    {
        public  static void Register()
        {
            var builder = new ContainerBuilder();
            //注册控制器
            //Assembly controllers = Assembly.Load("MVC5.manger");
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //反射Service层
            Assembly serviceAss = Assembly.Load("Service");
            builder.RegisterTypes(serviceAss.GetTypes()).AsImplementedInterfaces();
            //创建一个Autofac的容器
            var container = builder.Build();
            //将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}