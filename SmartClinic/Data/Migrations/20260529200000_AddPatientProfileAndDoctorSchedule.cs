using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Migrations
{
    public partial class AddPatientProfileAndDoctorSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Patient profile columns on AspNetUsers
            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPhone",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            // Doctor schedule columns
            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Doctors",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalAffiliation",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "FullTime");

            migrationBuilder.AddColumn<string>(
                name: "WorkingDays",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Mon,Tue,Wed,Thu,Fri");

            migrationBuilder.AddColumn<string>(
                name: "ShiftStart",
                table: "Doctors",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                defaultValue: "09:00");

            migrationBuilder.AddColumn<string>(
                name: "ShiftEnd",
                table: "Doctors",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                defaultValue: "17:00");

            migrationBuilder.AddColumn<int>(
                name: "SlotDurationMinutes",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "BloodGroup", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "Address", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "DateOfBirth", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "Gender", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "ProfileImage", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "EmergencyContact", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "EmergencyPhone", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "Experience", table: "Doctors");
            migrationBuilder.DropColumn(name: "Languages", table: "Doctors");
            migrationBuilder.DropColumn(name: "HospitalAffiliation", table: "Doctors");
            migrationBuilder.DropColumn(name: "LicenseNumber", table: "Doctors");
            migrationBuilder.DropColumn(name: "EmploymentType", table: "Doctors");
            migrationBuilder.DropColumn(name: "WorkingDays", table: "Doctors");
            migrationBuilder.DropColumn(name: "ShiftStart", table: "Doctors");
            migrationBuilder.DropColumn(name: "ShiftEnd", table: "Doctors");
            migrationBuilder.DropColumn(name: "SlotDurationMinutes", table: "Doctors");
        }
    }
}
