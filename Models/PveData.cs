using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PVE.Models
{
    public class PveData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("序号")]
        [Required(ErrorMessage = "序号是必须字段")]
        public string SerialNum { get; set; }

        [DisplayName("单位")]
        public string Producer { get; set; }

        #region 车型信息

        [Category("车型信息")]
        [DisplayName("车型")]
        public string VehicleType { get; set; }

        [Category("车型信息")]
        [DisplayName("OBD")]
        public string OBD { get; set; }

        [Category("车型信息")]
        [DisplayName("BOB")]
        public string BOB { get; set; }

        [Category("车型信息")]
        [DataType(DataType.Date)]
        [DisplayName("送样时间")]
        public DateTime? ReleaseDate { get; set; }

        [Category("车型信息")]
        [DisplayName("样车数量")]
        public int? VehicleNum { get; set; }

        [Category("车型信息")]
        [DisplayName("VIN")]
        [Required]
        public string VIN { get; set; }

        #endregion

        [DisplayName("测试内容")]
        public string TestContent { get; set; }

        #region 测试进度

        [Category("测试进度")]
        [DisplayName("J1")]
        public string ProgressJ1 { get; set; }

        [Category("测试进度")]
        [DisplayName("J2动")]
        public string ProgressJ2D { get; set; }

        [Category("测试进度")]
        [DisplayName("J2总")]
        public string ProgressJ2Z { get; set; }

        [Category("测试进度")]
        [DisplayName("J2完")]
        public string ProgressJ2W { get; set; }

        [Category("测试进度")]
        [DisplayName("J2豁")]
        public string ProgressJ2H { get; set; }

        [Category("测试进度")]
        [DisplayName("J2剩")]
        public string ProgressJ2S { get; set; }

        [Category("测试进度")]
        [DisplayName("J3")]
        public string ProgressJ3 { get; set; }

        #endregion

        #region 商务信息

        [Category("商务信息")]
        [DisplayName("客户")]
        public string ContactCustomer { get; set; }

        [Category("商务信息")]
        [DisplayName("市场")]
        public string ContactMarket { get; set; }

        [Category("商务信息")]
        [DisplayName("CATAC")]
        public string ContactCATAC { get; set; }

        [Category("商务信息")]
        [DisplayName("项目周期")]
        public string Period { get; set; }

        [Category("商务信息")]
        [DisplayName("合同方式")]
        public string ContractType { get; set; }

        [Category("商务信息")]
        [DisplayName("补充协议")]
        public string Agreement { get; set; }

        [Category("商务信息")]
        [DisplayName("项目报价")]
        [Range(0,double.MaxValue, ErrorMessage="数值范围0~Max")]
        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        public double? ProjectBid { get; set; }

        #endregion

        #region 费用结算

        [Category("费用结算")]
        [DisplayName("J1")]
        [Range(0, double.MaxValue, ErrorMessage = "数值范围0~Max")]
        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        public double? FeeJ1 { get; set; }

        [Category("费用结算")]
        [DisplayName("J2")]
        [Range(0, double.MaxValue, ErrorMessage = "数值范围0~Max")]
        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        public double? FeeJ2 { get; set; }

        [Category("费用结算")]
        [DisplayName("J2")]
        [Range(0, double.MaxValue, ErrorMessage = "数值范围0~Max")]
        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        public double? FeeJ3 { get; set; }

        [Category("费用结算")]
        [DisplayName("任务单")]
        public string TaskForm { get; set; }

        [Category("费用结算")]
        [DataType(DataType.Date)]
        [DisplayName("提交报告时间")]
        public DateTime? ReportDate { get; set; }

        [Category("费用结算")]
        [DataType(DataType.Date)]
        [DisplayName("还车时间")]
        public DateTime? ReturnDate { get; set; }

        [Category("费用结算")]
        [DisplayName("费用结算情况")]
        public string FeeStatus { get; set; }

        #endregion

        #region 项目状态

        [Category("项目状态")]
        [DisplayName("项目状态")]
        public string ProjectStatus { get; set; }

        [Category("项目状态")]
        [DisplayName("备注")]
        public string Remark { get; set; }

        #endregion

        public List<Signal> Signals { get; set; }
    }
}
