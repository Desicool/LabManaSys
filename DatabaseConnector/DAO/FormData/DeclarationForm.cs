using DatabaseConnector.Utils;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    [Table("DeclarationForm")]
    public class DeclarationForm : IComparable<DeclarationForm>
    {
        [JsonPropertyName("id")]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [JsonPropertyName("wid")]
        [Required]
        public long WorkFlowId { get; set; }
        [JsonPropertyName("lid")]
        [Required]
        public int LabId { get; set; }
        //申请人
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("uname")]
        public string UserName { get; set; }
        // 申报理由、用途
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        [JsonPropertyName("hid")]
        public int? HandlerId { get; set; }
        [JsonPropertyName("hname")]
        public string HandlerName { get; set; }
        [JsonPropertyName("stime")]
        public DateTime SubmitTime { get; set; }
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FormState State { get; set; }
        public int CompareTo(DeclarationForm obj)
        {
            if (SubmitTime == obj.SubmitTime)
                return Id.CompareTo(obj.Id);
            return SubmitTime < obj.SubmitTime ? -1 : 1;
        }
    }
}
