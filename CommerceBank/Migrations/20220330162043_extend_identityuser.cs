using Microsoft.EntityFrameworkCore.Migrations;

namespace CommerceBank.Migrations
{
    public partial class extend_identityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<float>(
                name: "TotalBalance",
                table: "AspNetUsers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "AspNetUsers");
        }
    }
}
