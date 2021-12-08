using Microsoft.EntityFrameworkCore.Migrations;

namespace MEGASuperChatBot.Migrations
{
    public partial class AddedUiAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScriptText",
                table: "CommandEntities",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScriptText",
                table: "CommandEntities");
        }
    }
}
