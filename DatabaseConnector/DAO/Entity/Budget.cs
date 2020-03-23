using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    public class Budget
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        // 预算周期，如xx年第x季度等
        public string Period { get; set; }
        [Required]
        //总预算
        public double Total { get; set; }
        [Required]
        //已用预算
        public double Used { get; set; } = 0.0;
        [Required]
        public int LabId { get; set; }
        public string LabName { get; set; }
    }
}
