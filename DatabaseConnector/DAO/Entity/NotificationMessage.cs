using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("NotificationMessage")]
    public class NotificationMessage
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationMessageId { get; set; }
        [JsonPropertyName("cfAck")]
        public long ClaimFormAck { get; set; }
        [JsonPropertyName("dfack")]
        public long DeclarationFormAck { get; set; }
        [JsonPropertyName("ffack")]
        public long FinancialFormAck { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }
}
