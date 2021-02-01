using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Quizzing.Web.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerText = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizId", "Title" },
                values: new object[,]
                {
                    { 1, "Quiz 1" },
                    { 2, "Quiz 2" },
                    { 3, "Quiz 3" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "QuestionText", "QuizId" },
                values: new object[,]
                {
                    { 1, "Question 1", 1 },
                    { 2, "Question 2", 1 },
                    { 3, "Question 3", 1 },
                    { 4, "Question 1", 2 },
                    { 5, "Question 2", 2 },
                    { 6, "Question 3", 2 },
                    { 7, "Question 1", 3 },
                    { 8, "Question 2", 3 },
                    { 9, "Question 3", 3 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "AnswerText", "IsCorrect", "QuestionId" },
                values: new object[,]
                {
                    { 1, "Answer 1", false, 1 },
                    { 21, "Answer 1", false, 6 },
                    { 22, "Answer 2", false, 6 },
                    { 23, "Answer 3", false, 6 },
                    { 24, "Answer 4", false, 6 },
                    { 25, "Answer 1", false, 7 },
                    { 26, "Answer 2", false, 7 },
                    { 20, "Answer 4", false, 5 },
                    { 27, "Answer 3", false, 7 },
                    { 29, "Answer 1", false, 8 },
                    { 30, "Answer 2", false, 8 },
                    { 31, "Answer 3", false, 8 },
                    { 32, "Answer 4", false, 8 },
                    { 33, "Answer 1", false, 9 },
                    { 34, "Answer 2", false, 9 },
                    { 28, "Answer 4", false, 7 },
                    { 19, "Answer 3", false, 5 },
                    { 18, "Answer 2", false, 5 },
                    { 17, "Answer 1", false, 5 },
                    { 2, "Answer 2", false, 1 },
                    { 3, "Answer 3", false, 1 },
                    { 4, "Answer 4", false, 1 },
                    { 5, "Answer 1", false, 2 },
                    { 6, "Answer 2", false, 2 },
                    { 7, "Answer 3", false, 2 },
                    { 8, "Answer 4", false, 2 },
                    { 9, "Answer 1", false, 3 },
                    { 10, "Answer 2", false, 3 },
                    { 11, "Answer 3", false, 3 },
                    { 12, "Answer 4", false, 3 },
                    { 13, "Answer 1", false, 4 },
                    { 14, "Answer 2", false, 4 },
                    { 15, "Answer 3", false, 4 },
                    { 16, "Answer 4", false, 4 },
                    { 35, "Answer 3", false, 9 },
                    { 36, "Answer 4", false, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            // Below code is for starting the objects Ids at 100.
            // Without this code, EF tries to add objects with Ids starting at 1 and crashes with the seeded data for test.
            // This code needs to be taken out when seeded data is not used
            migrationBuilder.RestartSequence("Quizzes_QuizId_seq", 100, "public");
            migrationBuilder.RestartSequence("Questions_QuestionId_seq", 100, "public");
            migrationBuilder.RestartSequence("Answers_AnswerId_seq", 100, "public");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
