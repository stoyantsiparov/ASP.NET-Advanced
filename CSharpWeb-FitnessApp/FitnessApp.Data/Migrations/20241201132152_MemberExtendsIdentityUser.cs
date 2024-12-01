using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MemberExtendsIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Members_MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_MembershipRegistrations_Members_MemberId1",
                table: "MembershipRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropIndex(
                name: "IX_SpaRegistrations_MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_MembershipRegistrations_MemberId1",
                table: "MembershipRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ClassesRegistrations_MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "SpaRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "MembershipRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "ClassesRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "First name of the member");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "Last name of the member");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "Phone number of the member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "SpaRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "MembershipRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "EventRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "ClassesRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Email of the member"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "First name of the member"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Last name of the member"),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Phone number of the member")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaRegistrations_MemberId1",
                table: "SpaRegistrations",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRegistrations_MemberId1",
                table: "MembershipRegistrations",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_MemberId1",
                table: "EventRegistrations",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesRegistrations_MemberId1",
                table: "ClassesRegistrations",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesRegistrations_Members_MemberId1",
                table: "ClassesRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Members_MemberId1",
                table: "EventRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipRegistrations_Members_MemberId1",
                table: "MembershipRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_Members_MemberId1",
                table: "SpaRegistrations",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
