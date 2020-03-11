using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManagement.DAO.Entity;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.DAO
{
    public class LabContext : DbContext
    {
        public DbSet<Chemical> chemicals;
    }
}
