﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Manager.WebManager.Models;

namespace Web.Manager.Migrations.HealthDbMigrations
{
    [DbContext(typeof(web_managerContext))]
    [Migration("20200715091802_health_0001")]
    partial class health_0001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("LogContent")
                        .HasColumnName("log_content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(4000);

                    b.Property<string>("LogIp")
                        .HasColumnName("log_ip")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("LogName")
                        .HasColumnName("log_name")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LogTime")
                        .HasColumnName("log_time")
                        .HasColumnType("datetime");

                    b.Property<int?>("LogType")
                        .HasColumnName("log_type")
                        .HasColumnType("int");

                    b.Property<string>("ManagerAccount")
                        .HasColumnName("manager_account")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("ManagerGuid")
                        .IsRequired()
                        .HasColumnName("manager_guid")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("MapMethod")
                        .HasColumnName("map_method")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("web_sys_log");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysManager", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("manager_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("create_time")
                        .HasColumnType("datetime");

                    b.Property<string>("CurToken")
                        .HasColumnName("cur_token")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int?>("IsSupper")
                        .HasColumnName("is_supper")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnName("last_login_time")
                        .HasColumnType("datetime");

                    b.Property<string>("ManagerEmail")
                        .HasColumnName("manager_email")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int?>("ManagerIsdel")
                        .HasColumnName("manager_isdel")
                        .HasColumnType("int");

                    b.Property<string>("ManagerName")
                        .HasColumnName("manager_name")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<string>("ManagerPwd")
                        .HasColumnName("manager_pwd")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("ManagerRealname")
                        .HasColumnName("manager_realname")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("ManagerScal")
                        .HasColumnName("manager_scal")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<int?>("ManagerStatus")
                        .HasColumnName("manager_status")
                        .HasColumnType("int");

                    b.Property<string>("ManagerTel")
                        .HasColumnName("manager_tel")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time")
                        .HasColumnType("datetime");

                    b.HasKey("ManagerId")
                        .HasName("PK__web_sys___5A6073FC8C370250");

                    b.HasIndex("ManagerName")
                        .IsUnique()
                        .HasName("IX_web_sys_manager");

                    b.ToTable("web_sys_manager");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysManagerRole", b =>
                {
                    b.Property<int>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("auto_id")
                        .HasColumnType("int");

                    b.Property<int?>("ManagerId")
                        .HasColumnName("manager_id")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("int");

                    b.HasKey("AutoId")
                        .HasName("PK__web_sys___8C1DE98810ECBBBC");

                    b.HasIndex("ManagerId", "RoleId")
                        .IsUnique()
                        .HasName("IX_web_sys_manager_role");

                    b.ToTable("web_sys_manager_role");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysMenu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("menu_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("create_time")
                        .HasColumnType("datetime");

                    b.Property<string>("IndexCode")
                        .HasColumnName("index_code")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("MenuIcon")
                        .HasColumnName("menu_icon")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("MenuItempages")
                        .HasColumnName("menu_itempages")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(3000);

                    b.Property<string>("MenuName")
                        .HasColumnName("menu_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int?>("MenuPid")
                        .HasColumnName("menu_pid")
                        .HasColumnType("int");

                    b.Property<int?>("MenuSort")
                        .HasColumnName("menu_sort")
                        .HasColumnType("int");

                    b.Property<int?>("MenuStatus")
                        .HasColumnName("menu_status")
                        .HasColumnType("int");

                    b.Property<string>("MenuUrl")
                        .HasColumnName("menu_url")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time")
                        .HasColumnType("datetime");

                    b.HasKey("MenuId")
                        .HasName("PK__web_sys___4CA0FADC38DD6F1D");

                    b.HasIndex("IndexCode")
                        .IsUnique()
                        .HasName("IX_web_sys_menu");

                    b.ToTable("web_sys_menu");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysMenuPage", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("page_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("create_time")
                        .HasColumnType("datetime");

                    b.Property<int?>("MainStatus")
                        .HasColumnName("main_status")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnName("menu_id")
                        .HasColumnType("int");

                    b.Property<string>("PageBtnname")
                        .HasColumnName("page_btnname")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("PageName")
                        .HasColumnName("page_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("PageParamters")
                        .HasColumnName("page_paramters")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.Property<int?>("PageStatus")
                        .HasColumnName("page_status")
                        .HasColumnType("int");

                    b.Property<int?>("PageType")
                        .HasColumnName("page_type")
                        .HasColumnType("int");

                    b.Property<string>("PageUrl")
                        .IsRequired()
                        .HasColumnName("page_url")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.Property<string>("PageViewname")
                        .HasColumnName("page_viewname")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time")
                        .HasColumnType("datetime");

                    b.HasKey("PageId")
                        .HasName("PK_web_sys_MENU_PAGE");

                    b.ToTable("web_sys_menu_page");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("create_time")
                        .HasColumnType("datetime");

                    b.Property<string>("RoleName")
                        .HasColumnName("role_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("RoleRemark")
                        .HasColumnName("role_remark")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int?>("RoleStatus")
                        .HasColumnName("role_status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time")
                        .HasColumnType("datetime");

                    b.HasKey("RoleId")
                        .HasName("PK__web_sys___760965CC2520DD2C");

                    b.ToTable("web_sys_role");
                });

            modelBuilder.Entity("Web.Manager.WebManager.Models.WebSysRoleMenu", b =>
                {
                    b.Property<int>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("auto_id")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnName("menu_id")
                        .HasColumnType("int");

                    b.Property<string>("PageIds")
                        .HasColumnName("page_ids")
                        .HasColumnType("varchar(300) CHARACTER SET utf8mb4")
                        .HasMaxLength(300);

                    b.Property<int?>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("int");

                    b.HasKey("AutoId")
                        .HasName("PK__web_sys___8C1DE988F421DA5E");

                    b.ToTable("web_sys_role_menu");
                });
#pragma warning restore 612, 618
        }
    }
}
