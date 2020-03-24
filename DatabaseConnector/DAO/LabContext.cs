using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO.Entity;
using DatabaseConnector.DAO.FormData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector.DAO
{
    public class LabContext : DbContext
    {
        public LabContext(DbContextOptions<LabContext> options)
            : base(options)
        { }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
        public DbSet<WorkFlow> WorkFlows { get; set; }
        public DbSet<DeclarationForm> DeclarationForms { get; set; }
        public DbSet<ClaimForm> ClaimForms { get; set; }
        public DbSet<FinancialForm> FinancialForms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoleRelation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chemical>()
                .HasIndex(c => c.LabId);
            modelBuilder.Entity<Budget>()
                .HasIndex(b => b.LabId);
            modelBuilder.Entity<NotificationMessage>()
                .HasIndex(u => u.UserId);
            modelBuilder.Entity<ClaimForm>()
                .HasIndex(c => c.LabId);
            modelBuilder.Entity<DeclarationForm>()
                .HasIndex(d => d.LabId);
            modelBuilder.Entity<FinancialForm>()
                .HasIndex(d => d.LabId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
    }
}
