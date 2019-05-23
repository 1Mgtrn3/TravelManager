using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TravelManager.Models
{
    public class TravelManagerContext: IdentityDbContext<UserIdentity>
    {
        public TravelManagerContext(DbContextOptions<TravelManagerContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            base.OnModelCreating(modelBuilder);
            //one-to-one
            modelBuilder.Entity<Document>().HasOne(d => d.DocumentAsignee).WithOne(d => d.Document).HasForeignKey<DocumentAsignee>(d => d.DocumentId);
            modelBuilder.Entity<Employee>().HasOne(d => d.DocumentAsignee).WithOne(d => d.Employee).HasForeignKey<DocumentAsignee>(d => d.EmployeeId);
            modelBuilder.Entity<Role>().HasOne(d => d.RoleAsignee).WithOne(d => d.Role).HasForeignKey<RoleAsignee>(d => d.RoleId);
            modelBuilder.Entity<Employee>().HasOne(d => d.RoleAsignee).WithOne(d => d.Employee).HasForeignKey<RoleAsignee>(d => d.EmployeeId);

            //one-to-many
            modelBuilder.Entity<Event>().HasOne(E => E.Place).WithMany(P => P.Events).HasForeignKey(E => E.PlaceId);
            modelBuilder.Entity<Event>().HasOne(E => E.Currency).WithMany(C => C.Events).HasForeignKey(E => E.CurrencyId);
            modelBuilder.Entity<User>().HasOne(U => U.Currency).WithMany(C => C.Users).HasForeignKey(E => E.CurrencyId);
            modelBuilder.Entity<ExchangeRate>().HasOne(E => E.FirstCurrency).WithMany(C => C.FirstExchangeRates).HasForeignKey(E => E.FirstCurrencyId);
            modelBuilder.Entity<ExchangeRate>().HasOne(E => E.SecondCurrency).WithMany(C => C.SecondExchangeRates).HasForeignKey(E => E.SecondCurrencyId);//.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DocumentAsignee>().HasOne(D => D.Event).WithMany(E => E.DocumentAsignees).HasForeignKey(D => D.EventId);
            modelBuilder.Entity<RoleAsignee>().HasOne(D => D.Event).WithMany(E => E.RoleAsignees).HasForeignKey(R => R.EventId);

            //many-to-many
            //Place<->Document
            modelBuilder.Entity<PlaceDocument>().HasKey(pd => new { pd.DocumentId, pd.PlaceId });
            modelBuilder.Entity<PlaceDocument>().HasOne(pd => pd.Document).WithMany(D => D.PlaceDocuments).HasForeignKey(pd => pd.DocumentId);
            modelBuilder.Entity<PlaceDocument>().HasOne(pd => pd.Place).WithMany(P => P.PlaceDocuments).HasForeignKey(pd => pd.PlaceId);

            //Place<->Role
            modelBuilder.Entity<PlaceRole>().HasKey(pr => new { pr.PlaceId, pr.RoleId });
            modelBuilder.Entity<PlaceRole>().HasOne(pd => pd.Role).WithMany(D => D.PlaceRoles).HasForeignKey(pd => pd.RoleId);
            modelBuilder.Entity<PlaceRole>().HasOne(pd => pd.Place).WithMany(P => P.PlaceRoles).HasForeignKey(pd => pd.PlaceId);
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }        
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Place> Places { get; set; }
        


        public DbSet<RoleAsignee> RoleAsignees { get; set; }
        public DbSet<DocumentAsignee> DocumentAsignees { get; set; }
        public DbSet<PlaceDocument> PlaceDocuments { get; set; }
        public DbSet<PlaceRole> PlaceRoles { get; set; }


    }
}
