using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class TreatmentDayMadeNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDay",
                table: "SpaProcedures",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                comment: "Appointment day for the spa service",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldComment: "Appointment day for the spa service");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDay",
                table: "SpaProcedures",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                comment: "Appointment day for the spa service",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldComment: "Appointment day for the spa service");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TreatmentDay",
                value: "Saturday");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "TreatmentDay",
                value: "Sunday");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3,
                column: "TreatmentDay",
                value: "Saturday");
        }
    }
}
