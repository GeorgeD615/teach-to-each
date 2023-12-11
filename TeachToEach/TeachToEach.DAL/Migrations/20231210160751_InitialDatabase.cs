using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachToEach.DAL.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "StatusOfRelations",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOfRelations", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.subject_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    last_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "character varying(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.CheckConstraint("age", "age > 7 AND age < 121");
                    table.ForeignKey(
                        name: "FK_Users_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStudentRelation",
                columns: table => new
                {
                    relation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teacher_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudentRelation", x => x.relation_id);
                    table.CheckConstraint("teacher_id", "teacher_id <> student_id");
                    table.ForeignKey(
                        name: "FK_TeacherStudentRelation_StatusOfRelations_status_id",
                        column: x => x.status_id,
                        principalTable: "StatusOfRelations",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudentRelation_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudentRelation_Users_student_id",
                        column: x => x.student_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudentRelation_Users_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    homework_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    relation_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", maxLength: 1000, nullable: false),
                    deadline = table.Column<string>(type: "text", nullable: true),
                    solution_time = table.Column<string>(type: "text", nullable: false),
                    is_completed = table.Column<bool>(type: "bool", nullable: false, defaultValue: false),
                    solution = table.Column<string>(type: "text", nullable: true),
                    teacher_comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.homework_id);
                    table.ForeignKey(
                        name: "FK_Homeworks_TeacherStudentRelation_relation_id",
                        column: x => x.relation_id,
                        principalTable: "TeacherStudentRelation",
                        principalColumn: "relation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    rating_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<short>(type: "smallint", nullable: false),
                    review = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    relation_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.rating_id);
                    table.CheckConstraint("value", "value > 0 AND value < 6");
                    table.ForeignKey(
                        name: "FK_Ratings_TeacherStudentRelation_relation_id",
                        column: x => x.relation_id,
                        principalTable: "TeacherStudentRelation",
                        principalColumn: "relation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "role_id", "name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Moderator" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "StatusOfRelations",
                columns: new[] { "status_id", "name" },
                values: new object[,]
                {
                    { 1, "Заявка на рассмотрении" },
                    { 2, "Заявка принята" },
                    { 3, "Заявка отклонена" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "subject_id", "name" },
                values: new object[,]
                {
                    { 1, "Математика" },
                    { 2, "Литература" },
                    { 3, "Биология" },
                    { 4, "История" },
                    { 5, "Обществознание" },
                    { 6, "Английский язык" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "age", "email", "first_name", "last_name", "login", "password", "role_id" },
                values: new object[] { 1, (short)20, "g.davlyatshin@gmail.com", "Георгий", "Давлятшин", "davlik2003", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_relation_id",
                table: "Homeworks",
                column: "relation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_relation_id",
                table: "Ratings",
                column: "relation_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudentRelation_status_id",
                table: "TeacherStudentRelation",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudentRelation_student_id",
                table: "TeacherStudentRelation",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudentRelation_subject_id",
                table: "TeacherStudentRelation",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudentRelation_teacher_id",
                table: "TeacherStudentRelation",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "TeacherStudentRelation");

            migrationBuilder.DropTable(
                name: "StatusOfRelations");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
