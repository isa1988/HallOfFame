using Microsoft.EntityFrameworkCore.Migrations;

namespace HallOfFame.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurName = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillOfLevels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<long>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillOfLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillOfLevels_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillOfPeoples",
                columns: table => new
                {
                    SkillOfLevelId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillOfPeoples", x => new { x.PersonId, x.SkillOfLevelId });
                    table.ForeignKey(
                        name: "FK_SkillOfPeoples_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillOfPeoples_SkillOfLevels_SkillOfLevelId",
                        column: x => x.SkillOfLevelId,
                        principalTable: "SkillOfLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillOfLevels_SkillId",
                table: "SkillOfLevels",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillOfPeoples_SkillOfLevelId",
                table: "SkillOfPeoples",
                column: "SkillOfLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillOfPeoples");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "SkillOfLevels");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
