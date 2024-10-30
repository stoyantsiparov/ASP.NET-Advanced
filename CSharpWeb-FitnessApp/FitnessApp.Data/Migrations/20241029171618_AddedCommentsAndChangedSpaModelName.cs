using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentsAndChangedSpaModelName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_SpaTypes_SpaTypeId",
                table: "SpaRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaTypes",
                table: "SpaTypes");

            migrationBuilder.RenameTable(
                name: "SpaTypes",
                newName: "SpaProcedures");

            migrationBuilder.RenameColumn(
                name: "SpaTypeId",
                table: "SpaRegistrations",
                newName: "SpaProcedureId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "SpaRegistrations",
                type: "datetime2",
                nullable: false,
                comment: "Appointment date for the spa service",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MembershipTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "Price of the membership type",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MembershipTypes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Membership type",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "MembershipTypes",
                type: "int",
                nullable: false,
                comment: "Duration of the membership type in days",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MembershipTypes",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Members",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "Phone number of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "MembershipTypeId",
                table: "Members",
                type: "int",
                nullable: false,
                comment: "Membership type of the member",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Members",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Last name of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Members",
                type: "datetime2",
                nullable: false,
                comment: "Date whn the member joined the gym",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "First name of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Email of the member",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Specialization of the fitness instructor",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Last name of the fitness instructor",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "First name of the fitness instructor",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Instructors",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FitnessEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Title of the fitness event",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FitnessEvents",
                type: "datetime2",
                nullable: false,
                comment: "Start date of the fitness event",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "FitnessEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Location of the fitness event",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FitnessEvents",
                type: "datetime2",
                nullable: false,
                comment: "End date of the fitness event",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FitnessEvents",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                comment: "Description of the fitness event",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FitnessEvents",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                comment: "Date and time of the fitness class",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Name of the fitness class",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Classes",
                type: "int",
                nullable: false,
                comment: "Instructor of the fitness class",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Classes",
                type: "int",
                nullable: false,
                comment: "Duration of the fitness class in minutes",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Classes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                comment: "Description of the fitness class",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Classes",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SpaProcedures",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "Price of the spa procedure",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpaProcedures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Spa type",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "SpaProcedures",
                type: "int",
                nullable: false,
                comment: "Duration of the spa procedure in minutes",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SpaProcedures",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                comment: "Description of the spa procedure",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SpaProcedures",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaProcedures",
                table: "SpaProcedures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_SpaProcedures_SpaProcedureId",
                table: "SpaRegistrations",
                column: "SpaProcedureId",
                principalTable: "SpaProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaRegistrations_SpaProcedures_SpaProcedureId",
                table: "SpaRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaProcedures",
                table: "SpaProcedures");

            migrationBuilder.RenameTable(
                name: "SpaProcedures",
                newName: "SpaTypes");

            migrationBuilder.RenameColumn(
                name: "SpaProcedureId",
                table: "SpaRegistrations",
                newName: "SpaTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "SpaRegistrations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Appointment date for the spa service");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MembershipTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "Price of the membership type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MembershipTypes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Membership type");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "MembershipTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Duration of the membership type in days");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MembershipTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Members",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComment: "Phone number of the member");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipTypeId",
                table: "Members",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Membership type of the member");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Members",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Last name of the member");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Members",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date whn the member joined the gym");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "First name of the member");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Email of the member");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Members",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Specialization of the fitness instructor");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Last name of the fitness instructor");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Instructors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "First name of the fitness instructor");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Instructors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FitnessEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Title of the fitness event");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FitnessEvents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Start date of the fitness event");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "FitnessEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Location of the fitness event");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FitnessEvents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "End date of the fitness event");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FitnessEvents",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldComment: "Description of the fitness event");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FitnessEvents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date and time of the fitness class");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Name of the fitness class");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Instructor of the fitness class");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Duration of the fitness class in minutes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Classes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldComment: "Description of the fitness class");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SpaTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "Price of the spa procedure");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpaTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Spa type");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "SpaTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Duration of the spa procedure in minutes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SpaTypes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldComment: "Description of the spa procedure");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SpaTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaTypes",
                table: "SpaTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaRegistrations_SpaTypes_SpaTypeId",
                table: "SpaRegistrations",
                column: "SpaTypeId",
                principalTable: "SpaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
