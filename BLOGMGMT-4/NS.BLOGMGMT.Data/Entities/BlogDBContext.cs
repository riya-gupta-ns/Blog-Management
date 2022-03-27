using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NS.BLOGMGMT.Data.CustomEntities;

namespace NS.BLOGMGMT.Data.Entities
{
    public partial class BlogDBContext : DbContext
    {
        public BlogDBContext()
        {
        }

        public BlogDBContext(DbContextOptions<BlogDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<BlogType> BlogTypes { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<StaticContent> StaticContents { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<BlogAndUser> BlogAndUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.2.30;Database=B22-BLOGMGMT;User Id=BLOGMGMT;Password=BLOGMGMT123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.BlogTitle).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsPublish).HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.BlogType)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.BlogTypeId)
                    .HasConstraintName("FK__Blogs__BlogTypeI__787EE5A0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Blogs__UserId__5165187F");
            });

            modelBuilder.Entity<BlogType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__BlogType__516F03B5F661D843");

                entity.ToTable("BlogType");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentContent).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__Comments__BlogId__5535A963");
            });

            modelBuilder.Entity<StaticContent>(entity =>
            {
                entity.HasKey(e => e.ContentId)
                    .HasName("PK__StaticCo__2907A81E27285561");

                entity.ToTable("StaticContent");

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(164)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasDefaultValueSql("('1')");

                entity.Property(e => e.UserFullName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });
            modelBuilder.Entity<BlogAndUser>(entity => {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
