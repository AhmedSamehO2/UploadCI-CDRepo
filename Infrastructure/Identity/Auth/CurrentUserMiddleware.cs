﻿using Application.Features.Identity.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Auth
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUserService _currentUserService;

        public CurrentUserMiddleware(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUserService.SetCurrentUser(context.User);
            await next(context);
        }
    }
}
