using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEventAndClassRegistationMemberFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId",
                table: "ClassesRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Members_MemberId",
                table: "EventRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassesRegistrations",
                table: "ClassesRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "EventRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "EventRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "ClassesRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "ClassesRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations",
                columns: new[] { "MemberId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassesRegistrations",
                table: "ClassesRegistrations",
                columns: new[] { "MemberId", "ClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_MemberId1",
                table: "EventRegistrations",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesRegistrations_MemberId1",
                table: "ClassesRegistrations",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesRegistrations_AspNetUsers_MemberId",
                table: "ClassesRegistrations",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId1",
                table: "ClassesRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_AspNetUsers_MemberId",
                table: "EventRegistrations",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Members_MemberId1",
                table: "EventRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassesRegistrations_AspNetUsers_MemberId",
                table: "ClassesRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_AspNetUsers_MemberId",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Members_MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ClassesRegistrations_MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassesRegistrations",
                table: "ClassesRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "EventRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "ClassesRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations",
                columns: new[] { "MemberId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassesRegistrations",
                table: "ClassesRegistrations",
                columns: new[] { "MemberId", "ClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId",
                table: "ClassesRegistrations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Members_MemberId",
                table: "EventRegistrations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}