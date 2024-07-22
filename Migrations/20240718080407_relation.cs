using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementApplication.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Tasks_TasksId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Tasks_TasksId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_TasksId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Document_TasksId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "Document");

            migrationBuilder.CreateIndex(
                name: "IX_Note_TaskId",
                table: "Note",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_TaskId",
                table: "Document",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Tasks_TaskId",
                table: "Document",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Tasks_TaskId",
                table: "Note",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Tasks_TaskId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Tasks_TaskId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_TaskId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Document_TaskId",
                table: "Document");

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "Note",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_TasksId",
                table: "Note",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_TasksId",
                table: "Document",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Tasks_TasksId",
                table: "Document",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Tasks_TasksId",
                table: "Note",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
