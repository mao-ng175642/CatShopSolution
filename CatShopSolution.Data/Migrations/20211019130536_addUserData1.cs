using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatShopSolution.Data.Migrations
{
    public partial class addUserData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e4f70cbd-eecd-48bd-8552-e636b9e136fb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Dob", "Email", "FirstName", "LastName", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "9cbb9154-6a6f-4831-91a2-1195f3ff10c8", new DateTime(2021, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "mao.ng175642@sis.hust.edu.vn", "Mao", "Nguyen", "mao.ng175642@sis.hust.edu.vn", "AQAAAAEAACcQAAAAEMqwFMoPauMVmJ46cIQlrbKO2GRJh22QQ5REiF0kIpunJcA3kLZiqcuLF50U4vqg5w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f9753389-f002-4e0f-9685-cde185c7a238");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Dob", "Email", "FirstName", "LastName", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "d646e494-d9c1-4522-a7f4-6014e7c7cb86", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "tedu.international@gmail.com", "Toan", "Bach", "tedu.international@gmail.com", "AQAAAAEAACcQAAAAEKbql3pyfZQB+tUvA6An9zVvUEH7//Wsk9XpuYKvwHHcOrXpxRrNQXMSp2e+abCxRw==" });
        }
    }
}
