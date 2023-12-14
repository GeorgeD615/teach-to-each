using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeachToEach.DAL.Migrations
{
    public partial class InitDatabase : Migration
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
                    email = table.Column<string>(type: "text", nullable: true),
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
                name: "TeacherSubjects",
                columns: table => new
                {
                    relation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teacher_id = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.relation_id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Users_teacher_id",
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
                    deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    solution_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    { 6, "Английский язык" },
                    { 7, "Информатика" },
                    { 8, "Музыка" },
                    { 9, "Физика" },
                    { 10, "Рисование" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "age", "email", "first_name", "last_name", "login", "password", "role_id" },
                values: new object[,]
                {
                    { 1, (short)20, "g.davlyatshin@gmail.com", "Георгий", "Давлятшин", "davlik2003", "1f451538ece752d6f41d727e536bb2686663ee6054caef362a5f588b3d0a35c0", 3 },
                    { 2, (short)20, "p.antropova@gmail.com", "Полина", "Антропова", "poliantr", "270ec79c4c409fca99c98a9f6ef17b738e67df8c19d9e53ac451980ba65a368d", 3 },
                    { 3, (short)20, "nassmir@gmail.com", "Настасья", "Смирнягина", "nassmir", "f16c5b2208b0fc5831f83c66c11dd2aaec0c0c44237313e403c360b0dd797002", 1 },
                    { 4, (short)20, null, "Егор", "Воронцов", "c0nda", "b12286d98bb60853e28b1db6a10b7c483b538751d8ea4b9ec972b353a1dbb75a", 1 },
                    { 5, (short)25, "a.bakirova@gmail.com", "Анна", "Бакирова", "bakirova", "5b833e49b37d591a7a97c85b41786ddac9f2bc193c5cab13fbed87d4e938a187", 1 },
                    { 6, (short)20, null, "Никита", "Варыгин", "varigin", "59b7c4cfdf4ea1b5c9cb040b66e1544490c669c217052646e3a21c759b1ed4a7", 1 },
                    { 7, (short)20, "micapic@gmail.com", "Михаэль", "Павлов", "micapic", "3175da04d4564876b093f48d5860c9f0a5feadf75fadfbbf792d38e912983af7", 1 },
                    { 8, (short)20, null, "Мария", "Грибанова", "mgrib", "87ece3956328a95b91da363bf013fbccfb6da07aff6c3446b08df5cb1dfff582", 1 },
                    { 9, (short)24, "e.maksimova@gmail.com", "Екатерина", "Максимова", "ekatmaksim", "9e83dc23e9c2296c6e736a57f4a5264bc2324f4f148ede5ed23fa72855ee2bbc", 1 },
                    { 10, (short)40, "maksikov77@gmail.com", "Максим", "Коровкин", "77max", "511da19947dac274ec11e10bd2fd5367e74b0949a4599883f06f183ead721dab", 1 }
                });

            migrationBuilder.InsertData(
                table: "TeacherStudentRelation",
                columns: new[] { "relation_id", "status_id", "student_id", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { 1, 2, 3, 2, 1 },
                    { 2, 2, 4, 2, 1 },
                    { 3, 1, 8, 1, 1 },
                    { 4, 2, 9, 5, 2 },
                    { 5, 2, 10, 5, 2 },
                    { 6, 2, 8, 4, 2 },
                    { 7, 1, 1, 4, 2 },
                    { 8, 2, 1, 10, 3 },
                    { 9, 2, 5, 10, 3 },
                    { 10, 1, 6, 10, 3 },
                    { 11, 2, 7, 3, 8 },
                    { 12, 2, 6, 3, 8 }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubjects",
                columns: new[] { "relation_id", "subject_id", "teacher_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 2 },
                    { 6, 1, 3 },
                    { 7, 10, 3 },
                    { 8, 6, 4 },
                    { 9, 7, 4 },
                    { 10, 2, 5 },
                    { 11, 6, 5 },
                    { 12, 8, 6 },
                    { 13, 5, 6 },
                    { 14, 6, 7 },
                    { 15, 4, 7 },
                    { 16, 3, 8 },
                    { 17, 8, 9 },
                    { 18, 9, 9 },
                    { 19, 4, 7 },
                    { 20, 7, 10 }
                });

            migrationBuilder.InsertData(
                table: "Homeworks",
                columns: new[] { "homework_id", "deadline", "description", "relation_id", "solution", "solution_time", "teacher_comment" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Прочитать Доктор Живаго", 1, null, null, null },
                    { 2, new DateTime(2024, 2, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Прочитать Анну Каренину", 1, null, null, null },
                    { 3, new DateTime(2024, 2, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Прочитать Доктор Живаго", 2, null, null, null },
                    { 4, new DateTime(2024, 2, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Прочитать Анну Каренину", 2, null, null, null },
                    { 5, new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Параграф 13(Реформа Столыпина)", 6, null, null, null },
                    { 6, new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Параграф 13(Реформа Столыпина)", 7, null, null, null }
                });

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
                name: "IX_TeacherSubjects_subject_id",
                table: "TeacherSubjects",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_teacher_id",
                table: "TeacherSubjects",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_login",
                table: "Users",
                column: "login",
                unique: true);

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
                name: "TeacherSubjects");

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
