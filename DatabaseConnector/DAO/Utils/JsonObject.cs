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
    public class PostFinancialFormParam
    {
        [JsonPropertyName("form")]
        public FinancialForm Form { get; set; }
    }
    public class PostClaimFormParam
    {
        [JsonPropertyName("form")]
        public ClaimForm Form { get; set; }
        [JsonPropertyName("chemicals")]
        public List<Chemical> Chemicals { get; set; }
    }
    public class SolveFormParam
    {
        // 操作者
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        [JsonPropertyName("uname")]
        public string UserName { get; set; }
        [JsonPropertyName("fid")]
        public int FormId { get; set; }
        [JsonPropertyName("lid")]
        public int LabId { get; set; }
    }
    public class MsgResult
    {
        [JsonPropertyName("cform")]
        public List<ClaimForm> ClaimForms { get; set; }
        [JsonPropertyName("dform")]
        public List<DeclarationForm> DeclarationForms { get; set; }
        [JsonPropertyName("fform")]
        public List<FinancialForm> FinancialForms { get; set; }
    }
    public class NotifyResult
    {
        [JsonPropertyName("cform")]
        public List<ClaimForm> ClaimForms { get; set; }

        [JsonPropertyName("wf")]
        public List<WorkFlow> WorkFlows { get; set; }
    }
}
