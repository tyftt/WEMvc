using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;
namespace BackManagement
{
    public static class ApiHelper
    {
        /// <summary>
        /// 封装调用api 
        /// </summary>
        /// <param name="verbs"></param>
        /// <param name="actionName">动作方法</param>
        /// <param name="apiName">api名称</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetApiResult ( string verbs, string actionName,string apiName, Object obj = null )
        {
            HttpClient client = new HttpClient ( );
            client.BaseAddress = new Uri ("http://localhost:23634/api/" + apiName+"/" );
            Task<HttpResponseMessage> task = null;
            string json = "";
            switch (verbs)
            {
                case "get":
                    task = client.GetAsync ( actionName );
                    break;
                case "post":
                    task = client.PostAsJsonAsync ( actionName, obj );
                    break;
                case "put":
                    task = client.PutAsJsonAsync ( actionName, obj );
                    break;
                case "delete":
                    task = client.DeleteAsync ( actionName );
                    break;
                default:
                    break;
            }
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync ( );
                read.Wait ( );
                json = read.Result;
            }
            return json;
        }

    }
}
