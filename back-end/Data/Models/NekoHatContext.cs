using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data.Models;

public partial class NekoHatContext : DbContext
{
    public NekoHatContext()
    {
    }

    public NekoHatContext(DbContextOptions<NekoHatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressStatus> AddressStatuses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.CountryId, "fk_addr_ctry");

            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
                .HasColumnName("address_id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("city");
            entity.Property(e => e.CountryId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("country_id");
            entity.Property(e => e.StreetName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("street_name");
            entity.Property(e => e.StreetNumber)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("street_number");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_addr_ctry");
        });

        modelBuilder.Entity<AddressStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("address_status");

            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.AddressStatus1)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("address_status");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(400)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.LanguageId, "fk_book_lang");

            entity.HasIndex(e => e.PublisherId, "fk_book_pub");

            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("isbn13");
            entity.Property(e => e.LanguageId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("language_id");
            entity.Property(e => e.NumPages)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("num_pages");
            entity.Property(e => e.PublicationDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("publication_date");
            entity.Property(e => e.PublisherId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("publisher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(400)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("title");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_book_pub");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_ba_author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_ba_book"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("PRIMARY");
                        j.ToTable("book_author");
                        j.HasIndex(new[] { "AuthorId" }, "fk_ba_author");
                        j.IndexerProperty<int>("BookId")
                            .HasColumnType("int(11)")
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId")
                            .HasColumnType("int(11)")
                            .HasColumnName("author_id");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .HasColumnType("int(11)")
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PRIMARY");

            entity.ToTable("publisher");

            entity.Property(e => e.PublisherId)
                .HasColumnType("int(11)")
                .HasColumnName("publisher_id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(400)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("publisher_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
