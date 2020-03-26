using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Utils
{
    public class PostDeclarationFormParam
    {
        [JsonPropertyName("form")]
        public DeclarationForm Form { get; set; }
        [JsonPropertyName("chemicals")]
        public List<Chemical> Chemicals { get; set; }
    }
}
