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
    public class FinancialForm
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
        public string UserId { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        //收款方
        [JsonPropertyName("receiver")]
        public string Receiver { get; set; }
    }
}
