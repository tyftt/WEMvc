using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackManagement.Filters
{
    /// <summary>
    /// 授权过滤器
    /// </summary>
    public class YGAuthController : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["Name"]==null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
            }
        }
    }
}