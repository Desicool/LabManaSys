using DatabaseConnector.DAO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    #region AccountController
    public class UserRoleResult
    {
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; }
        public UserRoleResult() { }
        public UserRoleResult(User _user, List<Role> _roles)
        {
            User = _user;
            Roles = _roles;
        }
    }
    public class LogoutParam
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
    #endregion
}
