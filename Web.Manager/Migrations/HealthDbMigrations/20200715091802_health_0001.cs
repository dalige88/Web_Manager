using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Manager.Migrations.HealthDbMigrations
{
    public partial class health_0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "web_sys_log",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manager_guid = table.Column<string>(maxLength: 50, nullable: false),
                    log_type = table.Column<int>(nullable: true),
                    log_content = table.Column<string>(maxLength: 4000, nullable: true),
                    log_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    log_name = table.Column<string>(maxLength: 100, nullable: true),
                    manager_account = table.Column<string>(maxLength: 50, nullable: true),
                    map_method = table.Column<string>(maxLength: 50, nullable: true),
                    log_ip = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_sys_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_manager",
                columns: table => new
                {
                    manager_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manager_name = table.Column<string>(maxLength: 30, nullable: true),
                    manager_pwd = table.Column<string>(maxLength: 50, nullable: true),
                    manager_scal = table.Column<string>(maxLength: 10, nullable: true),
                    manager_realname = table.Column<string>(maxLength: 20, nullable: true),
                    manager_tel = table.Column<string>(maxLength: 30, nullable: true),
                    manager_email = table.Column<string>(maxLength: 50, nullable: true),
                    manager_isdel = table.Column<int>(nullable: true),
                    manager_status = table.Column<int>(nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_supper = table.Column<int>(nullable: true),
                    last_login_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    cur_token = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__web_sys___5A6073FC8C370250", x => x.manager_id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_manager_role",
                columns: table => new
                {
                    auto_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    manager_id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__web_sys___8C1DE98810ECBBBC", x => x.auto_id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_menu",
                columns: table => new
                {
                    menu_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_name = table.Column<string>(maxLength: 50, nullable: true),
                    menu_pid = table.Column<int>(nullable: true),
                    menu_icon = table.Column<string>(maxLength: 20, nullable: true),
                    index_code = table.Column<string>(maxLength: 50, nullable: true),
                    menu_url = table.Column<string>(maxLength: 200, nullable: true),
                    menu_status = table.Column<int>(nullable: true),
                    menu_itempages = table.Column<string>(maxLength: 3000, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    menu_sort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__web_sys___4CA0FADC38DD6F1D", x => x.menu_id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_menu_page",
                columns: table => new
                {
                    page_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_id = table.Column<int>(nullable: true),
                    main_status = table.Column<int>(nullable: true),
                    page_name = table.Column<string>(maxLength: 50, nullable: true),
                    page_status = table.Column<int>(nullable: true),
                    page_viewname = table.Column<string>(maxLength: 50, nullable: true),
                    page_btnname = table.Column<string>(maxLength: 50, nullable: true),
                    page_type = table.Column<int>(nullable: true),
                    page_url = table.Column<string>(maxLength: 300, nullable: false),
                    page_paramters = table.Column<string>(maxLength: 300, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_sys_MENU_PAGE", x => x.page_id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_role",
                columns: table => new
                {
                    role_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(maxLength: 50, nullable: true),
                    role_status = table.Column<int>(nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    role_remark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__web_sys___760965CC2520DD2C", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "web_sys_role_menu",
                columns: table => new
                {
                    auto_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(nullable: true),
                    menu_id = table.Column<int>(nullable: true),
                    page_ids = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__web_sys___8C1DE988F421DA5E", x => x.auto_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_web_sys_manager",
                table: "web_sys_manager",
                column: "manager_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_web_sys_manager_role",
                table: "web_sys_manager_role",
                columns: new[] { "manager_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_web_sys_menu",
                table: "web_sys_menu",
                column: "index_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "web_sys_log");

            migrationBuilder.DropTable(
                name: "web_sys_manager");

            migrationBuilder.DropTable(
                name: "web_sys_manager_role");

            migrationBuilder.DropTable(
                name: "web_sys_menu");

            migrationBuilder.DropTable(
                name: "web_sys_menu_page");

            migrationBuilder.DropTable(
                name: "web_sys_role");

            migrationBuilder.DropTable(
                name: "web_sys_role_menu");
        }
    }
}
