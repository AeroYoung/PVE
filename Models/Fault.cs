using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PVE.Models
{
    public class Fault
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("总表主键")]
        public int PveDataID { get; set; }

        [DisplayName("车型")]
        public PveData PveData { get; set; }

        [DisplayName("设备主键")]
        public int EquipmentId { get; set; }

        [DisplayName("设备")]
        public Equipment Equipment { get; set; }

        [DisplayName("故障编号")]
        public string Code { get; set; }

        [DisplayName("故障描述")]
        public string Describe { get; set; }

        [DisplayName("开始时间")]
        public DateTime BeginTime { get; set; }

        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        [DisplayName("时长")]
        public TimeSpan TimeSpan => EndTime - BeginTime;
    }
}
