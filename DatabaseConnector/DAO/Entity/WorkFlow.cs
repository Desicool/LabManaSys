using DatabaseConnector.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("WorkFlow")]
    public class WorkFlow
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        // 申请账号的id
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("uname")]
        public string UserName { get; set; }
        [JsonPropertyName("chemicals")]
        public List<Chemical> Chemicals { get; set; }
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        // 简单的概括
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; } = "None";
    }
}
