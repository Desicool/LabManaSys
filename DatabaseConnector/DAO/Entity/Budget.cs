using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("Budget")]
    public class Budget
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BudgetId { get; set; }
        [JsonPropertyName("period")]
        [Required]
        // 预算周期，如xx年第x季度等
        public string Period { get; set; }
        [JsonPropertyName("total")]
        [Required]
        //总预算
        public double Total { get; set; }
        [JsonPropertyName("used")]
        [Required]
        //已用预算
        public double Used { get; set; } = 0.0;
        [JsonPropertyName("labId")]
        [Required]
        public int LabId { get; set; }
        [JsonPropertyName("labName")]
        public string LabName { get; set; }
    }
}
