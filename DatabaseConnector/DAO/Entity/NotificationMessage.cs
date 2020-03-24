using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    [Table("NotificationMessage")]
    public class NotificationMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationMessageId { get; set; }
        public long ClaimFormAck { get; set; }
        public long DeclarationFormAck { get; set; }
        public long FinancialFormAck { get; set; }
        public string UserId { get; set; }
    }
}
