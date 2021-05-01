using AttendanceProAPI.Models;
using AttendanceProCloudFunctions.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceProCloudFunctions
{
    public class UploadFile
    {
        private DataContext DbContext;
        public UploadFile(DataContext DbContext)
        {
            this.DbContext = DbContext;
        }
        /// <summary>
        /// This serverless function (Azure function) is used to upload the new spreadsheet data to the database.
        /// </summary>
        /// <param name="req">HTTP request object</param>
        [FunctionName("UploadFile")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            if (req.Headers["Authorization"].ToString().Contains("Bearer")) 
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(req.Headers["Authorization"].ToString().Split(" ")[1]);
                string auth0Audience = Environment.GetEnvironmentVariable("Auth0Audience");
                string auth0Domain = Environment.GetEnvironmentVariable("Auth0Domain");
                if (token.Audiences.Contains(auth0Audience) && token.Issuer == auth0Domain) 
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    FileRow[] file = JsonConvert.DeserializeObject<FileRow[]>(requestBody);
                    await DbContext.Students.AddRangeAsync(file);
                    await DbContext.SaveChangesAsync();
                    return new OkResult();
                }
                return new UnauthorizedObjectResult("Invalid JWT token!");                
            }
            return new BadRequestObjectResult("Authorization header needs to contain bearer token!");
        }
    }
}
