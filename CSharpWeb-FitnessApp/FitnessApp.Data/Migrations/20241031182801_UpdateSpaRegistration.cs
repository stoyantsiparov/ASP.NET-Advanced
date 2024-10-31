using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpaRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDay",
                table: "SpaRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Appointment day for the spa service",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Appointment day for the spa service");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations",
                columns: new[] { "SpaProcedureId", "MemberId", "TreatmentDay" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDay",
                table: "SpaRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Appointment day for the spa service",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Appointment day for the spa service");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaRegistrations",
                table: "SpaRegistrations",
                columns: new[] { "SpaProcedureId", "MemberId" });
        }
    }
}
