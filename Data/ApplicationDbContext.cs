using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestPrepation.Data.Models;

namespace TestPrepation.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Client { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuse { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =(localdb)\\MSSQLLocalDB ; Database = Test_Prepation");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Client>(entity =>
        //{
        //    entity.HasKey(e => e.ClientId).HasName("PK__Client__E67E1A04CBAC0B29");

        //    entity.ToTable("Client");

        //    entity.Property(e => e.ClientId)
        //        .ValueGeneratedNever()
        //        .HasColumnName("ClientID");
        //    entity.Property(e => e.DateOfBirth).HasColumnType("date");
        //    entity.Property(e => e.Email)
        //        .HasMaxLength(255)
        //        .IsUnicode(false);
        //    entity.Property(e => e.FirstName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.ImagePath)
        //        .HasMaxLength(255)
        //        .IsUnicode(false);
        //    entity.Property(e => e.LastName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");
        //    entity.Property(e => e.MobileNumber)
        //        .HasMaxLength(15)
        //        .IsUnicode(false);

        //    entity.HasOne(d => d.MaritalStatus).WithMany(p => p.Clients)
        //        .HasForeignKey(d => d.MaritalStatusId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_Client_MaritalStatus");
        //});

        //modelBuilder.Entity<MaritalStatus>(entity =>
        //{
        //    entity.HasKey(e => e.MaritalStatusId).HasName("PK__MaritalS__C8B1BA527352655F");

        //    entity.ToTable("MaritalStatus");

        //    entity.Property(e => e.MaritalStatusId)
        //        .ValueGeneratedNever()
        //        .HasColumnName("MaritalStatusID");
        //    entity.Property(e => e.MaritalStatusName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
