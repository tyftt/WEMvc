using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Model;
using BackManagement.Filters;

namespace BackManagement.Controllers
{
    [EmployeeExceptionFilter]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        // GET: Admin
      
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                string json = ApiHelper.GetApiResult("get", "LoginEmp/?Name=" + username + "&PassWord=" + password, "Emp");
                if (json != "")
                {
                    List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(json);
                    Employee emp = employee.FirstOrDefault();
                    Session["Name"] = emp.E_Name;
                    Session["Eid"] = emp.E_Id;

                

                    return Redirect("/important/index");
                }
                else
                {
                    return Redirect("/Admin/Index");
                }
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }
        }

       
    }
}