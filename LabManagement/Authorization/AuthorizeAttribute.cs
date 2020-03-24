using DatabaseConnector.DAO.Entity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using LabManagement.Utils;
using System.Threading;
using System.Security.Principal;
namespace LabManagement.Authorization
{
    public class AuthorizeAttribute : Attribute,IAuthorizationFilter
    {
        public string Role { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //If the Authorization header is empty or null
            //then return Unauthorized
            string header = context.HttpContext.Request.Headers["certification"];
            if (string.IsNullOrEmpty(header))
            {
                context.Result = new UnauthorizedObjectResult("headers don't include certification");
                return;
            }
            else
            {
                //call the cache to check the certification
                if (UserRoleCache.TryGetUserRole(header, out UserRoleResult result))
                {
                    if (string.IsNullOrEmpty(Role))
                    {
                        return;
                    }
                    var list = Role.Split(',').ToHashSet();
                    foreach(var role in result.Roles)
                    {
                        if (list.Contains(role.RoleName))
                        {
                            // matches, let go.
                            return;
                        }
                    }
                    context.Result = new UnauthorizedObjectResult("Insufficient permissions");
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("/account/login");
                    return;
                }
            }

        }
    }

}
