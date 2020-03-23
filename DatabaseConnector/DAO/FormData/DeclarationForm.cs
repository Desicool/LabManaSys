using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.FormData
{
    public class DeclarationForm
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long WorkFlowId { get; set; }
        [Required]
        public int LabId { get; set; }
        //申请人
        public string Applicant { get; set; }
        // 申报理由、用途
        public string Reason { get; set; }
        public string FactoryName { get; set; }
        // 生产日期
        public DateTime ProductionTime { get; set; }
        // 数量
        public int Amount { get; set; }
        // 单价
        public double UnitPrice { get; set; }
        // 计量单位
        public string UnitMeasurement { get; set; }

    }
}
