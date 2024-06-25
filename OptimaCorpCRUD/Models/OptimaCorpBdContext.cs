using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OptimaCorpCRUD.Models;

public partial class OptimaCorpBdContext : DbContext
{
    public OptimaCorpBdContext()
    {
    }

    public OptimaCorpBdContext(DbContextOptions<OptimaCorpBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DEPARTME__3214EC27BE1EBAA9");

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMPLOYEE__3214EC27A3EC1158");

            entity.ToTable("EMPLOYEE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IdDeparmentFk)
            .HasColumnName("ID_DEPARMENT_FK");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Salary)
            .HasColumnName("SALARY");
            entity.HasOne(d => d.IdDeparmentFkNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDeparmentFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DEPARMENT_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
