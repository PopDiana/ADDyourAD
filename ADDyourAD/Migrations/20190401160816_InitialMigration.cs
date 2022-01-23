using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADDyourAD.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID_Category = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category_Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID_User = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    First_Name = table.Column<string>(maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone_Number = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID_User);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    ID_Advertisement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Add_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Expiration_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    ID_User = table.Column<int>(nullable: false),
                    ID_Category = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.ID_Advertisement);
                    table.ForeignKey(
                        name: "FK_Advertisement_Category",
                        column: x => x.ID_Category,
                        principalTable: "Category",
                        principalColumn: "ID_Category",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisement_User",
                        column: x => x.ID_User,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID_Comment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    ID_Advertisement = table.Column<int>(nullable: false),
                    ID_User = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID_Comment);
                    table.ForeignKey(
                        name: "FK_Comment_Advertisement",
                        column: x => x.ID_Advertisement,
                        principalTable: "Advertisement",
                        principalColumn: "ID_Advertisement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_User",
                        column: x => x.ID_User,
                        principalTable: "User",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_ID_Category",
                table: "Advertisement",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_ID_User",
                table: "Advertisement",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_Category",
                table: "Category",
                column: "Category_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ID_Advertisement",
                table: "Comment",
                column: "ID_Advertisement");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ID_User",
                table: "Comment",
                column: "ID_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
