﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Identity.Constants.PermissionConstants;

namespace Infrastructure.Identity.Auth
{
    public class ShouldHavePermissionAttribute : AuthorizeAttribute
    {
        public ShouldHavePermissionAttribute(string action , string feature)
        {
            Policy = SchoolPermission.NameFor(action, feature);
        }
    }
}
