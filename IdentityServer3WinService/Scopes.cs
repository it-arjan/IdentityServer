using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityServer3.Core.Models;
using IdentityServer3WinService.Helpers;

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
                    Name = IdSrv3.ScopeMcvFrontEndHuman,
                    Type = ScopeType.Identity,
                    IncludeAllClaimsForUser = true
                },
                new Scope
                {
                    Name = IdSrv3.ScopeMvcFrontEnd,
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = IdSrv3.ScopeEntryQueueApi,
                    Type = ScopeType.Resource

                },
                new Scope
                {
                    Name = IdSrv3.ScopeNancyApi,
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = IdSrv3.ScopeServiceStackApi,
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = IdSrv3.ScopeWcfService,
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = StandardScopes.Roles.Name
                },
            };
            // all identity scopes
            scopes.AddRange(StandardScopes.All);
            return scopes;
        }
    }
}