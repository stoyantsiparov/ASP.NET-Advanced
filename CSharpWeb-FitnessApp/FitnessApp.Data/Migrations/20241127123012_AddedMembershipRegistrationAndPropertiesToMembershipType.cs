using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMembershipRegistrationAndPropertiesToMembershipType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MembershipTypes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                comment: "Description of the membership type");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MembershipTypes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Image URL of the membership type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Members",
                type: "datetime2",
                nullable: false,
                comment: "Date when the member joined the gym",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date whn the member joined the gym");

            migrationBuilder.CreateTable(
                name: "MembershipRegistrations",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false),
                    MemberId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRegistrations", x => new { x.MembershipTypeId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MembershipRegistrations_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipRegistrations_Members_MemberId1",
                        column: x => x.MemberId1,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MembershipRegistrations_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MembershipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRegistrations_MemberId",
                table: "MembershipRegistrations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRegistrations_MemberId1",
                table: "MembershipRegistrations",
                column: "MemberId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipRegistrations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MembershipTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Members",
                type: "datetime2",
                nullable: false,
                comment: "Date whn the member joined the gym",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date when the member joined the gym");

            migrationBuilder.AddColumn<int>(
                name: "MembershipTypeId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Membership type of the member");

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId",
                principalTable: "MembershipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
