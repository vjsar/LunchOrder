using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using LunchOrder.Identity;
using Newtonsoft.Json.Linq;


namespace LunchOrder.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private const string HostName = "";
        public async Task<IHttpActionResult> Post([FromBody] JObject submission)
        {
            var authenticationResult = await AuthenticateCredentials(submission.ToObject<UserCredentials>());

            if (!authenticationResult.Success)
            {
                return Unauthorized();
            }
            return Ok(new LoginResult { LoginToken = "Token", User = new User { UserId = authenticationResult.User.Login } });
        }

        private async Task<AuthenticationResponse> AuthenticateCredentials(UserCredentials credentials)
        {


            return new AuthenticationResponse() {Success = true, User = new ApplicationUser() {FirstName = "Vijai", LastName = "Sisubalan",Login = "sisuv"}};
        }
    }
}
