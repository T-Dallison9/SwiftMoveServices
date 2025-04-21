using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftMoveServices.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDateAndPriceToStorageBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "StorageBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "StorageBookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "StorageBookings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StorageBookings");
        }
    }
}
