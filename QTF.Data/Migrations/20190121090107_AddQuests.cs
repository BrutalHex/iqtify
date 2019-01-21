using Microsoft.EntityFrameworkCore.Migrations;

namespace QTF.Data.Migrations
{
    public partial class AddQuests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_AspNetUsers_CreatorId",
                table: "Quest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecord_Quest_QuestId",
                table: "QuestRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestTasks_Quest_QuestId",
                table: "QuestTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quest",
                table: "Quest");

            migrationBuilder.RenameTable(
                name: "Quest",
                newName: "Quests");

            migrationBuilder.RenameIndex(
                name: "IX_Quest_CreatorId",
                table: "Quests",
                newName: "IX_Quests_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quests",
                table: "Quests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecord_Quests_QuestId",
                table: "QuestRecord",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_AspNetUsers_CreatorId",
                table: "Quests",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestTasks_Quests_QuestId",
                table: "QuestTasks",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecord_Quests_QuestId",
                table: "QuestRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_AspNetUsers_CreatorId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestTasks_Quests_QuestId",
                table: "QuestTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quests",
                table: "Quests");

            migrationBuilder.RenameTable(
                name: "Quests",
                newName: "Quest");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_CreatorId",
                table: "Quest",
                newName: "IX_Quest_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quest",
                table: "Quest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_AspNetUsers_CreatorId",
                table: "Quest",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecord_Quest_QuestId",
                table: "QuestRecord",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestTasks_Quest_QuestId",
                table: "QuestTasks",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
