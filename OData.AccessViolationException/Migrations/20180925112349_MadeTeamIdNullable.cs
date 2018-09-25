using Microsoft.EntityFrameworkCore.Migrations;

namespace OData.AccessViolationException.Migrations
{
    public partial class MadeTeamIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "People",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "People",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
