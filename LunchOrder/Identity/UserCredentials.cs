using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LunchOrder.Identity
{
    public class UserCredentials
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

    public class LoginResult
    {
        [JsonProperty("authenticationToken")]
        public string LoginToken { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class ApplicationUser
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class AuthenticationResponse
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public ApplicationUser User { get; set; }

        public string Token { get; set; }
    }
}