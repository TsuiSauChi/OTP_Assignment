using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ef_dotnet
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Otp> Otps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=172.18.0.2;Database=postgres;Username=postgres;Password=example");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("otp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attempt).HasColumnName("attempt");

                entity.Property(e => e.Otp1).HasColumnName("otp");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("otp_user_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("users_status_id_fkey");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("user_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusName).HasColumnName("status_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
