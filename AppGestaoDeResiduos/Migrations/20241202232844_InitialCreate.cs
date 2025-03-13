using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_endereco",
                columns: table => new
                {
                    endereco_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cep = table.Column<int>(type: "NUMBER(10)", maxLength: 8, nullable: true),
                    estado = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: true),
                    cidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    rua = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    numero = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_endereco", x => x.endereco_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_localizacao",
                columns: table => new
                {
                    tb_localizacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    longitude = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    latitude = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_localizacao", x => x.tb_localizacao);
                });

            migrationBuilder.CreateTable(
                name: "tb_notificacao",
                columns: table => new
                {
                    notificacao_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    mensagem = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_notificacao", x => x.notificacao_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    agendou_coleta = table.Column<bool>(type: "NUMBER(1)", nullable: true),
                    foi_notificado = table.Column<bool>(type: "NUMBER(1)", nullable: true),
                    endereco_id = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.usuario_id);
                    table.ForeignKey(
                        name: "FK_tb_usuario_tb_endereco_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "tb_endereco",
                        principalColumn: "endereco_id");
                });

            migrationBuilder.CreateTable(
                name: "tb_caminhao",
                columns: table => new
                {
                    caminhao_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    placa = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: true),
                    qtd_de_coletas = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    qtd_de_coletas_max = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    localizacao_id = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_caminhao", x => x.caminhao_id);
                    table.ForeignKey(
                        name: "FK_tb_caminhao_tb_localizacao_localizacao_id",
                        column: x => x.localizacao_id,
                        principalTable: "tb_localizacao",
                        principalColumn: "tb_localizacao");
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_notificacao",
                columns: table => new
                {
                    usuario_notificacao_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_notificacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    notificacao_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    usuario_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_notificacao", x => x.usuario_notificacao_id);
                    table.ForeignKey(
                        name: "FK_tb_usuario_notificacao_tb_notificacao_notificacao_id",
                        column: x => x.notificacao_id,
                        principalTable: "tb_notificacao",
                        principalColumn: "notificacao_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_usuario_notificacao_tb_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "tb_usuario",
                        principalColumn: "usuario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_coleta",
                columns: table => new
                {
                    coleta_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    qtd_de_coletas = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    data_coleta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    endereco_id = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    caminhao_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_coleta", x => x.coleta_id);
                    table.ForeignKey(
                        name: "FK_tb_coleta_tb_caminhao_caminhao_id",
                        column: x => x.caminhao_id,
                        principalTable: "tb_caminhao",
                        principalColumn: "caminhao_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_coleta_tb_endereco_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "tb_endereco",
                        principalColumn: "endereco_id");
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_coleta",
                columns: table => new
                {
                    usuario_coleta_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    usuario_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    coleta_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_coleta", x => x.usuario_coleta_id);
                    table.ForeignKey(
                        name: "FK_tb_usuario_coleta_tb_coleta_coleta_id",
                        column: x => x.coleta_id,
                        principalTable: "tb_coleta",
                        principalColumn: "coleta_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_usuario_coleta_tb_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "tb_usuario",
                        principalColumn: "usuario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_caminhao_localizacao_id",
                table: "tb_caminhao",
                column: "localizacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coleta_caminhao_id",
                table: "tb_coleta",
                column: "caminhao_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coleta_endereco_id",
                table: "tb_coleta",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_endereco_id",
                table: "tb_usuario",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_coleta_coleta_id",
                table: "tb_usuario_coleta",
                column: "coleta_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_coleta_usuario_id",
                table: "tb_usuario_coleta",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_notificacao_notificacao_id",
                table: "tb_usuario_notificacao",
                column: "notificacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_notificacao_usuario_id",
                table: "tb_usuario_notificacao",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_usuario_coleta");

            migrationBuilder.DropTable(
                name: "tb_usuario_notificacao");

            migrationBuilder.DropTable(
                name: "tb_coleta");

            migrationBuilder.DropTable(
                name: "tb_notificacao");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_caminhao");

            migrationBuilder.DropTable(
                name: "tb_endereco");

            migrationBuilder.DropTable(
                name: "tb_localizacao");
        }
    }
}
