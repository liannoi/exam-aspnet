using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam.Persistence.Migrations
{
    public partial class Voting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Voting",
                table => new
                {
                    VotingId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 512)
                },
                constraints: table => { table.PrimaryKey("PK_Voting", x => x.VotingId); });

            migrationBuilder.CreateTable(
                "VotingPolle",
                table => new
                {
                    VotingPolleId = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_VotingPolle", x => x.VotingPolleId); });

            migrationBuilder.CreateTable(
                "VotingAnswers",
                table => new
                {
                    VotingAnswerId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VotingId = table.Column<int>(),
                    Text = table.Column<string>(maxLength: 512)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingAnswers", x => x.VotingAnswerId);
                    table.ForeignKey(
                        "FK_VotingAnswers_VotingId",
                        x => x.VotingId,
                        "Voting",
                        "VotingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "VotingPolles",
                table => new
                {
                    VotingPolleId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VotingId = table.Column<int>(),
                    PolleId = table.Column<int>(),
                    VotingAnswerId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingPolles", x => x.VotingPolleId);
                    table.ForeignKey(
                        "FK_VotingPolles_PolleId",
                        x => x.PolleId,
                        "VotingPolle",
                        "VotingPolleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_VotingPolles_VotingAnswerId",
                        x => x.VotingAnswerId,
                        "VotingAnswers",
                        "VotingAnswerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_VotingPolles_VotingId",
                        x => x.VotingId,
                        "Voting",
                        "VotingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "UNQ_Voting_Name",
                "Voting",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_VotingAnswers_VotingId",
                "VotingAnswers",
                "VotingId");

            migrationBuilder.CreateIndex(
                "IX_VotingPolles_PolleId",
                "VotingPolles",
                "PolleId");

            migrationBuilder.CreateIndex(
                "IX_VotingPolles_VotingAnswerId",
                "VotingPolles",
                "VotingAnswerId");

            migrationBuilder.CreateIndex(
                "IX_VotingPolles_VotingId",
                "VotingPolles",
                "VotingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "VotingPolles");

            migrationBuilder.DropTable(
                "VotingPolle");

            migrationBuilder.DropTable(
                "VotingAnswers");

            migrationBuilder.DropTable(
                "Voting");
        }
    }
}