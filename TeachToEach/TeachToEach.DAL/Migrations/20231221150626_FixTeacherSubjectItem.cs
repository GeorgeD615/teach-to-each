using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachToEach.DAL.Migrations
{
    public partial class FixTeacherSubjectItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeacherSubjects",
                keyColumn: "relation_id",
                keyValue: 19,
                column: "subject_id",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeacherSubjects",
                keyColumn: "relation_id",
                keyValue: 19,
                column: "subject_id",
                value: 4);
        }
    }
}
