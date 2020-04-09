using DatabaseConnector.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    [Table("FinancialForm")]
    public class FinancialForm : IComparable<FinancialForm>
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [JsonPropertyName("wid")]
        public long WorkFlowId { get; set; }
        [Required]
        [JsonPropertyName("lid")]
        public int LabId { get; set; }
        //申请人
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("uname")]
        public string UserName { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        //收款方
        [JsonPropertyName("receiver")]
        public string Receiver { get; set; }
        [JsonPropertyName("hid")]
        public int? HandlerId { get; set; }
        [JsonPropertyName("hname")]
        public string HandlerName { get; set; }
        [JsonPropertyName("stime")]
        public DateTime SubmitTime { get; set; }
        [JsonPropertyName("state")]
        public FormState State { get; set; }
        public int CompareTo(FinancialForm obj)
        {
            if (SubmitTime == obj.SubmitTime)
                return Id.CompareTo(obj.Id);
            return SubmitTime < obj.SubmitTime ? -1 : 1;
        }
    }
}
