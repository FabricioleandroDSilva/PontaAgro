using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ponta.Infra.Migrations
{
    /// <inheritdoc />
    public partial class crudusuariotarefas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tarefas");

            migrationBuilder.EnsureSchema(
                name: "usuario");

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nm_usuario = table.Column<string>(type: "varchar(100)", nullable: false),
                    nm_login = table.Column<string>(type: "varchar(25)", nullable: false),
                    nm_senha = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "tarefas",
                schema: "tarefas",
                columns: table => new
                {
                    id_tarefa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titulo = table.Column<string>(type: "varchar(80)", nullable: false),
                    descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "varchar(2)", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefas", x => x.id_tarefa);
                    table.ForeignKey(
                        name: "fk_usuario",
                        column: x => x.id_usuario,
                        principalSchema: "usuario",
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_id_tarefa",
                schema: "tarefas",
                table: "tarefas",
                column: "id_tarefa");

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_id_usuario",
                schema: "tarefas",
                table: "tarefas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_usuario",
                schema: "usuario",
                table: "usuario",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tarefas",
                schema: "tarefas");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "usuario");
        }
    }
}
