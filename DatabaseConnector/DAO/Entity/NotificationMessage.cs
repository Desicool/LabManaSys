using DatabaseConnector.DAO.FormData;
using Newtonsoft.Json.Converters;
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
        [JsonPropertyName("formType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FormType FormType { get; set; }
        [JsonPropertyName("fid")]
        public long FormId { get; set; }
        [JsonPropertyName("rid")]
        public int RoleId { get; set; } 
        [JsonPropertyName("issolved")]
        public bool IsSolved { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }

    public enum FormType { ClaimForm, DeclarationForm, FinancialForm }
}
