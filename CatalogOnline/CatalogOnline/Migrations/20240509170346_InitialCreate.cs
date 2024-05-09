using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogOnline.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    legal_name = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
