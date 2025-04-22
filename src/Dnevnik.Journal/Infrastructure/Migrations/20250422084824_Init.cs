using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dnevnik.Journal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_marks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lesson_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: true),
                    mark = table.Column<string>(type: "text", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_marks", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_marks_created_at",
                table: "user_marks",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_user_marks_lesson_id",
                table: "user_marks",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_marks_subject",
                table: "user_marks",
                column: "subject");

            migrationBuilder.CreateIndex(
                name: "ix_user_marks_user_id",
                table: "user_marks",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_marks");
        }
    }
}
