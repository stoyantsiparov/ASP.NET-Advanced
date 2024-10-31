using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpaProceduresModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatmentDay",
                table: "SpaProcedures");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentDay",
                table: "SpaRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Appointment day for the spa service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatmentDay",
                table: "SpaRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentDay",
                table: "SpaProcedures",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                comment: "Appointment day for the spa service");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TreatmentDay",
                value: null);

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "TreatmentDay",
                value: null);

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3,
                column: "TreatmentDay",
                value: null);
        }
    }
}
