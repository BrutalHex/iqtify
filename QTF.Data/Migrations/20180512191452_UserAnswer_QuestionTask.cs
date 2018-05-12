using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QTF.Web.Data.Migrations
{
    public partial class UserAnswer_QuestionTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAnswer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                table: "UserAnswer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtfTaskId",
                table: "UserAnswer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_QtfTaskId",
                table: "UserAnswer",
                column: "QtfTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_QtfTasks_QtfTaskId",
                table: "UserAnswer",
                column: "QtfTaskId",
                principalTable: "QtfTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_QtfTasks_QtfTaskId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_QtfTaskId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "AnswerType",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "QtfTaskId",
                table: "UserAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAnswer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
