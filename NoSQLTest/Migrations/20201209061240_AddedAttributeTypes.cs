using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoSQLTest.Migrations
{
    public partial class AddedAttributeTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AttributeTypes",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { new Guid("90f6e145-f8db-4552-bff9-490238047ee5"), "System.Boolean" },
                    { new Guid("26b9fbf2-ffa7-45c1-8ffc-0ce4cdd86d2f"), "System.String" },
                    { new Guid("d95cdd63-3c29-45fd-a353-dee2f174a86b"), "System.DateTime" },
                    { new Guid("518fee74-8537-4f4a-874b-35f32b24c282"), "System.Int32" },
                    { new Guid("19f00918-4b2e-40b6-ae9d-6ea2a3682270"), "System.Double" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeTypes",
                keyColumn: "Id",
                keyValue: new Guid("19f00918-4b2e-40b6-ae9d-6ea2a3682270"));

            migrationBuilder.DeleteData(
                table: "AttributeTypes",
                keyColumn: "Id",
                keyValue: new Guid("26b9fbf2-ffa7-45c1-8ffc-0ce4cdd86d2f"));

            migrationBuilder.DeleteData(
                table: "AttributeTypes",
                keyColumn: "Id",
                keyValue: new Guid("518fee74-8537-4f4a-874b-35f32b24c282"));

            migrationBuilder.DeleteData(
                table: "AttributeTypes",
                keyColumn: "Id",
                keyValue: new Guid("90f6e145-f8db-4552-bff9-490238047ee5"));

            migrationBuilder.DeleteData(
                table: "AttributeTypes",
                keyColumn: "Id",
                keyValue: new Guid("d95cdd63-3c29-45fd-a353-dee2f174a86b"));
        }
    }
}
