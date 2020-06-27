using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImage_Products_ProductId",
                table: "GalleryImage");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "File");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousPrice",
                table: "Products",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "GalleryImage",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileId",
                table: "Products",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImage_Products_ProductId",
                table: "GalleryImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_File_FileId",
                table: "Products",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImage_Products_ProductId",
                table: "GalleryImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_File_FileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "GalleryImage",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "File",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImage_Products_ProductId",
                table: "GalleryImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
