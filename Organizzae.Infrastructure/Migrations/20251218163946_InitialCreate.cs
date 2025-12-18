using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Organizzae.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorHexadecimal = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false, defaultValue: "#6c757d"),
                    EhPadrao = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FotoPerfil = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UltimoAcesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FormaPagamento = table.Column<int>(type: "int", nullable: true),
                    TipoRecorrencia = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AnexoComprovante = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorEconomizado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlvo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ImagemRepresentativa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objetivos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TipoRecorrencia = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receitas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Ativo", "CorHexadecimal", "DataAtualizacao", "DataCriacao", "Descricao", "EhPadrao", "Icone", "Nome", "Tipo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "#FF6B6B", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aluguel, condomínio, IPTU, manutenção", true, "fa-home", "Moradia", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "#4ECDC4", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Supermercado, restaurantes, delivery", true, "fa-utensils", "Alimentação", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, "#FFE66D", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Combustível, transporte público, manutenção veículo", true, "fa-car", "Transporte", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, "#95E1D3", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plano de saúde, medicamentos, consultas", true, "fa-heartbeat", "Saúde", 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), true, "#A8E6CF", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursos, livros, mensalidade escolar", true, "fa-graduation-cap", "Educação", 1 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), true, "#C7CEEA", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cinema, viagens, hobbies", true, "fa-gamepad", "Lazer", 1 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), true, "#FFDAC1", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roupas, calçados, acessórios", true, "fa-tshirt", "Vestuário", 1 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), true, "#B4A7D6", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Energia, água, internet, telefone", true, "fa-file-invoice-dollar", "Contas e Serviços", 1 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), true, "#6C757D", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesas diversas", true, "fa-ellipsis-h", "Outros", 1 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), true, "#51CF66", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salário mensal, décimo terceiro", true, "fa-money-bill-wave", "Salário", 2 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), true, "#339AF0", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trabalhos autônomos, freelas", true, "fa-laptop-code", "Freelance", 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), true, "#FF6B6B", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dividendos, rendimentos, ações", true, "fa-chart-line", "Investimentos", 2 },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), true, "#FFD43B", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vendas de produtos ou serviços", true, "fa-shopping-cart", "Vendas", 2 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), true, "#6C757D", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receitas diversas", true, "fa-plus-circle", "Outros", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EhPadrao",
                table: "Categorias",
                column: "EhPadrao");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nome_Tipo",
                table: "Categorias",
                columns: new[] { "Nome", "Tipo" });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Tipo",
                table: "Categorias",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DataVencimento",
                table: "Despesas",
                column: "DataVencimento");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_Status",
                table: "Despesas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_TipoRecorrencia",
                table: "Despesas",
                column: "TipoRecorrencia");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId_DataVencimento",
                table: "Despesas",
                columns: new[] { "UsuarioId", "DataVencimento" });

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_DataAlvo",
                table: "Objetivos",
                column: "DataAlvo");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_Status",
                table: "Objetivos",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_UsuarioId",
                table: "Objetivos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_UsuarioId_Status",
                table: "Objetivos",
                columns: new[] { "UsuarioId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_CategoriaId",
                table: "Receitas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_DataPrevista",
                table: "Receitas",
                column: "DataPrevista");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_Status",
                table: "Receitas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_TipoRecorrencia",
                table: "Receitas",
                column: "TipoRecorrencia");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioId",
                table: "Receitas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioId_DataPrevista",
                table: "Receitas",
                columns: new[] { "UsuarioId", "DataPrevista" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Receitas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
