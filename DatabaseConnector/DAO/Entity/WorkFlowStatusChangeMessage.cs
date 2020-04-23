using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("StatusChangeMessage")]
    public class StatusChangeMessage
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [JsonPropertyName("rid")]
        public long RelatedId { get; set; }
        [JsonPropertyName("rtype")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RelatedTypeEnum RelatedType { get; set; } = RelatedTypeEnum.None;
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("isread")]
        public bool IsRead { get; set; }
        [JsonIgnore]
        public WorkFlow WorkFlow { get; set; }
    }
        public enum RelatedTypeEnum { None, ClaimForm, WorkFlow }
}
