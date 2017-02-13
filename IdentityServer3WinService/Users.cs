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
                Username = "bob",
                Password = "realBigSecret",
                Subject = "bobbie@bob.com", //unique userid
                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.Name, "Bobbie bob"),
                    new Claim(Constants.ClaimTypes.GivenName, "Bobbie"),
                    new Claim(Constants.ClaimTypes.FamilyName, "bob")
                }
            }
        };
        }
    }
}
