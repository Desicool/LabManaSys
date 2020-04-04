using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DatabaseConnector.DAO.Entity
{
    [Table("Chemical")]
    public class Chemical
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ChemicalId { get; set; }
        [JsonPropertyName("name")]
        [Required, MaxLength(64)]
        public string Name { get; set; }
        //所在实验室
        [JsonPropertyName("labId")]
        [Required]
        public int LabId { get; set; }
        [JsonPropertyName("wfId")]
        public long WorkFlowId { get; set; }
        [JsonPropertyName("labName")]
        public string LabName { get; set; }
        [JsonPropertyName("amount")]
        [Required]
        public int Amount { get; set; }
        // 生产厂家
        [JsonPropertyName("fname")]
        public string FactoryName { get; set; }
        // 生产日期
        [JsonPropertyName("ptime")]
        public DateTime ProductionTime { get; set; }
        // 单价
        [JsonPropertyName("unitprice")]
        public double UnitPrice { get; set; }
        // 计量单位
        [JsonPropertyName("unitmeasurement")]
        public string UnitMeasurement { get; set; }
        [JsonPropertyName("state")]
        public ChemicalState State { get; set; } = ChemicalState.None;
        
    }
    public enum ChemicalState { None = 0, Lab, InUse, Obsoleted }
}
