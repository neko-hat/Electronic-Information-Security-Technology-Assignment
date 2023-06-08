using System;
using System.Collections.Generic;
using back_end.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data;

public partial class rentallistContext : DbContext
{
    public rentallistContext()
    {
    }

    public rentallistContext(DbContextOptions<rentallistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rentallist> Rentallists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rentallist>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PRIMARY");

            entity.ToTable("rentallist");

            entity.Property(e => e.List1)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("list1");
            entity.Property(e => e.List2)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("list2");
            entity.Property(e => e.List3)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("list3");
            entity.Property(e => e.List4)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("list4");
            entity.Property(e => e.List5)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("list5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
