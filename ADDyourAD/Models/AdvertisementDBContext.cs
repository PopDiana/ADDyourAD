using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ADDyourAD.Models
{
    public partial class AdvertisementDBContext : DbContext
    {
        public AdvertisementDBContext()
        {
        }

        public AdvertisementDBContext(DbContextOptions<AdvertisementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Advertisement> Advertisement { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=AdvertisementDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.HasKey(e => e.IdAdvertisement);

                entity.Property(e => e.IdAdvertisement).HasColumnName("ID_Advertisement");

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("Expiration_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Advertisement)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Advertisement_Category");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Advertisement)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Advertisement_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("IX_Category")
                    .IsUnique();

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("Category_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.Property(e => e.IdComment).HasColumnName("ID_Comment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IdAdvertisement).HasColumnName("ID_Advertisement");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.IdAdvertisementNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdAdvertisement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Advertisement");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                /*entity.HasIndex(e => e.Username)
                    .HasName("IX_User")
                    .IsUnique();*/

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
