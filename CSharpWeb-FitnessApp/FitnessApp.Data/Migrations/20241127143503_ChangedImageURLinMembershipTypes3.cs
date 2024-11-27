using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImageURLinMembershipTypes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://i0.wp.com/poolstats.co/wp-content/uploads/2019/01/Basic-Membership.png?fit=400%2C327&ssl=1");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://thumbs.dreamstime.com/b/premium-membership-badge-stamp-golden-red-ribbon-text-30827692.jpg");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://cdn11.bigcommerce.com/s-2ooutu2zpl/images/stencil/original/products/35315/51564/VIP_Badge_2__62906.1641934958.png?c=2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
