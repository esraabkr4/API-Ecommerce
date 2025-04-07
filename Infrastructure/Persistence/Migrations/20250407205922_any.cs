using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class any : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_TypeId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBrand",
                table: "ProductBrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductType",
                newName: "productsTypes");

            migrationBuilder.RenameTable(
                name: "ProductBrand",
                newName: "productBrands");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_TypeId",
                table: "products",
                newName: "IX_products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productsTypes",
                table: "productsTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productBrands",
                table: "productBrands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productBrands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productsTypes_TypeId",
                table: "products",
                column: "TypeId",
                principalTable: "productsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productBrands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productsTypes_TypeId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productsTypes",
                table: "productsTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productBrands",
                table: "productBrands");

            migrationBuilder.RenameTable(
                name: "productsTypes",
                newName: "ProductType");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "productBrands",
                newName: "ProductBrand");

            migrationBuilder.RenameIndex(
                name: "IX_products_TypeId",
                table: "Product",
                newName: "IX_Product_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductType",
                table: "ProductType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBrand",
                table: "ProductBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_TypeId",
                table: "Product",
                column: "TypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
