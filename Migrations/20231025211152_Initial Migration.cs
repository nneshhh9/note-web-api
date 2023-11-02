using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NuGet.Packaging.Signing;

#nullable disable

namespace NoteWeb.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dbNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NoteContext = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    NoteReminder = table.Column<Timestamp>(type: "timestamp with time zone", nullable: true),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbNotes_dbTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "dbTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dbReminder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeReminder = table.Column<Timestamp>(type: "timestamp with time zone", nullable: false),
                    NoteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbReminder_dbNotes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "dbNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbNotes_TagsId",
                table: "dbNotes",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_dbReminder_NoteId",
                table: "dbReminder",
                column: "NoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbReminder");

            migrationBuilder.DropTable(
                name: "dbNotes");

            migrationBuilder.DropTable(
                name: "dbTags");
        }
    }
}
