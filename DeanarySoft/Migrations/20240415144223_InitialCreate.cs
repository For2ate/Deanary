using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeanarySoft.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deanary");

            migrationBuilder.CreateTable(
                name: "models",
                schema: "deanary",
                columns: table => new
                {
                    model_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manufactor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    model = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    equipment_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    access_level = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("models_pkey", x => x.model_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                schema: "deanary",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    department = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    access_level = table.Column<short>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("staff_pkey", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                schema: "deanary",
                columns: table => new
                {
                    type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statuses_pkey", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                schema: "deanary",
                columns: table => new
                {
                    equipment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_id = table.Column<int>(type: "integer", nullable: false),
                    deadline_period = table.Column<DateOnly>(type: "date", nullable: false),
                    commissioning_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("equipment_pkey", x => x.equipment_id);
                    table.ForeignKey(
                        name: "equipment_model_id_fkey",
                        column: x => x.model_id,
                        principalSchema: "deanary",
                        principalTable: "models",
                        principalColumn: "model_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactphones",
                schema: "deanary",
                columns: table => new
                {
                    contact = table.Column<int>(type: "integer", nullable: false),
                    staff_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contactphones_pkey", x => new { x.contact, x.staff_id });
                    table.ForeignKey(
                        name: "contactphones_staff_id_fkey",
                        column: x => x.staff_id,
                        principalSchema: "deanary",
                        principalTable: "staff",
                        principalColumn: "staff_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "deanary",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "integer", nullable: false),
                    equipment_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    return_date = table.Column<DateOnly>(type: "date", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "requests_equipment_id_fkey",
                        column: x => x.equipment_id,
                        principalSchema: "deanary",
                        principalTable: "equipment",
                        principalColumn: "equipment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "requests_staff_id_fkey",
                        column: x => x.staff_id,
                        principalSchema: "deanary",
                        principalTable: "staff",
                        principalColumn: "staff_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status",
                schema: "deanary",
                columns: table => new
                {
                    equipment_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    date_of_assignment = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "status_equipment_id_fkey",
                        column: x => x.equipment_id,
                        principalSchema: "deanary",
                        principalTable: "equipment",
                        principalColumn: "equipment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "status_type_id_fkey",
                        column: x => x.type_id,
                        principalSchema: "deanary",
                        principalTable: "statuses",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactphones_staff_id",
                schema: "deanary",
                table: "contactphones",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_model_id",
                schema: "deanary",
                table: "equipment",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "IX_requests_equipment_id",
                schema: "deanary",
                table: "requests",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_requests_staff_id",
                schema: "deanary",
                table: "requests",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_equipment_id",
                schema: "deanary",
                table: "status",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_type_id",
                schema: "deanary",
                table: "status",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactphones",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "status",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "staff",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "equipment",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "statuses",
                schema: "deanary");

            migrationBuilder.DropTable(
                name: "models",
                schema: "deanary");
        }
    }
}
