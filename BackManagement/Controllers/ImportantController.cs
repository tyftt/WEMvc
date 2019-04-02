using BackManagement.Filters;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackManagement.Controllers
{
    [Filters.YGAuthController]
    [EmployeeExceptionFilter]
    [AllowAnonymous]
    public class ImportantController : Controller
    {
        #region 员工信息管理
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        // GET: Important
        public ActionResult Index()
        {
            string js = ApiHelper.GetApiResult("get", "Show", "Postion");
            List<Position> list = JsonConvert.DeserializeObject<List<Position>>(js);
            ViewBag.Bind = list;
            return View();
        }

        /// <summary>
        /// 默认右侧显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 显示查询员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EmpShow(string Ename)
        {
            try
            {
                Bing();
                ViewBag.BingZW = Bing();
                string json = ApiHelper.GetApiResult("get", "SShow/?Ename=" + Ename, "Emp");
                List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(json);
                return View(list);
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }

        }

        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        [HttpGet]
        public List<Position> Bing()
        {
            try
            {
                string json = ApiHelper.GetApiResult("get", "Show", "Postion");
                List<Position> list = JsonConvert.DeserializeObject<List<Position>>(json);
                return list;
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                throw;
            }
        }
        /// <summary>
        /// 翻填员工信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string FantianShow(int id)
        {
            try
            {
                string josn = ApiHelper.GetApiResult("get", "FantianShow/?eid=" + id, "Emp");
                List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(josn);

                Employee em = list.FirstOrDefault(m => m.E_Id == id);

                string n = JsonConvert.SerializeObject(em);
                return n;
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                throw;
            }

        }
        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int uptEmp(string emp)
        {
            Employee em = JsonConvert.DeserializeObject<Employee>(emp);
            Session["Name"] = em.E_Name;
            int josn = Convert.ToInt32(ApiHelper.GetApiResult("put", "Upt", "Emp", em));
            return josn;
        }
        /// <summary>
        /// 添加员工信息模板
        /// </summary>
        /// <returns></returns>   
        //测试
        public ActionResult AddEmp()
        {

            ViewBag.BingZW = Bing();
            Bing();
            return View();
        }
        /// <summary>
        /// 添加员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int AddEmp(string t)
        {
            try
            {
                Employee employee = JsonConvert.DeserializeObject<Employee>(t);
                int json = int.Parse(ApiHelper.GetApiResult("post", "Add", "Emp", employee));
                if (json > 0)
                {
                    return json;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 员工业绩
        /// </summary>
        /// <returns></returns>
        public List<Sale> GetSales(int pageIndex = 1, int pageSize = 10)
        {
            string json = ApiHelper.GetApiResult("get", "EmpCount", "Emp");
            List<Sale> list = JsonConvert.DeserializeObject<List<Sale>>(json);
            ViewBag.totalCount = list.Count;
            ViewBag.pageIndex = pageIndex;
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }
        /// <summary>
        /// 员工业绩
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSale()
        {
            try
            {
                return View(GetSales());
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }

        }
        [HttpGet]
        public ActionResult GetSaleShow(int pageIndex = 0, int pageSize = 0)
        {
            try
            {
                return PartialView("_Sale", GetSales(pageIndex, pageSize));
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }
        }


        #endregion
        #region 客户信息管理
        /// <summary>
        /// 预约客户信息
        /// </summary>
        /// <returns></returns>
        public List<Bespeak> GetBespeaks(int pageIndex = 1, int pageSize = 10)
        {
            string json = ApiHelper.GetApiResult("get", "BespeakInfo", "Future_k");
            List<Bespeak> list = JsonConvert.DeserializeObject<List<Bespeak>>(json);
            ViewBag.totalCount = list.Count;
            ViewBag.pageIndex = pageIndex;
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }
        /// <summary>
        /// 显示预约客户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Bespeak()
        {
            try
            {
                return View(GetBespeaks());
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }
        }


        /// <summary>
        /// 封装 查询客户信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<Consumption> GetUserInfo(string Name = "", int pageIndex = 1, int pageSize = 10)
        {
            string json = ApiHelper.GetApiResult("get", "seleShow/?name=" + Name, "Future_k");
            List<Consumption> list = JsonConvert.DeserializeObject<List<Consumption>>(json);
            ViewBag.totalCount = list.Count;
            ViewBag.pageIndex = pageIndex;
            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }
        /// <summary>
        /// 修改客户预约信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelBespeak(int id)
        {
            try
            {
                int a = Convert.ToInt32(ApiHelper.GetApiResult("delete", "DelBespeak/?id=" + id, "Future_k"));
                return View("Bespeak", GetBespeaks());
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }

        }
        /// <summary>
        /// 显示客户
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfoShow()
        {
            try
            {
                return View(GetUserInfo());
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }

        }
        [HttpGet]
        public ActionResult UserShow(string Name = "", int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return PartialView("_parUser", GetUserInfo(Name, pageIndex, pageSize));
            }
            catch (Exception)
            {
                Exception e = new Exception("Invalid Controller or/and Action Name");
                HandleErrorInfo eInfo = new HandleErrorInfo(e, "Admin", "Login");
                return View("Error", eInfo);
                throw;
            }

        }


        public int Del(int id)
        {
            int i = Convert.ToInt32(ApiHelper.GetApiResult("del", "Del/" + id, "Future_k"));
            if (i > 0)
            {
                return i;
            }
            else
            {
                return 0;
            }

        }
        #endregion

        /// <summary>
        /// 统计图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult linechart()
        {

            return View();
        }
        /// <summary>
        /// 柱形图统计每月卖出去数量
        /// </summary>
        /// <returns></returns>
        public string GetDataInfo()
        {
            return ApiHelper.GetApiResult("get", "showCount", "Emp");
        }
        /// <summary>
        /// 折线图
        /// </summary>
        /// <returns></returns>
        public ActionResult ZxTu()
        {
            return View();
        }
        /// <summary>
        /// 显示员工每月买房数量
        /// </summary>
        /// <returns></returns>
        public string ZheLine()
        {
            string json = ApiHelper.GetApiResult("get", "EmpCount", "Emp");
            return json;
        }

        /// <summary>
        /// 合同信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Contract(int id)
        {
            ViewBag.Id = id;
            string json = ApiHelper.GetApiResult("get", "SShow", "Emp");
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(json);

            ViewBag.Emp = list;
            return View();
        }

        /// <summary>
        /// 添加合同信息
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public int AddContract(string json)
        {
            Model.Contract contract = JsonConvert.DeserializeObject<Model.Contract>(json);
            contract.C_SignTime = DateTime.Now;
            contract.C_EndTime = contract.C_SignTime.AddYears(70);
            object number = ApiHelper.GetApiResult("get", "GetNumberOrder", "Emp");
            string num = DateTime.Now.ToString("yyyyMMdd");
            string data = "1";
            string dataNum = number.ToString().Substring(1, 8);
            if (number != null && !string.IsNullOrEmpty(number.ToString()))
            {
                long n = long.Parse(number.ToString().Substring(9, 5));
                if (dataNum != num)
                {
                    n = 00001;
                }
                n += 1;
                contract.C_Number = num + n.ToString().PadLeft(5, '0');
            }
            else
            {
                contract.C_Number = num + data.PadLeft(5, '0');
            }
            return int.Parse(ApiHelper.GetApiResult("post", "AddContract", "Emp", contract));
        }

        /// <summary>
        /// 根据身份证号获取身份信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetCardId(string number)
        {
            Model.CardId card = new Model.CardId()
            {
                Address = CardId.getAddress(number),
                Sex = CardId.getSex(number),
                Birthday = CardId.getBirthday(number)
            };
            return JsonConvert.SerializeObject(card);
        }

        /// <summary>
        /// 显示合同信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractShow()
        {
            return View();
        }

        /// <summary>
        /// 显示合同信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractinfoShow()
        {
            return View();
        }

        public string GetContractInfo()
        {
            string json = ApiHelper.GetApiResult("get", "Show", "Emp");
            List<Contract> list = JsonConvert.DeserializeObject<List<Contract>>(json);
            string data = "{\"code\":0,\"msg\":\"\",\"count\":" + list.Count + ",\"data\":";
            data = data + json + "}";
            return data;
        }
    }
}