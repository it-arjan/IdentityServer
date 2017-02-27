using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using IdentityServer3.Core;

namespace IdentityServer3WinService
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
        {
            new InMemoryUser
            {
                Username = Helpers.Appsettings.UserBob(),
                Password = Helpers.Appsettings.UserBobPassword(),
                Subject = "bob@hitmaster.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "Bob Dylan"),
                    new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                    new Claim(Constants.ClaimTypes.FamilyName, "Dylan"),
                    new Claim(Constants.ClaimTypes.Role, "Guest"),
                    new Claim(Constants.ClaimTypes.Role, "SendMessage")
                }
            },
            new InMemoryUser
            {
                Username = "bob2",
                Password = Helpers.Appsettings.UserBobPassword(),
                Subject = "bob2@hitmaster.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "Bob without Messages"),
                    new Claim(Constants.ClaimTypes.GivenName, "Bob2"),
                    new Claim(Constants.ClaimTypes.FamilyName, "no messages"),
                    new Claim(Constants.ClaimTypes.Role, "Guest"),
                }
            }
        };
        }
    }
}
