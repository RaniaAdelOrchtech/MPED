using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPMAR.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Common
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly MPMAR.Business.IMyEmailSender _emailSender;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IConfiguration configuration, MPMAR.Business.IMyEmailSender emailSender)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                var adminEmail = _configuration["InitialAdminEmail"];
                await _emailSender.SendEmailAsync(adminEmail, "Admin", "MPED Exception", e.Message);
                throw;
            }
        }
    }
}
