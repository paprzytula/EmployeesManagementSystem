using EmployeesManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;

namespace EmployeesManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(d => d.Employees).WithOne(e => e.Department); // One to many 


            base.OnModelCreating(modelBuilder);

            // Seed Department Table
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(
    new Department { DepartmentId = 2, DepartmentName = "Scada" });
            modelBuilder.Entity<Department>().HasData(
    new Department { DepartmentId = 3, DepartmentName = "Adminstration" });
            modelBuilder.Entity<Department>().HasData(
    new Department { DepartmentId = 4, DepartmentName = "Admin" });

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne<Employee>(es => es.Employee)
                .WithMany(e => e.EmployeeSkills)
                .HasForeignKey(s => s.EmployeesId);

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne<Skill>(es => es.Skill)
                .WithMany(e => e.EmployeeSkills)
                .HasForeignKey(s => s.SkillsId);

            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(p => new { p.EmployeesId, p.SkillsId});
        }
    }
}
