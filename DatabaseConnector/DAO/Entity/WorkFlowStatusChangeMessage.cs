using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
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
        [JsonConverter(typeof(RelatedTypeConverter))]
        public RelatedTypeEnum RelatedType { get; set; } = RelatedTypeEnum.None;
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("isread")]
        public bool IsRead { get; set; }
    }
    public enum RelatedTypeEnum { None, ClaimForm, WorkFlow }

    public class RelatedTypeConverter : JsonConverter<RelatedTypeEnum>
    {
        public override RelatedTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var tmp = reader.GetString().ToLower();
                if (tmp == "claimform")
                {
                    return RelatedTypeEnum.ClaimForm;
                }
                if (tmp == "workflow")
                {
                    return RelatedTypeEnum.WorkFlow;
                }
                if (tmp == "none")
                {
                    return RelatedTypeEnum.None;
                }
            }
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, RelatedTypeEnum value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case RelatedTypeEnum.ClaimForm:
                    writer.WriteStringValue("ClaimForm");
                    break;
                case RelatedTypeEnum.WorkFlow:
                    writer.WriteStringValue("WorkFlow");
                    break;
                case RelatedTypeEnum.None:
                    writer.WriteStringValue("None");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
