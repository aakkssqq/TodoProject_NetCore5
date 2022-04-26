using Microsoft.EntityFrameworkCore.Migrations;

namespace ObserverPatternWeb.Migrations
{
    public partial class AddUserAttrAddressAndPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ObsUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "ObsUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "ObsOUs",
                type: "NVARCHAR(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "ObsOUs",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ObsUsers");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "ObsUsers");

            migrationBuilder.DropColumn(
                name: "Enable",
                table: "ObsOUs");

            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "ObsOUs",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(40)",
                oldMaxLength: 40);
        }
    }
}
