using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEGASuperChatBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommandAmswer",
                table: "CommandEntities",
                newName: "CommandAnswer");

            migrationBuilder.AlterColumn<string>(
                name: "CommandCreateDate",
                table: "CommandEntities",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommandAnswer",
                table: "CommandEntities",
                newName: "CommandAmswer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommandCreateDate",
                table: "CommandEntities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
