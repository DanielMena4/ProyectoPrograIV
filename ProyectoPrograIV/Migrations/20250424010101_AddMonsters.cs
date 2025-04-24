using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPrograIV.Migrations
{
    /// <inheritdoc />
    public partial class AddMonsters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonsterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonsterType1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonsterType2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonsterAttack = table.Column<int>(type: "int", nullable: false),
                    MonsterSpecialAttack = table.Column<int>(type: "int", nullable: false),
                    MonsterDefense = table.Column<int>(type: "int", nullable: false),
                    MonsterSpecialDefense = table.Column<int>(type: "int", nullable: false),
                    MonsterSpeed = table.Column<int>(type: "int", nullable: false),
                    MonsterHealth = table.Column<int>(type: "int", nullable: false),
                    MonsterCurrentHealth = table.Column<int>(type: "int", nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monsters_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_TeamId",
                table: "Monsters",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
