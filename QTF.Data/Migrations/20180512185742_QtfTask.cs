using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QTF.Web.Data.Migrations
{
    public partial class QtfTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskQuizRecord_QuestTask_QuestTaskId",
                table: "TaskQuizRecord");

            migrationBuilder.DropTable(
                name: "QuestTask");

            migrationBuilder.DropIndex(
                name: "IX_TaskQuizRecord_QuestTaskId",
                table: "TaskQuizRecord");

            migrationBuilder.AddColumn<int>(
                name: "QtfTaskId",
                table: "TaskQuizRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QtfTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    InternalName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    QuestId = table.Column<int>(nullable: true),
                    TaskType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QtfTasks", x => x.Id);
                    table.UniqueConstraint("AK_QtfTasks_InternalName", x => x.InternalName);
                    table.ForeignKey(
                        name: "FK_QtfTasks_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskQuizRecord_QtfTaskId",
                table: "TaskQuizRecord",
                column: "QtfTaskId",
                unique: true,
                filter: "[QtfTaskId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QtfTasks_QuestId",
                table: "QtfTasks",
                column: "QuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskQuizRecord_QtfTasks_QtfTaskId",
                table: "TaskQuizRecord",
                column: "QtfTaskId",
                principalTable: "QtfTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskQuizRecord_QtfTasks_QtfTaskId",
                table: "TaskQuizRecord");

            migrationBuilder.DropTable(
                name: "QtfTasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskQuizRecord_QtfTaskId",
                table: "TaskQuizRecord");

            migrationBuilder.DropColumn(
                name: "QtfTaskId",
                table: "TaskQuizRecord");

            migrationBuilder.CreateTable(
                name: "QuestTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    QuestId = table.Column<int>(nullable: true),
                    TaskType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestTask_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskQuizRecord_QuestTaskId",
                table: "TaskQuizRecord",
                column: "QuestTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestTask_QuestId",
                table: "QuestTask",
                column: "QuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskQuizRecord_QuestTask_QuestTaskId",
                table: "TaskQuizRecord",
                column: "QuestTaskId",
                principalTable: "QuestTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
