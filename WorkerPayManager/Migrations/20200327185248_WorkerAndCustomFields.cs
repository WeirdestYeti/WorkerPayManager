using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerPayManager.Migrations
{
    public partial class WorkerAndCustomFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomWorkerFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomWorkerFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomWorkerFields_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(nullable: true),
                    IdentificationNumber = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomWorkerFieldValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomWorkerFieldId = table.Column<int>(nullable: true),
                    WorkerId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomWorkerFieldValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomWorkerFieldValues_CustomWorkerFields_CustomWorkerFieldId",
                        column: x => x.CustomWorkerFieldId,
                        principalTable: "CustomWorkerFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomWorkerFieldValues_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomWorkerFields_CompanyId",
                table: "CustomWorkerFields",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomWorkerFieldValues_CustomWorkerFieldId",
                table: "CustomWorkerFieldValues",
                column: "CustomWorkerFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomWorkerFieldValues_WorkerId",
                table: "CustomWorkerFieldValues",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CompanyId",
                table: "Workers",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomWorkerFieldValues");

            migrationBuilder.DropTable(
                name: "CustomWorkerFields");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
