using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    #region AccountController
    public class LoginReturn
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("certification")]
        public string Certification { get; set; }
    }
    public class UserRoleResult
    {
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; }
    }
    public class LogoutParam
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
    #endregion
    #region FormController

    #endregion
}
