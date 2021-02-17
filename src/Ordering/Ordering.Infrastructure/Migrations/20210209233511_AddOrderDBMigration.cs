using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ordering.Infrastructure.Migrations
{
    public partial class AddOrderDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>("text", nullable: true),
                    TotalPrice = table.Column<decimal>("numeric", nullable: false),
                    FirstName = table.Column<string>("text", nullable: true),
                    LastName = table.Column<string>("text", nullable: true),
                    EmailAddress = table.Column<string>("text", nullable: true),
                    AddressLine = table.Column<string>("text", nullable: true),
                    Country = table.Column<string>("text", nullable: true),
                    State = table.Column<string>("text", nullable: true),
                    ZipCode = table.Column<string>("text", nullable: true),
                    CardName = table.Column<string>("text", nullable: true),
                    CardNumber = table.Column<string>("text", nullable: true),
                    Expiration = table.Column<string>("text", nullable: true),
                    CVV = table.Column<string>("text", nullable: true),
                    PaymentMethod = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Orders", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Orders");
        }
    }
}