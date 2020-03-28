using DatabaseConnector.DAO.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    //申领表
    [Table("ChaimForm")]
    public class ClaimForm
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [JsonPropertyName("lid")]
        [Required]
        public int LabId { get; set; }
        //申请人
        [JsonPropertyName("uid")]
        public int UserId { get; set; }
        //预计归还时间
        [JsonPropertyName("rtime")]
        public DateTime ReturnTime { get; set; }
        //实际归还时间
        [JsonPropertyName("rrtime")]
        public DateTime RealReturnTime { get; set; }
        //审核人
        [JsonPropertyName("aid")]
        public int? ApproverId { get; set; }
    }
    [Table("ClaimFormChemical")]
    public class ClaimFormChemical
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public long Id { get; set; }
        [JsonPropertyName("cfId")]
        public long ClaimFormId { get; set; }
        [JsonPropertyName("cid")]
        public long ChemicalId { get; set; }
        [JsonIgnore]
        public Chemical Chemical { get; set; }
        [JsonIgnore]
        public ClaimForm ClaimForm { get; set; }
    }
}
