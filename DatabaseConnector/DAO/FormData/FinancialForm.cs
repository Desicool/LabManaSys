using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    public class FinancialForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long WorkFlowId { get; set; }
        [Required]
        public int LabId { get; set; }
        //申请人
        public string Applicant { get; set; }
        public double Price { get; set; }
        public string Receiver { get; set; }
    }
}
