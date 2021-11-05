using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MEGASuperChatBot.Migrations
{
    public partial class fixAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandEntities",
                columns: table => new
                {
                    CommandId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommandTrigger = table.Column<string>(type: "text", nullable: true),
                    SourcesNames = table.Column<string>(type: "text", nullable: true),
                    CommandAmswer = table.Column<string>(type: "text", nullable: true),
                    CommandAuthor = table.Column<string>(type: "text", nullable: true),
                    CommandDescription = table.Column<string>(type: "text", nullable: true),
                    CommandCreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsScript = table.Column<bool>(type: "boolean", nullable: false),
                    ScriptName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandEntities", x => x.CommandId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandEntities");
        }
    }
}
