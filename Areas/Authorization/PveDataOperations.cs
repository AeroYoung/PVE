using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PVE.Areas.Authorization
{
    public static class PveDataOperations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
    }

}
