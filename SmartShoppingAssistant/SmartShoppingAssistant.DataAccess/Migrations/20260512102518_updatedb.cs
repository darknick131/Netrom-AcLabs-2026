using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartShoppingAssistant.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 4, "Pâine, cozonaci și produse de patiserie", "Panificație" },
                    { 5, "Legume și fructe proaspete", "Legume și Fructe" },
                    { 6, "Produse din carne și mezeluri", "Carne și Mezeluri" },
                    { 7, "Produse de igienă și cosmetice", "Îngrijire Personală" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 5, "Băutură carbogazoasă cu aromă de portocale", null, "Fanta Portocale 500ml", 5.49m },
                    { 6, "Băutură carbogazoasă cu lămâie și lime", null, "Sprite 500ml", 5.49m },
                    { 7, "Apă minerală plată", null, "Apă Plată Bucovina 2L", 3.99m },
                    { 8, "Chipsuri crocante în tub", null, "Pringles Original 165g", 12.99m },
                    { 9, "Chipsuri de porumb cu aromă de brânză", null, "Doritos Nacho Cheese 150g", 9.99m },
                    { 10, "Ciocolată cu lapte alpin", null, "Milka Lapte Alpin 100g", 6.49m },
                    { 11, "Lapte proaspăt de vacă 3.5% grăsime", null, "Lapte Zuzu 1L", 7.29m },
                    { 12, "Brânză telemea de vacă", null, "Brânză Telemea 400g", 14.99m },
                    { 13, "Smântână pentru gătit și salate", null, "Smântână 20% 400g", 8.49m },
                    { 14, "Pâine albă moale, feliată", null, "Pâine Albă Feliată 500g", 5.99m },
                    { 15, "Croissant cu unt, proaspăt", null, "Croissant Simplu", 3.49m },
                    { 16, "Mere românești, soiul Ionatan", null, "Mere Ionatan 1kg", 6.99m },
                    { 17, "Roșii cherry proaspete", null, "Roșii Cherry 500g", 8.99m }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "CategoryId", "IsActive", "Name", "ProductId", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[,]
                {
                    { 4, null, true, "Cumpără 2 Pepsi, primești 1 gratis", 2, 0, 1, 2m, 0 },
                    { 5, 3, true, "Cumpără 4 lactate, primești 1 gratis", null, 0, 1, 4m, 0 },
                    { 6, null, true, "20% reducere la comenzi peste 200 RON", null, 1, 20, 200.00m, 1 },
                    { 8, null, true, "5% reducere la comenzi peste 50 RON", null, 1, 5, 50.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 4, 14 },
                    { 4, 15 },
                    { 5, 16 },
                    { 5, 17 }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "CategoryId", "IsActive", "Name", "ProductId", "Reward", "RewardValue", "Threshold", "Type" },
                values: new object[] { 7, null, true, "Cumpără 3 Ape Plată, primești 1 gratis", 7, 0, 1, 3m, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 14 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 16 });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 5, 17 });

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
