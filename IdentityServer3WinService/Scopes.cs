using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;

namespace IdentityServer3WinService
{
    static class Scopes
    {
        public static List<Scope> Get()
        {
            var scopes= new List<Scope>
            {
            //requiring acces TO
                new Scope
                {
                    Name = Helpers.IdSrv3.ScopeEntryQueueApi
                },
                new Scope
                {
                    Name = Helpers.IdSrv3.ScopeMvcFrontEnd
                },
                new Scope
                {
                    Name = Helpers.IdSrv3.ScopeNancyApi
                },
                new Scope
                {
                    Name = Helpers.IdSrv3.ScopeServiceStackApi
                },
                new Scope
                {
                    Name = Helpers.IdSrv3.ScopeWcfService
                },
                new Scope
                {
                    Name = StandardScopes.Roles.Name
                }
            };
            scopes.AddRange(StandardScopes.All); 
            return scopes;
        }
    }
}