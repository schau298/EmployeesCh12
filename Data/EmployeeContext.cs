using EmployeesCh12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesCh12.Data;
namespace EmployeesCh12.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        public DbSet<Benefits>Benefits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite Key
            modelBuilder.Entity<DepartmentLocation>().HasKey(d => new { d.DepartmentID, d.LocationID });
            //one to many relationship
            modelBuilder.Entity<DepartmentLocation>().HasOne(d1 => d1.Department)
            .WithMany(d => d.DepartmentLocations).HasForeignKey(d1 => d1.DepartmentID);
            //one to many
            modelBuilder.Entity<DepartmentLocation>().HasOne(d1 => d1.Location)
            .WithMany(d => d.DepartmentLocations).HasForeignKey(d1 => d1.LocationID);
            //one to one
            modelBuilder.Entity<Benefits>().HasOne<Employee>(p => p.Employee)
                .WithOne(s => s.Benefits).HasForeignKey<Employee>(b => b.BenefitID);
            modelBuilder.Entity<Employee>().HasOne<Benefits>(p => p.Benefits)
                .WithOne(s => s.Employee).HasForeignKey<Benefits>(b => b.EmployeeID);
        }
    }
}
