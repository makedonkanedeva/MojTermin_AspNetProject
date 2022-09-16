using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.Identity;
using MojTermin.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<MojTerminUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Referral> Referral { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<DiagnosisInVisit> DiagnosisInVisits { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<EmailMessage> EmailMessage { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DiagnosisInVisit>()
                .HasKey(c => new { c.DiagnosisId, c.VisitId });

        }
    }
}
