using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepositoryLayer.Migrations
{
    public partial class demo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Collaborators",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Collaborators",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
