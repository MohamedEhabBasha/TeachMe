using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfile.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserProfiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserProfiles");
        }
    }
}
