using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "MenuItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "IsActive", "IsDeleted", "Link", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, true, false, null, null, "Operation" },
                    { 5, true, false, null, null, "Stock Management" },
                    { 9, true, false, "~/Views/Purchase/Index.cshtml", null, "Purchase" },
                    { 10, true, false, "~/Views/Accounts/Index.cshtml", null, "Accounts" },
                    { 11, true, false, null, null, "Finance" },
                    { 14, true, false, "~/Views/Admin/Index.cshtml", null, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "IsActive", "IsDeleted", "Link", "ParentId", "Title" },
                values: new object[,]
                {
                    { 2, true, false, "~/Views/Operation/QuoteRequest.cshtml", 1, "Quote Request" },
                    { 3, true, false, "~/Views/Operation/Quote.cshtml", 1, "Quote" },
                    { 4, true, false, "~/Views/Operation/WorkList.cshtml", 1, "Work List" },
                    { 6, true, false, "~/Views/StockManagement/Product.cshtml", 5, "Product" },
                    { 7, true, false, "~/Views/StockManagement/WareHouse.cshtml", 5, "WareHouse" },
                    { 8, true, false, "~/Views/StockManagement/Warehouse Transfer.cshtml", 5, "WareHouse Transfer" },
                    { 12, true, false, "~/Views/Finance/Expense.cshtml", 11, "Expense" },
                    { 13, true, false, "~/Views/Finance/HumanResource.cshtml", 11, "Human Resource" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
