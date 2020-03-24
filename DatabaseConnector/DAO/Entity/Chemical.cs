using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DatabaseConnector.DAO.Entity
{
    [Table("Chemical")]
    public class Chemical
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChemicalId { get; set; }
        [Required,MaxLength(64)]
        public string Name { get; set; }
        //所在实验室
        [Required]
        public int LabId { get; set; }
        public string LabName { get; set; }
        [Required]
        public int Amount { get; set; }
        public ChemicalState State { get; set; } = ChemicalState.None;
        
    }
    public enum ChemicalState { None = 0, Lab, InUse, Obsoleted }
}
