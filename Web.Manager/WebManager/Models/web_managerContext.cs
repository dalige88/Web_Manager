using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web.Manager.WebManager.Models
{
    public partial class web_managerContext : DbContext
    {
        public web_managerContext()
        {
        }

        public web_managerContext(DbContextOptions<web_managerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WebSysLog> WebSysLog { get; set; }
        public virtual DbSet<WebSysManager> WebSysManager { get; set; }
        public virtual DbSet<WebSysManagerRole> WebSysManagerRole { get; set; }
        public virtual DbSet<WebSysMenu> WebSysMenu { get; set; }
        public virtual DbSet<WebSysMenuPage> WebSysMenuPage { get; set; }
        public virtual DbSet<WebSysRole> WebSysRole { get; set; }
        public virtual DbSet<WebSysRoleMenu> WebSysRoleMenu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=47.100.172.61;userid=root;pwd=root;port=3306;database=web_manager;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebSysLog>(entity =>
            {
                entity.ToTable("web_sys_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogContent)
                    .HasColumnName("log_content")
                    .HasMaxLength(4000);

                entity.Property(e => e.LogIp)
                    .HasColumnName("log_ip")
                    .HasMaxLength(100);

                entity.Property(e => e.LogName)
                    .HasColumnName("log_name")
                    .HasMaxLength(100);

                entity.Property(e => e.LogTime)
                    .HasColumnName("log_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LogType).HasColumnName("log_type");

                entity.Property(e => e.ManagerAccount)
                    .HasColumnName("manager_account")
                    .HasMaxLength(50);

                entity.Property(e => e.ManagerGuid)
                    .IsRequired()
                    .HasColumnName("manager_guid")
                    .HasMaxLength(50);

                entity.Property(e => e.MapMethod)
                    .HasColumnName("map_method")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WebSysManager>(entity =>
            {
                entity.HasKey(e => e.ManagerId)
                    .HasName("PK__web_sys___5A6073FC8C370250");

                entity.ToTable("web_sys_manager");

                entity.HasIndex(e => e.ManagerName)
                    .HasName("IX_web_sys_manager")
                    .IsUnique();

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurToken)
                    .HasColumnName("cur_token")
                    .HasMaxLength(50);

                entity.Property(e => e.IsSupper).HasColumnName("is_supper");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ManagerEmail)
                    .HasColumnName("manager_email")
                    .HasMaxLength(50);

                entity.Property(e => e.ManagerIsdel).HasColumnName("manager_isdel");

                entity.Property(e => e.ManagerName)
                    .HasColumnName("manager_name")
                    .HasMaxLength(30);

                entity.Property(e => e.ManagerPwd)
                    .HasColumnName("manager_pwd")
                    .HasMaxLength(50);

                entity.Property(e => e.ManagerRealname)
                    .HasColumnName("manager_realname")
                    .HasMaxLength(20);

                entity.Property(e => e.ManagerScal)
                    .HasColumnName("manager_scal")
                    .HasMaxLength(10);

                entity.Property(e => e.ManagerStatus).HasColumnName("manager_status");

                entity.Property(e => e.ManagerTel)
                    .HasColumnName("manager_tel")
                    .HasMaxLength(30);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<WebSysManagerRole>(entity =>
            {
                entity.HasKey(e => e.AutoId)
                    .HasName("PK__web_sys___8C1DE98810ECBBBC");

                entity.ToTable("web_sys_manager_role");

                entity.HasIndex(e => new { e.ManagerId, e.RoleId })
                    .HasName("IX_web_sys_manager_role")
                    .IsUnique();

                entity.Property(e => e.AutoId).HasColumnName("auto_id");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");
            });

            modelBuilder.Entity<WebSysMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__web_sys___4CA0FADC38DD6F1D");

                entity.ToTable("web_sys_menu");

                entity.HasIndex(e => e.IndexCode)
                    .HasName("IX_web_sys_menu")
                    .IsUnique();

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.IndexCode)
                    .HasColumnName("index_code")
                    .HasMaxLength(50);

                entity.Property(e => e.MenuIcon)
                    .HasColumnName("menu_icon")
                    .HasMaxLength(20);

                entity.Property(e => e.MenuItempages)
                    .HasColumnName("menu_itempages")
                    .HasMaxLength(3000);

                entity.Property(e => e.MenuName)
                    .HasColumnName("menu_name")
                    .HasMaxLength(50);

                entity.Property(e => e.MenuPid).HasColumnName("menu_pid");

                entity.Property(e => e.MenuSort).HasColumnName("menu_sort");

                entity.Property(e => e.MenuStatus).HasColumnName("menu_status");

                entity.Property(e => e.MenuUrl)
                    .HasColumnName("menu_url")
                    .HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<WebSysMenuPage>(entity =>
            {
                entity.HasKey(e => e.PageId)
                    .HasName("PK_web_sys_MENU_PAGE");

                entity.ToTable("web_sys_menu_page");

                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MainStatus).HasColumnName("main_status");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.PageBtnname)
                    .HasColumnName("page_btnname")
                    .HasMaxLength(50);

                entity.Property(e => e.PageName)
                    .HasColumnName("page_name")
                    .HasMaxLength(50);

                entity.Property(e => e.PageParamters)
                    .HasColumnName("page_paramters")
                    .HasMaxLength(300);

                entity.Property(e => e.PageStatus).HasColumnName("page_status");

                entity.Property(e => e.PageType).HasColumnName("page_type");

                entity.Property(e => e.PageUrl)
                    .IsRequired()
                    .HasColumnName("page_url")
                    .HasMaxLength(300);

                entity.Property(e => e.PageViewname)
                    .HasColumnName("page_viewname")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<WebSysRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__web_sys___760965CC2520DD2C");

                entity.ToTable("web_sys_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(50);

                entity.Property(e => e.RoleRemark)
                    .HasColumnName("role_remark")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleStatus).HasColumnName("role_status");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<WebSysRoleMenu>(entity =>
            {
                entity.HasKey(e => e.AutoId)
                    .HasName("PK__web_sys___8C1DE988F421DA5E");

                entity.ToTable("web_sys_role_menu");

                entity.Property(e => e.AutoId).HasColumnName("auto_id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.PageIds)
                    .HasColumnName("page_ids")
                    .HasMaxLength(300);

                entity.Property(e => e.RoleId).HasColumnName("role_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
