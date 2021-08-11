using Microsoft.EntityFrameworkCore.Migrations;

namespace IBCustomerSite.Migrations
{
    public partial class AddFailedStateBillPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFailed",
                table: "BillPays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFailed",
                table: "BillPays");
        }
    }
}
