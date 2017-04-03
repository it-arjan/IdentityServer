using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3WinService.Helpers;

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
                Username = Appsettings.User("bob"),
                Password = Appsettings.UserPassword("bob"),
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
                Username = Appsettings.User("admin"),
                Password = Appsettings.UserPassword("admin"),
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
                Username = "bob-nomq",
                Password = Appsettings.UserPassword("bob"),
                Subject = "bob2@hitmaster.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "Bob without MQ access"),
                    new Claim(Constants.ClaimTypes.GivenName, "Bob-no-MQ"),
                    new Claim(Constants.ClaimTypes.FamilyName, "no MQ"),
                    new Claim(Constants.ClaimTypes.Role, "Guest"),
                }
            }
        };
        }
    }
}
