using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    //申领表
    public class ClaimForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long WorkFlowId { get; set; }
        [Required]
        public int LabId { get; set; }
        //申请人，外联userid
        public string Applicant { get; set; }
        public int ChemicalId { get; set; }
        //预计归还时间
        public DateTime ReturnTime { get; set; }
        //实际归还时间
        public DateTime RealReturnTime { get; set; }
        //审核人
        public string Approver { get; set; }
    }
}
