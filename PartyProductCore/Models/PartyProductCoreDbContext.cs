using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PartyProductCore.Models;

public partial class PartyProductCoreDbContext : DbContext
{
    public PartyProductCoreDbContext()
    {
    }

    public PartyProductCoreDbContext(DbContextOptions<PartyProductCoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAssignParty> TblAssignParties { get; set; }

    public virtual DbSet<TblInvoiceDetail> TblInvoiceDetails { get; set; }

    public virtual DbSet<TblParty> TblParties { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductRate> TblProductRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAssignParty>(entity =>
        {
            entity.ToTable("tblAssignParty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartyId).HasColumnName("party_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Party).WithMany(p => p.TblAssignParties)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblAssignParty_tblParty");

            entity.HasOne(d => d.Product).WithMany(p => p.TblAssignParties)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_tblAssignParty_tblProduct");
        });

        modelBuilder.Entity<TblInvoiceDetail>(entity =>
        {
            entity.ToTable("tblInvoiceDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartyId).HasColumnName("party_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("rate");

            entity.HasOne(d => d.Party).WithMany(p => p.TblInvoiceDetails)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblInvoiceDetail_tblParty");

            entity.HasOne(d => d.Product).WithMany(p => p.TblInvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblInvoiceDetail_tblProduct");
        });

        modelBuilder.Entity<TblParty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblParty_1");

            entity.ToTable("tblParty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("party_name");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.ToTable("tblProduct");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_name");
        });

        modelBuilder.Entity<TblProductRate>(entity =>
        {
            entity.ToTable("tblProductRate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("rate");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductRates)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_tblProductRate_tblProduct");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
