using Microsoft.EntityFrameworkCore.Migrations;

namespace eVotingApp.Migrations
{
    public partial class checkdbagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Parties",
                newName: "WebsiteName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WebsiteName",
                table: "Parties",
                newName: "Website");
        }
    }
}
