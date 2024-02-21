using Microsoft.EntityFrameworkCore.Migrations;

namespace eVotingApp.Migrations
{
    public partial class checs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CapitalizedPartyName",
                table: "Parties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalizedPartyName",
                table: "Parties");
        }
    }
}
