using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportClubAPI.Migrations
{
    public partial class SeederAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Age", "Email", "FirstName", "Gender", "RegistrationDate", "SecondName", "SubscribeStatus" },
                values: new object[,]
                {
                    { 1, "St.Johnes St, 57", 24, "tom-jackson.@gmail.com", "Tom", 0, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5300), "Jackson", 0 },
                    { 2, "TK-center, 5", 60, "meina-gladston.@gmail.com", "Meina", 1, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5360), "Gladston", 1 },
                    { 3, "Jackson St., 24", 50, "John-batista.@gmail.com", "John", 0, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5370), "Batista", 1 },
                    { 4, "Britain St., 77", 52, "Boris-Johnson.@gmail.com", "Boris", 0, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5379), "Johnson", 0 },
                    { 5, "Ladozhskaya St., 15", 22, "Vlad-Kutepov.@gmail.com", "Vladislav", 0, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5389), "Kutepov", 0 },
                    { 6, "Moskovskaya St., 15", 30, "Vlad-Kutepov.@gmail.com", "Evgeniy", 0, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5401), "Bazhenov", 1 },
                    { 7, "Komsomolskaya St., 15", 40, "elena-kapustkina.@gmail.com", "Elena", 1, new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5410), "Kapustovna", 1 }
                });

            migrationBuilder.InsertData(
                table: "SportGoods",
                columns: new[] { "SportGoodId", "Category", "Description", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, 0, "A ball that is used in a football game", "Football ball", 20L },
                    { 2, 1, "A ball that is used in a basketball game", "Basketball ball", 15L },
                    { 3, 5, "A good that is used for a faster swimming", "Slippers", 25L },
                    { 4, 4, "It is used for punching a tennis ball", "Tennis crocket", 30L },
                    { 5, 4, "A ball that is used in a tennis game", "Tennis ball", 15L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SportGoods",
                keyColumn: "SportGoodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SportGoods",
                keyColumn: "SportGoodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SportGoods",
                keyColumn: "SportGoodId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SportGoods",
                keyColumn: "SportGoodId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SportGoods",
                keyColumn: "SportGoodId",
                keyValue: 5);
        }
    }
}
