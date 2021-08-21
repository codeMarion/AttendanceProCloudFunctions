using AttendanceProCloudFunctions.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

[assembly: FunctionsStartup(typeof(AttendanceProCloudFunctions.Startup))]
namespace AttendanceProCloudFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("SqlConnectionString")));
            builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("Auth0Domain");
                    options.Audience = Environment.GetEnvironmentVariable("Auth0Audience");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
        }
    }
}
