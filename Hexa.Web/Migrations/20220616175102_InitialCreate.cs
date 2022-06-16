using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexa.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrantTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrantTypeid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrantTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_GrantTypes_GrantTypes_GrantTypeid",
                        column: x => x.GrantTypeid,
                        principalTable: "GrantTypes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrantTypes_GrantTypeid",
                table: "GrantTypes",
                column: "GrantTypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrantTypes");
        }
    }
}
