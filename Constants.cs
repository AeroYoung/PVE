using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVE
{
    public class Constants
    {
        public const string AdministratorRole = "Administrator";
        public const string AdminUserEmail = "admin@admin.com";
        public const string AdminUserName = "admin@admin.com";

        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string ViewDataPveDataId = "PveDataId";
        public static readonly string ViewDataVIN = "VIN";
        public static readonly string ViewDataSignalId = "SignalId";
        public static readonly string ViewDataEquipmentId = "Equipment_Id";

        public static readonly string ViewDataReturnUrl = "returnUrl";
    }
}
