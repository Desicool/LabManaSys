using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseConnector.DAO.Entity
{
    //personal data
    public class User : IdentityUser
    {
        //所属实验室的编号和名称
        public int LabId { get; set; }
        public string LabName { get; set; }
    }
}
