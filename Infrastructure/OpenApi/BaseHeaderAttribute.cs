﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OpenApi
{
    [AttributeUsage(AttributeTargets.Method , AllowMultiple = false)]
    public class BaseHeaderAttribute (string headerName , string description , string defaultValue , bool isRequired): Attribute
    {
        public string HeaderName { get; set; } = headerName;
        public string Description { get; set; } = description;
        public string DefaultValue { get; set; } = defaultValue;
        public bool IsRequired { get; set; } = isRequired;

    }
}
