﻿using Application.Exceptions;
using Application.Features.Identity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private ClaimsPrincipal _principal;
        public string Name => _principal.Identity.Name;

        public IEnumerable<Claim> GetUserClaims()
        {
            return _principal.Claims;
        }

        public string GetUSerEmail()
        {
            if (IsAuthenticated())
            {
                return _principal.GetEmail();
            }
            return string.Empty;
        }

        public string GetUserId()
        {
            if (IsAuthenticated())
            {
                return _principal.GetUserId();
            }
            return string.Empty;
        }

        public string GetUserTenant()
            => IsAuthenticated() ? _principal.GetTenant() : string.Empty;

        public bool IsAuthenticated()
        => _principal.Identity.IsAuthenticated;

        public bool IsInRole(string roleName)
        {
            return _principal.IsInRole(roleName);
        }

        public void SetCurrentUser(ClaimsPrincipal principal)
        {
            if(_principal is not null)
            {
                throw new ConflictException("Invalid Operation on claim .");
            }
            _principal = principal;

        }
    }
}
