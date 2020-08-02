﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AIDB.Models
{
    public partial class ai_platformContext : DbContext
    {
        public ai_platformContext()
        {
        }

        public ai_platformContext(DbContextOptions<ai_platformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Platforminfo> Platforminfo { get; set; }
        public virtual DbSet<Postcontent> Postcontent { get; set; }
        public virtual DbSet<Subchannel> Subchannel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=47.100.172.61;userid=root;pwd=root;port=3306;database=ai_platform;sslmode=none", x => x.ServerVersion("5.7.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platforminfo>(entity =>
            {
                entity.ToTable("platforminfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddressUrl)
                    .HasColumnName("AddressURL")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PlatformName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Postcontent>(entity =>
            {
                entity.ToTable("postcontent");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateManagerId)
                    .HasColumnName("CreateManagerID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateType).HasColumnType("int(255)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CreateUserID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateUserType).HasColumnType("int(255)");

                entity.Property(e => e.MsgAuthor)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgContent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgTitle)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgType).HasColumnType("int(255)");

                entity.Property(e => e.OpenStatus).HasColumnType("int(255)");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("PlatformID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SubChannelId)
                    .HasColumnName("SubChannelID")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Subchannel>(entity =>
            {
                entity.ToTable("subchannel");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddressUrl)
                    .HasColumnName("AddressURL")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnalogPacket)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("PlatformID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.States).HasColumnType("int(11)");

                entity.Property(e => e.SubChannelName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPwd)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
