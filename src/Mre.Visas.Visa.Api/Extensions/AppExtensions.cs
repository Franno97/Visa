﻿using Microsoft.AspNetCore.Builder;
using Mre.Visas.Visa.Api.Middlewares;

namespace Mre.Visas.Visa.Api.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app, string name)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", name);
                options.RoutePrefix = string.Empty;
            });
        }

        public static void UseApiExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}