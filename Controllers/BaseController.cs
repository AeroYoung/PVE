﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PVE.Data;
using PVE.Models;

namespace PVE.Controllers
{
    public class BaseController : Controller
    {
        protected readonly PVEContext _context;

        protected bool GetPveData(int? foreignKey, out PveData pveData)
        {
            pveData = null;
            if (foreignKey == null)
                return false;

            pveData = _context.PveData.SingleOrDefault(p => p.ID.Equals(foreignKey.Value));

            if (pveData == null)
                return false;

            ViewData[Constants.ViewDataPveDataId] = foreignKey;
            ViewData[Constants.ViewDataVIN] = pveData.VIN;
            return true;
        }

        protected bool GetEquipment(int? foreignKey, out Equipment euiEquipment)
        {
            euiEquipment = null;
            if (foreignKey == null)
                return false;

            euiEquipment = _context.Equipment.SingleOrDefault(p => p.ID.Equals(foreignKey.Value));

            if (euiEquipment == null)
                return false;

            ViewData[Constants.ViewDataEquipmentId] = foreignKey;
            return true;
        }

        public BaseController(PVEContext context)
        {
            _context = context;
        }

    }
}
