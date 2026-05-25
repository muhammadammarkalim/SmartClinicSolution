using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class CompleteClinicSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyID",
                keyValue: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtyID", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "??", "General Physician" },
                    { 2, "??", "Cardiology" },
                    { 3, "??", "Neurology" },
                    { 4, "??", "Dentistry" },
                    { 5, "??", "Orthopedics" },
                    { 6, "??", "Pediatrics" },
                    { 7, "??", "Dermatology" },
                    { 8, "??", "ENT" },
                    { 9, "???", "Ophthalmology" },
                    { 10, "??", "Psychiatry" }
                });
        }
    }
}
