using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImageURLinMembershipTypes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "file:///D:/GitHub/Fitness-App/CSharpWeb-FitnessApp/FitnessApp.Web/wwwroot/img/vip.jpg");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "file:///D:/GitHub/Fitness-App/CSharpWeb-FitnessApp/FitnessApp.Web/wwwroot/img/premium.jpg");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "file:///D:/GitHub/Fitness-App/CSharpWeb-FitnessApp/FitnessApp.Web/wwwroot/img/basic.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/wwwroot/img/basic");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/wwwroot/img/premium");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/wwwroot/img/vip");
        }
    }
}
