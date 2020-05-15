using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PVE.Models
{
    public class Signal
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("总表关联主键")]
        public int PveDataID { get; set; }

        [DisplayName("车型")]
        public PveData PveData { get; set; }
        
        [DisplayName("Pin编号")]
        [Required]
        public string PinNo { get; set; }

        [DisplayName("名称")]
        public string PinName { get; set; }

        [DisplayName("功能1")]
        public string Func1 { get; set; }

        [DisplayName("功能2")]
        public string Func2 { get; set; }

        [DisplayName("OBD是否检测")]
        public bool OBD { get; set; }
    }
}
