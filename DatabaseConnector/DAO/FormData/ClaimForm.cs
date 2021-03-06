﻿using DatabaseConnector.DAO.Entity;
using DatabaseConnector.Utils;
using Newtonsoft.Json.Converters;
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
    [Table("ClaimForm")]
    public class ClaimForm : IComparable<ClaimForm>
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
        [JsonPropertyName("uname")]
        public string UserName { get; set; }
        //预计归还时间
        [JsonPropertyName("rtime")]
        public DateTime ReturnTime { get; set; }
        //实际归还时间
        [JsonPropertyName("rrtime")]
        public DateTime? RealReturnTime { get; set; }
        //审核人
        [JsonPropertyName("hid")]
        public int? HandlerId { get; set; }
        [JsonPropertyName("hname")]
        public string HandlerName { get; set; }
        [JsonPropertyName("stime")]
        public DateTime SubmitTime { get; set; }
        [JsonPropertyName("state")]
        [JsonConverter(typeof(FormStateConverter))]
        public FormState State { get; set; }
        public int CompareTo(ClaimForm obj)
        {
            if (SubmitTime == obj.SubmitTime)
                return Id.CompareTo(obj.Id);
            return SubmitTime < obj.SubmitTime ? -1 : 1;
        }
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
