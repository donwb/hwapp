using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hwapp
{
    public partial class mapContext : DbContext
    {
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Mapuser> Mapuser { get; set; }
        public virtual DbSet<Useractions> Useractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:mapprogram.database.windows.net,1433;Initial Catalog=map;uid=mapuser;Password=MapProgram!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actions>(entity =>
            {
                entity.ToTable("actions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasColumnName("action")
                    .HasColumnType("text");

                entity.Property(e => e.Points).HasColumnName("points");
            });

            modelBuilder.Entity<Mapuser>(entity =>
            {
                entity.ToTable("mapuser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Useractions>(entity =>
            {
                entity.ToTable("useractions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actiondate)
                    .HasColumnName("actiondate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Actionsid).HasColumnName("actionsid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Actions)
                    .WithMany(p => p.Useractions)
                    .HasForeignKey(d => d.Actionsid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserActions_ActionID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Useractions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserActions_Mapid");
            });
        }
    }
}