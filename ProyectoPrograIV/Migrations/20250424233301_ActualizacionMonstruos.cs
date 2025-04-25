using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPrograIV.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionMonstruos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AlterColumn<string>(
           name: "MonsterType2",
           table: "Monsters",
           type: "nvarchar(max)",
           nullable: true,
           oldClrType: typeof(string),
           oldType: "nvarchar(max)",
           oldNullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "MonsterType2",
            table: "Monsters",
            type: "nvarchar(max)",
            nullable: false, 
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
        }
    }
}
