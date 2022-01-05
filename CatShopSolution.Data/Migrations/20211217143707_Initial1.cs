using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatShopSolution.Data.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5110dded-045e-43a3-a90c-d52fb4988fbe");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Dob", "PasswordHash" },
                values: new object[] { "dbabe0cd-6a91-42ed-b441-393d0d369ca3", new DateTime(2021, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEDa0Oa+bpoEGrH1uqJTwcbVmpXWEimfvMGrAFlccs9Roo89XivMhPOGUkcbsAV6Jdg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 12, 17, 21, 37, 6, 875, DateTimeKind.Local).AddTicks(8079));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1924ec9c-8faa-42a2-adc7-3589969a2590");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Dob", "PasswordHash" },
                values: new object[] { "12e83aad-054b-47c5-bb92-13249bcf7a39", new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEDF7kvuglebfaOR+p/eu2ofPx37+xRBa9+SPMmI3BQHHz8ITxPfqjgrwzsGfZZGe3g==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 10, 17, 23, 36, 44, 295, DateTimeKind.Local).AddTicks(7836));
        }
    }
}
