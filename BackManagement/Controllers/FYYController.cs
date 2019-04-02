using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackManagement.Controllers
{
    public class FYYController : Controller
    {
        // GET: FYY
        public ActionResult Index()
        {
            return View();
        }

 

        #region  楼盘图片

        static string urlPath = string.Empty;
        public FYYController()
        {
            var applicationPath = VirtualPathUtility.ToAbsolute("~") == "/" ? "" : VirtualPathUtility.ToAbsolute("~");
            urlPath = applicationPath + "/Upload";
        }

        public ActionResult ShowImg()
        {
            return View();
        }


        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));



            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = urlPath + "/" + filePathName
            });
        }






        #endregion

        #region   户型信息




        /// <summary>
        /// 添加户型信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddApartMent()
        {
            return View();
        }
        [HttpPost]
        public void AddApartMent(Apartment a)
        {
            int Result = int.Parse(ApiHelper.GetApiResult("post", "Add", "Apartment", a));

            if (Result > 0)
            {

                Response.Write("<script>alert('恭喜,您户型添加成功了！');</script>");
            }
            else
            {
                Response.Write("<script>alert('抱歉,您的户型添加失败啦时!');</script>");
            }

        }


        /// <summary>
        /// 显示户型信息
        /// </summary>
        /// <returns></returns
        public void BindApartMent()
        {
            string json = ApiHelper.GetApiResult("get", "Show", "Apartment");
            List<Apartment> list = JsonConvert.DeserializeObject<List<Apartment>>(json);
            ViewData["type"] = list;
        }


        /// <summary>
        /// 修改户型信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UptApartMent()
        {
            return View();
        }
        [HttpPut]
        public void UptApartMent(Apartment a)
        {
            int Result = int.Parse(ApiHelper.GetApiResult("put", "Upt", "Apartment", a));
            if (Result > 0)
            {

                Response.Write("<script>alert('恭喜,您户型修改成功啦！')</script>");
            }
            else
            {
                Response.Write("<script>alert('抱歉,您的户型修改失败啦!')</script>");
            }
        }
        #endregion

        #region   楼盘信息

        static int size = 5;

        [HttpGet]
        public ActionResult GetHouse(int index = 1, string Name = "")
        {
            BindApartMent();
            BindProvince();
            ViewBag.Index = index;
            string json = ApiHelper.GetApiResult("get", "Show", "House");
            List<HouseInfo> list = JsonConvert.DeserializeObject<List<HouseInfo>>(json);
            List<HouseInfo> newlist = list.Where(n => n.H_Name.Contains(Name)).ToList();
            ViewBag.Page = newlist.Count % size > 0 ? newlist.Count / size + 1 : newlist.Count / size;
            return View(newlist.Skip((index - 1) * size).Take(size).ToList());
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetHouses(string Name = "", int index = 1)
        {
            BindApartMent();
            BindProvince();
            ViewBag.Index = index;
            string json = ApiHelper.GetApiResult("get", "Show", "House");
            List<HouseInfo> list = JsonConvert.DeserializeObject<List<HouseInfo>>(json);
            List<HouseInfo> newlist = list.Where(n => n.H_Name.Contains(Name)).ToList();
            ViewBag.Page = newlist.Count % size > 0 ? newlist.Count / size + 1 : newlist.Count / size;
            newlist.Skip((index - 1) * size).Take(size).ToList();
            return JsonConvert.SerializeObject(newlist);

        }

        /// <summary>
        /// 添加楼盘信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddHouse()
        {
            BindApartMent();
            BindProvince();

            return View();
        }
        [HttpPost]
        public void AddHouse(HouseInfo h, string imgInfo = "")
        {

            string[] str = JsonConvert.DeserializeObject<string[]>(imgInfo);
            MyInfo m = new MyInfo()
            {
                HouseInfo = h,
                imgInfo = str
            };
            int Result = int.Parse(ApiHelper.GetApiResult("post", "Add", "House", m));
            if (Result > 0)
            {

                Response.Write("<script>alert('恭喜,楼盘信息添加成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('抱歉,楼盘信息添加失败啦!');location.href='/FYY/AddHouse';</script>");
            }

        }

        /// <summary>
        /// 修改楼盘信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UptHouse()
        {
            BindApartMent();
            BindProvince();
            return View();
        }


        /// <summary>
        /// 通过ID查看楼盘信息,反填
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetHouseByID(int id)
        {
            ViewBag.EditId = id;
            string json = ApiHelper.GetApiResult("get", "GetHouseById/" + id, "House");
            List<HouseInfo> list = JsonConvert.DeserializeObject<List<HouseInfo>>(json);
            HouseInfo ho = list.FirstOrDefault(m => m.H_Id == id);
            string ns = JsonConvert.SerializeObject(ho);
            return ns;
        }





        public string UptHouseAction(string h)
        {

            HouseInfo hou = JsonConvert.DeserializeObject<HouseInfo>(h);
            string Result = ApiHelper.GetApiResult("put", "Upt", "House", hou);
            return Result;
        }



        public int DelHouse(int id)
        {
            int Result = int.Parse(ApiHelper.GetApiResult("delete", "Del/" + id, "House"));
            return Result;
        }







        #endregion

        #region 三级联动

        /// <summary>
        /// 显示省信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void BindProvince()
        {
            string json = ApiHelper.GetApiResult("get", "Show", "Province");
            List<Province> list = JsonConvert.DeserializeObject<List<Province>>(json);
            ViewBag.prov = list;

        }
        /// <summary>
        /// 显示省信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string FanBindProvince()
        {
            string json = ApiHelper.GetApiResult("get", "Show", "Province");
            List<Province> list = JsonConvert.DeserializeObject<List<Province>>(json);
            //ViewBag.prov = new SelectList(list, "Prov_Id", "Prov_Name");
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 显示市信息
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public string BindCity(int id)
        {

            string json = ApiHelper.GetApiResult("get", "ShowCity?prov_id=" + id, "City");
            JsonConvert.SerializeObject(json);
            return json;
        }

        /// <summary>
        /// 显示区信息
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public string BindArea(int id)
        {
            string json = ApiHelper.GetApiResult("get", "ShowArea?city_id=" + id, "Area");
            JsonConvert.SerializeObject(json);
            return json;
        }

        #endregion

    }
}