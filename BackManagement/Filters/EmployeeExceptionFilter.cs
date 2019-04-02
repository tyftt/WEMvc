using BackManagement.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackManagement.Filters
{
    //异常过滤器
    public class EmployeeExceptionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            FileLogger logger = new FileLogger();

            logger.LogException(filterContext.Exception);
            base.OnException(filterContext);

            filterContext.ExceptionHandled = true;
        }
    }
}