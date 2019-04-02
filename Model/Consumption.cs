using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 消费表
    /// </summary>
    public class Consumption
    {
        /// <summary>
        /// 消费编号
        /// </summary>
        public int C_Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int U_Id { get; set; }

        /// <summary>
        /// 房屋编号
        /// </summary>
        public int H_Id { get; set; }

        /// <summary>
        /// 付款状态  0:全付 1:首付
        /// </summary>
        public int C_State { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public double C_Price { get; set; }

        /// <summary>
        /// 分期期数
        /// </summary>
        public int C_Num { get; set; }

        /// <summary>
        /// 缴费时间
        /// </summary>
        public DateTime C_PayTime { get; set; }

        /// <summary>
        /// 用户名称（昵称）
        /// </summary>
        public string U_Name { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string U_Phone { get; set; }
        /// <summary>
        /// 用户职业
        /// </summary>
        public string U_Message { get; set; }

        /// <summary>
        /// 房子型号（几室几厅）
        /// </summary>
        public string H_HomeType { get; set; }
        /// <summary>
        /// 房屋名称
        /// </summary>
        public string H_Name { get; set; }
        /// <summary>
        /// 房屋价格
        /// </summary>
        public Double H_Price { get; set; }
        /// <summary>
        /// 房屋大小
        /// </summary>
        public Double H_Size { get; set; }
        /// <summary>
        /// 房屋户型
        /// </summary>
        public int Apartment_Id { get; set; }


        /// <summary>
        /// 房子详细地址
        /// </summary>
        public string H_Address { get; set; }

        //状态sql字段
        public string Statee { get; set; }

    }
}
