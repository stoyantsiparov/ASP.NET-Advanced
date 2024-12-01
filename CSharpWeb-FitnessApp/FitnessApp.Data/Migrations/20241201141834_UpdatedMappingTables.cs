using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMappingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_FitnessEvents_FitnessEventId",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_FitnessEventId",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "FitnessEventId",
                table: "EventRegistrations");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Duration", "ImageUrl", "Schedule" },
                values: new object[] { "A fun and energetic Zumba dance class for all levels.", 60, "https://i.ytimg.com/vi/N3wBXogMYfM/hq720.jpg?sqp=-oaymwE7CK4FEIIDSFryq4qpAy0IARUAAAAAGAElAADIQj0AgKJD8AEB-AH-CYAC0AWKAgwIABABGGUgUihUMA8=&rs=AOn4CLD9yvCPKa7mHvL_lLUQr-TvnlNYRw", new DateTime(2024, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A basic membership that grants access to all regular classes and gym facilities.", 30, "https://i0.wp.com/poolstats.co/wp-content/uploads/2019/01/Basic-Membership.png?fit=400%2C327&ssl=1", "Basic", 59.99m },
                    { 2, "An elite membership offering access to all classes, gym facilities, and spa treatments.", 60, "https://cdn.vectorstock.com/i/500p/49/16/elite-gold-label-vector-2944916.jpg", "Elite", 99.99m },
                    { 3, "A premium membership offering access to all classes, gym facilities, and spa treatments.", 180, "https://thumbs.dreamstime.com/b/premium-membership-badge-stamp-golden-red-ribbon-text-30827692.jpg", "Premium", 299.99m },
                    { 4, "An exclusive membership with additional perks including priority booking for events and personal training.", 365, "https://cdn11.bigcommerce.com/s-2ooutu2zpl/images/stencil/original/products/35315/51564/VIP_Badge_2__62906.1641934958.png?c=2", "VIP", 499.99m }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_FitnessEvents_EventId",
                table: "EventRegistrations",
                column: "EventId",
                principalTable: "FitnessEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_FitnessEvents_EventId",
                table: "EventRegistrations");

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "FitnessEventId",
                table: "EventRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Duration", "ImageUrl", "Schedule" },
                values: new object[] { "A fun and energetic dance workout to get your heart pumping.", 45, "https://fpt.vn/wp-content/uploads/2022/09/zumba.jpg", new DateTime(2024, 12, 6, 19, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_FitnessEventId",
                table: "EventRegistrations",
                column: "FitnessEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_FitnessEvents_FitnessEventId",
                table: "EventRegistrations",
                column: "FitnessEventId",
                principalTable: "FitnessEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
