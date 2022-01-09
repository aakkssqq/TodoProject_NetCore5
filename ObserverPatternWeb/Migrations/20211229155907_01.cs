using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ObserverPatternWeb.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObsOUMembers",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OUID = table.Column<int>(type: "INT", nullable: false),
                    UserID = table.Column<int>(type: "INT", nullable: false),
                    Default = table.Column<bool>(type: "BIT", nullable: false),
                    LeaderTitle = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Level = table.Column<int>(type: "INT", nullable: false),
                    OrderIndex = table.Column<int>(type: "INT", nullable: false),
                    GUID = table.Column<string>(type: "NVARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsOUMembers", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "ObsOUs",
                columns: table => new
                {
                    OUID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OUCode = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    OUName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Level = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: true),
                    ParentOUID = table.Column<int>(type: "INT", nullable: false),
                    GUID = table.Column<string>(type: "NVARCHAR", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsOUs", x => x.OUID);
                });

            migrationBuilder.CreateTable(
                name: "ObsRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObsUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObsRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsRoleClaims_ObsRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ObsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsUserClaims_ObsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ObsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ObsUserLogins_ObsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ObsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ObsUserRoles_ObsRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ObsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObsUserRoles_ObsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ObsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ObsUserTokens_ObsUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ObsUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObsRoleClaims_RoleId",
                table: "ObsRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ObsRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ObsUserClaims_UserId",
                table: "ObsUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsUserLogins_UserId",
                table: "ObsUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsUserRoles_RoleId",
                table: "ObsUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ObsUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ObsUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObsOUMembers");

            migrationBuilder.DropTable(
                name: "ObsOUs");

            migrationBuilder.DropTable(
                name: "ObsRoleClaims");

            migrationBuilder.DropTable(
                name: "ObsUserClaims");

            migrationBuilder.DropTable(
                name: "ObsUserLogins");

            migrationBuilder.DropTable(
                name: "ObsUserRoles");

            migrationBuilder.DropTable(
                name: "ObsUserTokens");

            migrationBuilder.DropTable(
                name: "ObsRoles");

            migrationBuilder.DropTable(
                name: "ObsUsers");
        }
    }
}
