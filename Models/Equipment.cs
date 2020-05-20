using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PVE.Models
{
    public class Equipment
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("设备编号")]
        [Required]
        [Remote("ValidateCode", "Equipments", AdditionalFields = "ID")]
        public string Code { get; set; }

        [DisplayName("设备名称")]
        public string Name { get; set; }

        [DisplayName("描述")]
        public string Describe => !string.IsNullOrWhiteSpace(Name) ? $"{Code}-{Name}" : Code;

        [DisplayName("备注")]
        public string Remark { get; set; }
    }
}
