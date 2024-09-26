using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdCreative.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordAdd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountWord = table.Column<int>(type: "int", nullable: true),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAdd", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WordAdd",
                columns: new[] { "Id", "CountWord", "UniqueId", "Word" },
                values: new object[,]
                {
                    { 1, 7, "47b63467-7d4d-48e7-b81b-8fcd4d8b183e", "AbCdEfG" },
                    { 2, 12, "6be64795-9d99-4765-ac7a-6ed15aba97c0", "Test2AbCdEfG" },
                    { 3, 13, "c2f8bbce-2eee-4f3a-887b-97b2ea11dd7c", "TeStUcAbCdEfG" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordAdd");
        }
    }
}
