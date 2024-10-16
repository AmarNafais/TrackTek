using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class added_registration_email_template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Name", "Subject", "Body" },
                values: new object[,]
                {
                    {
                        "RegistrationSuccess",
                        "Welcome to Our Platform",
                        "Hello {Name},\n\nYour account has been successfully created. Below are your login details:\n\nUsername: {Email}\nPassword: {Password}\n\nPlease change your password after logging in.\n\nBest Regards,\nThe Team"
                    }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Name",
                keyValue: "RegistrationSuccess");
        }
    }
}