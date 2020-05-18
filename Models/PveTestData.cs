using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PVE.Models
{
    public class PveTestData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("总表主键")]
        public int PveDataID { get; set; }

        [DisplayName("车型")]
        public PveData PveData { get; set; }

        [DisplayName("发起日期")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime BeginDate { get; set; }

        [DisplayName("发起人")]
        public string BeginMan { get; set; }

        [DisplayName("发起单位")]
        public string BeginCompany { get; set; }

        [DisplayName("类别")]
        public string Type { get; set; }

        [DisplayName("问题")]
        public string Problem { get; set; }

        [DisplayName("优先级")]
        public string Priors { get; set; }

        [DisplayName("责任人")]
        public string ChargeMan { get; set; }

        [DisplayName("责任单位")]
        public string ChargeCompany { get; set; }

        [DisplayName("截止日期")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DisplayName("状态")]
        public string State { get; set; }

        [DisplayName("进度")]
        public string FeedPercent { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }

        [DisplayName("领克反馈19011")]
        public string L19011 { get; set; }
    }
}
