using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace XtraSpurt.MultiDbSupport.PgSql.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "spurt");

            migrationBuilder.CreateTable(
                name: "xtrablogs",
                schema: "spurt",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrablogs", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "xtraroles",
                schema: "spurt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtraroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "xtrausers",
                schema: "spurt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrausers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "xtraposts",
                schema: "spurt",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    BlogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtraposts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_xtraposts_xtrablogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "spurt",
                        principalTable: "xtrablogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "xtraroleclaims",
                schema: "spurt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtraroleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_xtraroleclaims_xtraroles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "spurt",
                        principalTable: "xtraroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "xtrauserclaims",
                schema: "spurt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrauserclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_xtrauserclaims_xtrausers_UserId",
                        column: x => x.UserId,
                        principalSchema: "spurt",
                        principalTable: "xtrausers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "xtrauserlogins",
                schema: "spurt",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrauserlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_xtrauserlogins_xtrausers_UserId",
                        column: x => x.UserId,
                        principalSchema: "spurt",
                        principalTable: "xtrausers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "xtrauserroles",
                schema: "spurt",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrauserroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_xtrauserroles_xtraroles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "spurt",
                        principalTable: "xtraroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_xtrauserroles_xtrausers_UserId",
                        column: x => x.UserId,
                        principalSchema: "spurt",
                        principalTable: "xtrausers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "xtrausertokens",
                schema: "spurt",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xtrausertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_xtrausertokens_xtrausers_UserId",
                        column: x => x.UserId,
                        principalSchema: "spurt",
                        principalTable: "xtrausers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_xtraposts_BlogId",
                schema: "spurt",
                table: "xtraposts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_xtraroleclaims_RoleId",
                schema: "spurt",
                table: "xtraroleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "spurt",
                table: "xtraroles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_xtrauserclaims_UserId",
                schema: "spurt",
                table: "xtrauserclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_xtrauserlogins_UserId",
                schema: "spurt",
                table: "xtrauserlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_xtrauserroles_RoleId",
                schema: "spurt",
                table: "xtrauserroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "spurt",
                table: "xtrausers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "spurt",
                table: "xtrausers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "xtraposts",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtraroleclaims",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrauserclaims",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrauserlogins",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrauserroles",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrausertokens",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrablogs",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtraroles",
                schema: "spurt");

            migrationBuilder.DropTable(
                name: "xtrausers",
                schema: "spurt");
        }
    }
}
