using Microsoft.EntityFrameworkCore.Migrations;

namespace NoSQLTest.Migrations
{
    public partial class AddInnerLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InnerLabel",
                table: "EntityAttributes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerLabel",
                table: "EntityAttributes");
        }
    }
}
