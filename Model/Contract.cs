using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 合同表
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int C_Id { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string C_Number { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public int E_Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_Name { get; set; }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public int H_Id { get; set; }
        /// <summary>
        /// 签订日期
        /// </summary>
        public DateTime C_SignTime { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime C_EndTime { get; set; }

        /// <summary>
        /// 电子图片
        /// </summary>
        public string C_Img { get; set; }

        /// <summary>
        /// 法定人
        /// </summary>
        public string C_Legal { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string C_Phone { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string C_CardId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string C_Address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string C_Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string C_Birthday { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string C_Bank { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string C_BankNumber { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string E_Name { get; set; }
        /// <summary>
        /// 房子姓名
        /// </summary>
        public string H_Name { get; set; }

        /// <summary>
        /// 房子详细地址
        /// </summary>
        public string H_Address { get; set; }
    }
}
