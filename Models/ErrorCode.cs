using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PVE.Models
{
    public class ErrorCode
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("总表主键")]
        public int PveDataID { get; set; }

        [DisplayName("车型")]
        public PveData PveData { get; set; }
        
        [DisplayName("名称")]
        [Required]
        public string Name { get; set; }
        
        [DisplayName("编码")]
        [Required]
        public string Code { get; set; }
    }
}
