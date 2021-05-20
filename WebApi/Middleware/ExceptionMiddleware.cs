using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json"; //Envíe el error en formato json
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var reponse = _env.IsDevelopment() ? new CodeErrorException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace.ToString())
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; //Para que las propiedades se mantengan en minúscula

                var json = JsonSerializer.Serialize(reponse, options);
                await context.Response.WriteAsync(json);
            }
        } 

    }
}
