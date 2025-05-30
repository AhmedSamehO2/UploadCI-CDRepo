﻿using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OpenApi
{
    public class TenantHeaderAttributeProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if(context.MethodInfo.GetCustomAttribute(typeof(BaseHeaderAttribute)) is BaseHeaderAttribute swaggerHeader)
            {
                var parameters = context.OperationDescription.Operation.Parameters;

                var existingParam = parameters.FirstOrDefault(p=>p.Kind == NSwag.OpenApiParameterKind.Header && p.Name == swaggerHeader.HeaderName);
                if(existingParam is not null)
                {
                    parameters.Remove(existingParam);
                }
                parameters.Add(new NSwag.OpenApiParameter
                {
                    Name = swaggerHeader.HeaderName,
                    Kind = NSwag.OpenApiParameterKind.Header,
                    Description = swaggerHeader.Description,
                    IsRequired = swaggerHeader.IsRequired,
                    Schema = new NJsonSchema.JsonSchema
                    {
                        Type = NJsonSchema.JsonObjectType.String,
                        Default = swaggerHeader.DefaultValue,
                    }
                });
            }
            return true;
        }
    }
}
