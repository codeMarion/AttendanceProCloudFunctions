using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceProCloudFunctions.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    StudyLevel = table.Column<string>(nullable: true),
                    CourseYear = table.Column<string>(nullable: true),
                    RegStatus = table.Column<string>(nullable: true),
                    CourseTitle = table.Column<string>(nullable: true),
                    CourseCode = table.Column<string>(nullable: true),
                    Teaching = table.Column<int>(nullable: false),
                    Attended = table.Column<int>(nullable: false),
                    Explained = table.Column<int>(nullable: false),
                    NonAttended = table.Column<int>(nullable: false),
                    AttendancePercentage = table.Column<float>(nullable: false),
                    UnexcusedAttendancePercentage = table.Column<float>(nullable: false),
                    LastAttendance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
