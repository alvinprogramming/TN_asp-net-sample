using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Entities;

public partial class SampleDbContext : DbContext
{
    // this is file for database,
    // for each new table, add DbSet + OnCreatingModel > Entity
    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    // NOTE : In the database, call the respective tables that you will use using the format/syntax below:
    public virtual DbSet<DepartmentInfo> DepartmentInfos { get; set; }
    public virtual DbSet<UserInfo> UserInfos { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer();
        }
    }
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source =DESKTOP-Q79TMCA\\SQLEXPRESS;Database=SampleDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // NOTE : With the said called table above, it MUST match the table fields
        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity
                .ToTable("UserInfo");

            entity.Property(e => e.Id).ValueGeneratedNever(); // Primary Key
            entity.Property(e => e.Address)
            .HasMaxLength(250)
            .IsUnicode(false); // Varchar()
            entity.Property(e => e.Age).HasColumnType("int"); // Number
            entity.Property(e => e.CreatedDate).HasColumnType("datetime"); // Datetime
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsEnabled);

        });

        modelBuilder.Entity<DepartmentInfo>(entity =>
        {
            entity
                .ToTable("DepartmentInfo");

            entity.Property(e => e.DepartmentID).ValueGeneratedNever();
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsEnabled);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
