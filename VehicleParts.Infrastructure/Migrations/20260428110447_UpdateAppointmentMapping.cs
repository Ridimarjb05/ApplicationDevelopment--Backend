using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleParts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_CustomerDetails_CustomerDetailCustomerID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_StaffDetails_StaffDetailStaffID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CustomerDetailCustomerID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StaffDetailStaffID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CustomerDetailCustomerID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StaffDetailStaffID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "StaffID",
                table: "Appointments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerID",
                table: "Appointments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StaffID",
                table: "Appointments",
                column: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_CustomerDetails_CustomerID",
                table: "Appointments",
                column: "CustomerID",
                principalTable: "CustomerDetails",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_StaffDetails_StaffID",
                table: "Appointments",
                column: "StaffID",
                principalTable: "StaffDetails",
                principalColumn: "StaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_CustomerDetails_CustomerID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_StaffDetails_StaffID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CustomerID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StaffID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "StaffID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerDetailCustomerID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StaffDetailStaffID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerDetailCustomerID",
                table: "Appointments",
                column: "CustomerDetailCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StaffDetailStaffID",
                table: "Appointments",
                column: "StaffDetailStaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_CustomerDetails_CustomerDetailCustomerID",
                table: "Appointments",
                column: "CustomerDetailCustomerID",
                principalTable: "CustomerDetails",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_StaffDetails_StaffDetailStaffID",
                table: "Appointments",
                column: "StaffDetailStaffID",
                principalTable: "StaffDetails",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
