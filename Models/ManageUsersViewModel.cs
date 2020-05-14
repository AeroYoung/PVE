using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PVE.Models
{
    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }

        public IdentityUser[] Everyone { get; set; }
    }
}