using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppointmentDateTimeToSpaProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDateTime",
                table: "SpaRegistrations");

            migrationBuilder.DropColumn(
                name: "TreatmentDay",
                table: "SpaRegistrations");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "SpaProcedures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Appointment date and time for the spa service");

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppointmentDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppointmentDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SpaProcedures",
                keyColumn: "Id",
                keyValue: 3,
                column: "AppointmentDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDateTime",
                table: "SpaProcedures");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "SpaRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Appointment date and time for the spa service");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentDay",
                table: "SpaRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Appointment day for the spa service");
        }
    }
}
