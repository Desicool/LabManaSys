using DatabaseConnector.DAO.FormData;
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
    [Table("NotificationMessage")]
    public class NotificationMessage
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationMessageId { get; set; }
        [JsonPropertyName("formType")]
        [JsonConverter(typeof(FormTypeConverter))]
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
    public class FormTypeConverter : JsonConverter<FormType>
    {
        public override FormType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.String)
            {
                var tmp = reader.GetString().ToLower();
                if(tmp == "claimform")
                {
                    return FormType.ClaimForm;
                }
                if(tmp == "declarationform")
                {
                    return FormType.DeclarationForm;
                }   
                if(tmp == "financialform")
                {
                    return FormType.FinancialForm;
                }
            }
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, FormType value, JsonSerializerOptions options)
        {
            Console.WriteLine(value.ToString());
            switch (value)
            {
                case FormType.ClaimForm:
                    writer.WriteStringValue("ClaimForm");
                    break;
                case FormType.FinancialForm:
                    writer.WriteStringValue("FinancialForm");
                    break;
                case FormType.DeclarationForm:
                    writer.WriteStringValue("DeclarationForm");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
