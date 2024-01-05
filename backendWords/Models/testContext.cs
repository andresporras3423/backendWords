using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backendWords.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Techno> Technos { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;
        public virtual DbSet<WordPractice> WordPractices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:all-my-data.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=oscar;Password=Malthus4534.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Techno>(entity =>
            {
                entity.ToTable("technos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.TechnoName)
                    .HasMaxLength(255)
                    .HasColumnName("techno_name");

                entity.Property(e => e.TechnoStatus)
                    .HasMaxLength(5)
                    .HasColumnName("techno_status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Technos)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__technos_t__user___02084FDA");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("tests");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Correct).HasColumnName("correct");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__tests_tem__user___02FC7413");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.PasswordDigest)
                    .HasMaxLength(255)
                    .HasColumnName("password_digest");

                entity.Property(e => e.RememberToken)
                    .HasMaxLength(255)
                    .HasColumnName("remember_token");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("words");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.TechnoId).HasColumnName("techno_id");

                entity.Property(e => e.Translation).HasColumnName("translation");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Word1)
                    .HasMaxLength(255)
                    .HasColumnName("word");

                entity.HasOne(d => d.Techno)
                    .WithMany(p => p.Words)
                    .HasForeignKey(d => d.TechnoId)
                    .HasConstraintName("FK__words_tem__techn__04E4BC85");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Words)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__words_tem__user___03F0984C");
            });

            modelBuilder.Entity<WordPractice>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.translation).HasColumnName("translation");
                entity.Property(e => e.word).HasColumnName("word");
                entity.Property(e => e.techno_name).HasColumnName("techno_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
