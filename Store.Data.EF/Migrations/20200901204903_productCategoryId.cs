using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class productCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
             name: "PK_ProductCategory",
             table: "ProductCategory");

            migrationBuilder.DropColumn(
             name: "Id",
             table: "ProductCategory");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ProductCategory",
                nullable: false,
                type: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
             name: "PK_ProductCategory",
             table: "ProductCategory");

            migrationBuilder.DropColumn(
                 name: "Id",
                table: "ProductCategory");

            migrationBuilder.AddColumn<long>(
             name: "Id",
             table: "ProductCategory",
             nullable: false,
             type: "bigint")
             .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
