using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RequestsKeeper2.Models;

namespace RequestsKeeper2.DB
{
    public partial class user50_4Context : DbContext
    {
        public user50_4Context()
        {
        }

        public user50_4Context(DbContextOptions<user50_4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<StatusRequest> StatusRequests { get; set; } = null!;
        public virtual DbSet<SubDivision> SubDivisions { get; set; } = null!;
        public virtual DbSet<TypeRequest> TypeRequests { get; set; } = null!;
        public virtual DbSet<Visit> Visits { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;
        public virtual DbSet<VisitorRequest> VisitorRequests { get; set; } = null!;
        public virtual DbSet<VisitorVisit> VisitorVisits { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.200.35;user=user50;password=26643;database=user50_4;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CI_AI_KS_SC_UTF8");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Cause).HasMaxLength(100);

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StatusRequestId).HasColumnName("StatusRequestID");

                entity.Property(e => e.TypeRequestId).HasColumnName("TypeRequestID");

                entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

                entity.HasOne(d => d.StatusRequest)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.StatusRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_StatusRequest");

                entity.HasOne(d => d.TypeRequest)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.TypeRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_TypeRequest");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Worker");
            });

            modelBuilder.Entity<StatusRequest>(entity =>
            {
                entity.ToTable("StatusRequest");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SubDivision>(entity =>
            {
                entity.ToTable("SubDivision");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeRequest>(entity =>
            {
                entity.ToTable("TypeRequest");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("Visit");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.GroupNumber).HasMaxLength(10);

                entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Worker");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("Visitor");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(125);

                entity.Property(e => e.Organisation).HasMaxLength(100);

                entity.Property(e => e.PassportNumber).HasMaxLength(6);

                entity.Property(e => e.PassportSeries).HasMaxLength(4);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(125);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Surname).HasMaxLength(125);
            });

            modelBuilder.Entity<VisitorRequest>(entity =>
            {
                entity.ToTable("VisitorRequest");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.VisitorRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitorRequest_Request");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorRequests)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitorRequest_Visitor");
            });

            modelBuilder.Entity<VisitorVisit>(entity =>
            {
                entity.ToTable("VisitorVisit");

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitorVisits)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitorVisit_Visit");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorVisits)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitorVisit_Visitor");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Name).HasMaxLength(125);

                entity.Property(e => e.Patronymic).HasMaxLength(125);

                entity.Property(e => e.SubDivisionId).HasColumnName("SubDivisionID");

                entity.Property(e => e.Surname).HasMaxLength(125);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Worker_Department");

                entity.HasOne(d => d.SubDivision)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.SubDivisionId)
                    .HasConstraintName("FK_Worker_SubDivision");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
