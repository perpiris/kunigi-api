using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentGames",
                columns: table => new
                {
                    ParentGameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    MainTitle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SubTitle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProfileImagePath = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentGames", x => x.ParentGameId);
                    table.ForeignKey(
                        name: "FK_ParentGames_Teams_HostId",
                        column: x => x.HostId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentGames_Teams_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentGames_HostId",
                table: "ParentGames",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentGames_WinnerId",
                table: "ParentGames",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentGames");
        }
    }
}
