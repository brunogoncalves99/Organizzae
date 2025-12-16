using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;

namespace Organizzae.Infrastructure.Data.Seed;

public class CategoriaSeed : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        var dataAtual = new DateTime(2024, 1, 1);

        builder.HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Nome = "Moradia",
                Descricao = "Aluguel, condomínio, IPTU, manutenção",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-home",
                CorHexadecimal = "#FF6B6B",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Nome = "Alimentação",
                Descricao = "Supermercado, restaurantes, delivery",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-utensils",
                CorHexadecimal = "#4ECDC4",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nome = "Transporte",
                Descricao = "Combustível, transporte público, manutenção veículo",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-car",
                CorHexadecimal = "#FFE66D",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Nome = "Saúde",
                Descricao = "Plano de saúde, medicamentos, consultas",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-heartbeat",
                CorHexadecimal = "#95E1D3",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Nome = "Educação",
                Descricao = "Cursos, livros, mensalidade escolar",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-graduation-cap",
                CorHexadecimal = "#A8E6CF",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Nome = "Lazer",
                Descricao = "Cinema, viagens, hobbies",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-gamepad",
                CorHexadecimal = "#C7CEEA",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Nome = "Vestuário",
                Descricao = "Roupas, calçados, acessórios",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-tshirt",
                CorHexadecimal = "#FFDAC1",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Nome = "Contas e Serviços",
                Descricao = "Energia, água, internet, telefone",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-file-invoice-dollar",
                CorHexadecimal = "#B4A7D6",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                Nome = "Outros",
                Descricao = "Despesas diversas",
                Tipo = TipoCategoria.Despesa,
                Icone = "fa-ellipsis-h",
                CorHexadecimal = "#6C757D",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            }
        );

        builder.HasData(
            new
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Nome = "Salário",
                Descricao = "Salário mensal, décimo terceiro",
                Tipo = TipoCategoria.Receita,
                Icone = "fa-money-bill-wave",
                CorHexadecimal = "#51CF66",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Nome = "Freelance",
                Descricao = "Trabalhos autônomos, freelas",
                Tipo = TipoCategoria.Receita,
                Icone = "fa-laptop-code",
                CorHexadecimal = "#339AF0",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Nome = "Investimentos",
                Descricao = "Dividendos, rendimentos, ações",
                Tipo = TipoCategoria.Receita,
                Icone = "fa-chart-line",
                CorHexadecimal = "#FF6B6B",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Nome = "Vendas",
                Descricao = "Vendas de produtos ou serviços",
                Tipo = TipoCategoria.Receita,
                Icone = "fa-shopping-cart",
                CorHexadecimal = "#FFD43B",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            },

            new
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                Nome = "Outros",
                Descricao = "Receitas diversas",
                Tipo = TipoCategoria.Receita,
                Icone = "fa-plus-circle",
                CorHexadecimal = "#6C757D",
                EhPadrao = true,
                DataCriacao = dataAtual,
                Ativo = true
            }
        );
    }
}
