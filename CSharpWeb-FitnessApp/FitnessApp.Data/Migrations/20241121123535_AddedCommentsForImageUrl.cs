using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentsForImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "SpaProcedures",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Image URL of the spa procedure",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "FitnessEvents",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Image URL of the fitness class",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Image URL of the fitness class",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "SpaProcedures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Image URL of the spa procedure");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "FitnessEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Image URL of the fitness class");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Image URL of the fitness class");
        }
    }
}
