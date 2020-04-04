using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("User")]
    public class User
    {
        [JsonPropertyName("userId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("userPassword")]
        public string UserPassword { get; set; }

        //所属实验室的编号和名称
        [JsonPropertyName("labId")]
        public int LabId { get; set; }

        [JsonPropertyName("labName")]
        public string LabName { get; set; }
    }
}
