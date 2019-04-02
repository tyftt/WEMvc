using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户预约表
    /// </summary>
    public class Bespeak
    {
        /// <summary>
        /// 预约编号
        /// </summary>
        public int B_Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int U_Id { get; set; }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public int H_Id { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime B_Time { get; set; }
        /// <summary>
        /// 看房时间
        /// </summary>
        public DateTime B_ShowingTime { get; set; }
        public int B_State { get; set; }



        public string U_Name { get; set; }
        public string U_Phone { get; set; }
        public string H_Name { get; set; }
        public string H_Address { get; set; }
        

    }
}
