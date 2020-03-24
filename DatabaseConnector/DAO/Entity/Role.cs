using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("Role")]
    public class Role
    {
        [JsonPropertyName("roleId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [JsonPropertyName("roleName")]
        public string RoleName { get; set; }
    }
}
