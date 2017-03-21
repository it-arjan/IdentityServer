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
                Username = Helpers.Appsettings.User("bob"),
                Password = Helpers.Appsettings.UserPassword("bob"),
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
                Username = Helpers.Appsettings.User("admin"),
                Password = Helpers.Appsettings.UserPassword("admin"),
                Subject = "admin@messagequeuefrontend.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "admin"),
                    new Claim(Constants.ClaimTypes.GivenName, "admin almighty"),
                    new Claim(Constants.ClaimTypes.FamilyName, "almighty"),
                    //new Claim(Constants.ClaimTypes.Role, "Guest"),
                    //new Claim(Constants.ClaimTypes.Role, "SendMessage"),
                    new Claim(Constants.ClaimTypes.Role, "admin")
                }
            },
            new InMemoryUser
            {
                Username = "bob-no-mq",
                Password = Helpers.Appsettings.UserPassword("bob"),
                Subject = "bob2@hitmaster.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "Bob without Messages"),
                    new Claim(Constants.ClaimTypes.GivenName, "Bob-no-messages"),
                    new Claim(Constants.ClaimTypes.FamilyName, "no messages"),
                    new Claim(Constants.ClaimTypes.Role, "Guest"),
                }
            }
        };
        }
    }
}
