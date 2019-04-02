using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackManagement.Logger
{
    public class FileLogger
    {
        public void LogException(Exception e)
        {
            File.WriteAllLines("C://Error//" + DateTime.Now.ToString("yyyy-MM-dd mm hh ss") + ".txt",
                new string[]
                  {
                      "Message:"+e.Message,
                      "Stacktrace:"+e.StackTrace
                  });
        }
        public void LogExceptionContext(ExceptionContext filterContext)
        {
            File.WriteAllLines("C://Error//" + DateTime.Now.ToString("yyyy-MM-dd mm hh ss") + ".txt",
                new string[]
                  {
                      "时间："+DateTime.Now.ToString("yyyy-MM-dd mm hh ss"),
                      "ip地址："+filterContext.HttpContext.Request.UserHostAddress,
                      "控制器："+filterContext.RouteData.Values["Controller"],
                      "动作方法："+filterContext.RouteData.Values["Action"],
                      "错误信息："+filterContext.Exception.Message+">>>"+filterContext.Exception.StackTrace
                  });
        }
    }
}