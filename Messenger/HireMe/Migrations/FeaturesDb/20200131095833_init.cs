using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HireMe.Web.Migrations.FeaturesDb
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    dateTime = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true),
                    isRead = table.Column<bool>(nullable: false),
                    isImportant = table.Column<bool>(nullable: false),
                    isStared = table.Column<bool>(nullable: false),
                    deletedFromSender = table.Column<bool>(nullable: false),
                    deletedFromReceiver = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
