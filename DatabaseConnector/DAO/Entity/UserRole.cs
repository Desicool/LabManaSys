using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("UserRole")]
    public class UserRole
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }
}
