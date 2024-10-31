using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSpaRegistratitonMemberPropToIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId",
                table: "SpaRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "SpaRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "SpaRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations",
                columns: new[] { "MemberId", "SpaProcedureId" });

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.dshieldsusa.com/wp-content/uploads/2021/05/relaxing-massage-slide.jpg");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://spamd.net/wp-content/uploads/2022/03/medications-facial-treatments.jpg");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://elementsmassage.com/files/shared/AZ%20-%20Elements%20Massage%205-1864269.jpg");

            migrationBuilder.CreateIndex(
                name: "IX_SpaRegistrations_MemberId1",
                table: "SpaRegistrations",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_AspNetUsers_MemberId",
                table: "SpaRegistrations",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId1",
                table: "SpaRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_AspNetUsers_MemberId",
                table: "SpaRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_SpaRegistrations_MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "SpaRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations",
                columns: new[] { "MemberId", "SpaProcedureId" });

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/relaxing-massage.jpg");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/facial-treatment.jpg");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://example.com/images/aromatherapy.jpg");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId",
                table: "SpaRegistrations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}