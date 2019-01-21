using Microsoft.EntityFrameworkCore.Migrations;

namespace QTF.Data.Migrations
{
    public partial class UniqueInternalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecord_Quests_QuestId",
                table: "QuestRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecord_AspNetUsers_UserId",
                table: "QuestRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_QuestTasks_QuestTaskId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestRecord",
                table: "QuestRecord");

            migrationBuilder.RenameTable(
                name: "UserAnswer",
                newName: "UserAnswers");

            migrationBuilder.RenameTable(
                name: "QuestRecord",
                newName: "QuestRecords");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_QuestTaskId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuestTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestRecord_UserId",
                table: "QuestRecords",
                newName: "IX_QuestRecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestRecord_QuestId",
                table: "QuestRecords",
                newName: "IX_QuestRecords_QuestId");

            migrationBuilder.AlterColumn<string>(
                name: "InternalName",
                table: "Quests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Quests_InternalName",
                table: "Quests",
                column: "InternalName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestRecords",
                table: "QuestRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecords_Quests_QuestId",
                table: "QuestRecords",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecords_AspNetUsers_UserId",
                table: "QuestRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuestTasks_QuestTaskId",
                table: "UserAnswers",
                column: "QuestTaskId",
                principalTable: "QuestTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_AspNetUsers_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecords_Quests_QuestId",
                table: "QuestRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestRecords_AspNetUsers_UserId",
                table: "QuestRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuestTasks_QuestTaskId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_AspNetUsers_UserId",
                table: "UserAnswers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Quests_InternalName",
                table: "Quests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestRecords",
                table: "QuestRecords");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "UserAnswer");

            migrationBuilder.RenameTable(
                name: "QuestRecords",
                newName: "QuestRecord");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_UserId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuestTaskId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_QuestTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestRecords_UserId",
                table: "QuestRecord",
                newName: "IX_QuestRecord_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestRecords_QuestId",
                table: "QuestRecord",
                newName: "IX_QuestRecord_QuestId");

            migrationBuilder.AlterColumn<string>(
                name: "InternalName",
                table: "Quests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestRecord",
                table: "QuestRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecord_Quests_QuestId",
                table: "QuestRecord",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestRecord_AspNetUsers_UserId",
                table: "QuestRecord",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_QuestTasks_QuestTaskId",
                table: "UserAnswer",
                column: "QuestTaskId",
                principalTable: "QuestTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
