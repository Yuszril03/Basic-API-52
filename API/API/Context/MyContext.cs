using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{

    public class MyContext : DbContext //Menghubungkan App -> Database
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            //this.OnConfigu
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Profiling> Profiling { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<AccountRole> AccountRole { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.account)
                .WithOne(e => e.employee)
                .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.profiling)
                .WithOne(p => p.account)
                .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<Education>()
              .HasOne(ed => ed.university)
              .WithMany(u => u.educations);

            modelBuilder.Entity<Profiling>()
                .HasOne(p => p.education)
                .WithMany(ed => ed.profilings); 
            
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.account)
                .WithMany(a => a.accountRoles);

            modelBuilder.Entity<AccountRole>()
               .HasOne(ar => ar.role)
               .WithMany(r => r.accountRoles);




        }

      
    }
}
