using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cyclings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    TimeTaken = table.Column<double>(type: "double precision", nullable: false),
                    AverageSpeed = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cyclings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cyclings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hikings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    TimeTaken = table.Column<double>(type: "double precision", nullable: false),
                    ElevationGain = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hikings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hikings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Runnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    TimeTaken = table.Column<double>(type: "double precision", nullable: false),
                    AverageSpeed = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Runnings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Swimmings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Laps = table.Column<int>(type: "integer", nullable: false),
                    TimeTaken = table.Column<double>(type: "double precision", nullable: false),
                    AverageHeartRate = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swimmings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Swimmings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Walkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Steps = table.Column<int>(type: "integer", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    TimeTaken = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walkings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sets = table.Column<int>(type: "integer", nullable: false),
                    Repetitions = table.Column<int>(type: "integer", nullable: false),
                    WeightLifted = table.Column<double>(type: "double precision", nullable: false),
                    CaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CalorieTarget = table.Column<double>(type: "double precision", nullable: false),
                    TotalCaloriesBurned = table.Column<double>(type: "double precision", nullable: false),
                    IsAchieved = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WalkingId = table.Column<int>(type: "integer", nullable: true),
                    CyclingId = table.Column<int>(type: "integer", nullable: true),
                    RunningId = table.Column<int>(type: "integer", nullable: true),
                    SwimmingId = table.Column<int>(type: "integer", nullable: true),
                    WeightsId = table.Column<int>(type: "integer", nullable: true),
                    HikingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Cyclings_CyclingId",
                        column: x => x.CyclingId,
                        principalTable: "Cyclings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Hikings_HikingId",
                        column: x => x.HikingId,
                        principalTable: "Hikings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Runnings_RunningId",
                        column: x => x.RunningId,
                        principalTable: "Runnings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Swimmings_SwimmingId",
                        column: x => x.SwimmingId,
                        principalTable: "Swimmings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_Walkings_WalkingId",
                        column: x => x.WalkingId,
                        principalTable: "Walkings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Weights_WeightsId",
                        column: x => x.WeightsId,
                        principalTable: "Weights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cyclings_UserId",
                table: "Cyclings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_CyclingId",
                table: "Goals",
                column: "CyclingId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_HikingId",
                table: "Goals",
                column: "HikingId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_RunningId",
                table: "Goals",
                column: "RunningId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_SwimmingId",
                table: "Goals",
                column: "SwimmingId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_WalkingId",
                table: "Goals",
                column: "WalkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_WeightsId",
                table: "Goals",
                column: "WeightsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hikings_UserId",
                table: "Hikings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Runnings_UserId",
                table: "Runnings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Swimmings_UserId",
                table: "Swimmings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Walkings_UserId",
                table: "Walkings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_UserId",
                table: "Weights",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Cyclings");

            migrationBuilder.DropTable(
                name: "Hikings");

            migrationBuilder.DropTable(
                name: "Runnings");

            migrationBuilder.DropTable(
                name: "Swimmings");

            migrationBuilder.DropTable(
                name: "Walkings");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
