using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PVE.Models
{
    public class J2TestData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("总表主键")]
        public int PveDataID { get; set; }

        [DisplayName("车型")]
        public PveData PveData { get; set; }

        [DisplayName("系族名称")]
        [Required]
        public string GroupName { get; set; }

        [DisplayName("测试组")]
        public string TestGroup { get; set; }

        [DisplayName("实际试验日期")]
        [DataType(DataType.Date)]
        public DateTime ActualTestDate { get; set; }

        [DisplayName("软件版本")]
        public string SoftVersion { get; set; }

        [DisplayName("SAB版本")]
        public string SABVersion { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }

    }
}
