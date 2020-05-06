using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
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
        public DateTime? ProductionTime { get; set; }
        // 单价
        [JsonPropertyName("unitprice")]
        public double UnitPrice { get; set; }
        // 计量单位
        [JsonPropertyName("unitmeasurement")]
        public string UnitMeasurement { get; set; }
        [JsonPropertyName("state")]
        [JsonConverter(typeof(ChemicalStateConverter))]
        public ChemicalState State { get; set; } = ChemicalState.None;
        
    }
    public enum ChemicalState { None = 0, Lab, InUse, Obsoleted, InApplication }
    public class ChemicalStateConverter : JsonConverter<ChemicalState>
    {
        public override ChemicalState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var tmp = reader.GetString().ToLower();
                if (tmp == "none")
                {
                    return ChemicalState.None;
                }
                if (tmp == "在库")
                {
                    return ChemicalState.Lab;
                }
                if (tmp == "使用中")
                {
                    return ChemicalState.InUse;
                }
                if (tmp == "已销毁")
                {
                    return ChemicalState.Obsoleted;
                }
                if(tmp == "待审核")
                {
                    return ChemicalState.InApplication;
                }
            }
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ChemicalState value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ChemicalState.None:
                    writer.WriteStringValue("None");
                    break;
                case ChemicalState.Lab:
                    writer.WriteStringValue("在库");
                    break;
                case ChemicalState.InUse:
                    writer.WriteStringValue("使用中");
                    break;
                case ChemicalState.InApplication:
                    writer.WriteStringValue("待审核");
                    break;
                case ChemicalState.Obsoleted:
                    writer.WriteStringValue("已销毁");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
