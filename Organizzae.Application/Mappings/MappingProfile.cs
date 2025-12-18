using AutoMapper;
using Organizzae.Application.DTOs.Categoria;
using Organizzae.Application.DTOs.Dashboard;
using Organizzae.Application.DTOs.Despesa;
using Organizzae.Application.DTOs.Objetivo;
using Organizzae.Application.DTOs.Receita;
using Organizzae.Application.DTOs.Usuario;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Usuario Mappings
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.CPF.ObterFormatado()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Endereco));

            // Categoria Mappings
            CreateMap<Categoria, CategoriaDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));

            CreateMap<CriarCategoriaDto, Categoria>()
                .ConvertUsing<CriarCategoriaDtoToCategoria>();

            // Despesa Mappings
            CreateMap<Despesa, DespesaDto>()
                .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ForMember(dest => dest.CategoriaIcone, opt => opt.MapFrom(src => src.Categoria.Icone))
                .ForMember(dest => dest.CategoriaCor, opt => opt.MapFrom(src => src.Categoria.CorHexadecimal))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor.Valor))
                .ForMember(dest => dest.ValorFormatado, opt => opt.MapFrom(src => src.Valor.ObterFormatado()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.FormaPagamento, opt => opt.MapFrom(src => src.FormaPagamento.HasValue ? src.FormaPagamento.ToString() : null))
                .ForMember(dest => dest.TipoRecorrencia, opt => opt.MapFrom(src => src.TipoRecorrencia.ToString()))
                .ForMember(dest => dest.DiasParaVencimento, opt => opt.MapFrom(src => src.DiasParaVencimento()))
                .ForMember(dest => dest.EstaVencida, opt => opt.MapFrom(src => src.EstaVencida()));

            CreateMap<CriarDespesaDto, Despesa>()
                .ConvertUsing<CriarDespesaDtoToDespesa>();

            CreateMap<Despesa, DespesaResumoDto>()
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor.Valor))
                .ForMember(dest => dest.ValorFormatado, opt => opt.MapFrom(src => src.Valor.ObterFormatado()))
                .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ForMember(dest => dest.CategoriaCor, opt => opt.MapFrom(src => src.Categoria.CorHexadecimal));

            // Receita Mappings
            CreateMap<Receita, ReceitaDto>()
                .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ForMember(dest => dest.CategoriaIcone, opt => opt.MapFrom(src => src.Categoria.Icone))
                .ForMember(dest => dest.CategoriaCor, opt => opt.MapFrom(src => src.Categoria.CorHexadecimal))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor.Valor))
                .ForMember(dest => dest.ValorFormatado, opt => opt.MapFrom(src => src.Valor.ObterFormatado()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.TipoRecorrencia, opt => opt.MapFrom(src => src.TipoRecorrencia.ToString()))
                .ForMember(dest => dest.DiasParaRecebimento, opt => opt.MapFrom(src => src.DiasParaRecebimento()))
                .ForMember(dest => dest.EstaAtrasada, opt => opt.MapFrom(src => src.EstaAtrasada()));

            CreateMap<CriarReceitaDto, Receita>()
                .ConvertUsing<CriarReceitaDtoToReceita>();

            // Objetivo Mappings
            CreateMap<Objetivo, ObjetivoDto>()
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal.Valor))
                .ForMember(dest => dest.ValorTotalFormatado, opt => opt.MapFrom(src => src.ValorTotal.ObterFormatado()))
                .ForMember(dest => dest.ValorEconomizado, opt => opt.MapFrom(src => src.ValorEconomizado.Valor))
                .ForMember(dest => dest.ValorEconomizadoFormatado, opt => opt.MapFrom(src => src.ValorEconomizado.ObterFormatado()))
                .ForMember(dest => dest.ValorRestante, opt => opt.MapFrom(src => src.CalcularValorRestante().Valor))
                .ForMember(dest => dest.ValorRestanteFormatado, opt => opt.MapFrom(src => src.CalcularValorRestante().ObterFormatado()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PercentualProgresso, opt => opt.MapFrom(src => src.CalcularPercentualProgresso()))
                .ForMember(dest => dest.DiasRestantes, opt => opt.MapFrom(src => src.DiasRestantes()))
                .ForMember(dest => dest.ValorMensalNecessario, opt => opt.MapFrom(src => src.CalcularValorMensalNecessario().Valor))
                .ForMember(dest => dest.ValorMensalNecessarioFormatado, opt => opt.MapFrom(src => src.CalcularValorMensalNecessario().ObterFormatado()))
                .ForMember(dest => dest.EstaDentroDoPrazo, opt => opt.MapFrom(src => src.EstaDentroDoPrazo()));

            CreateMap<CriarObjetivoDto, Objetivo>()
                .ConvertUsing<CriarObjetivoDtoToObjetivo>();
        }
    }

    // Type Converters customizados

    public class CriarCategoriaDtoToCategoria : ITypeConverter<CriarCategoriaDto, Categoria>
    {
        public Categoria Convert(CriarCategoriaDto source, Categoria destination, ResolutionContext context)
        {
            var tipo = Enum.Parse<TipoCategoria>(source.Tipo);
            return new Categoria(source.Nome, tipo, source.Descricao, source.Icone, source.CorHexadecimal);
        }
    }

    public class CriarDespesaDtoToDespesa : ITypeConverter<CriarDespesaDto, Despesa>
    {
        public Despesa Convert(CriarDespesaDto source, Despesa destination, ResolutionContext context)
        {
            var valor = new Dinheiro(source.Valor);
            var tipoRecorrencia = Enum.Parse<TipoRecorrencia>(source.TipoRecorrencia);

            var despesa = new Despesa(
                source.UsuarioId,
                source.CategoriaId,
                source.Descricao,
                valor,
                source.DataVencimento,
                tipoRecorrencia
            );

            if (!string.IsNullOrWhiteSpace(source.Observacoes))
            {
                despesa.DefinirObservacoes(source.Observacoes);
            }

            return despesa;
        }
    }

    public class CriarReceitaDtoToReceita : ITypeConverter<CriarReceitaDto, Receita>
    {
        public Receita Convert(CriarReceitaDto source, Receita destination, ResolutionContext context)
        {
            var valor = new Dinheiro(source.Valor);
            var tipoRecorrencia = Enum.Parse<TipoRecorrencia>(source.TipoRecorrencia);

            var receita = new Receita(
                source.UsuarioId,
                source.CategoriaId,
                source.Descricao,
                valor,
                source.DataPrevista,
                tipoRecorrencia
            );

            if (!string.IsNullOrWhiteSpace(source.Observacoes))
            {
                receita.DefinirObservacoes(source.Observacoes);
            }

            return receita;
        }
    }

    public class CriarObjetivoDtoToObjetivo : ITypeConverter<CriarObjetivoDto, Objetivo>
    {
        public Objetivo Convert(CriarObjetivoDto source, Objetivo destination, ResolutionContext context)
        {
            var valorTotal = new Dinheiro(source.ValorTotal);
            var valorInicial = source.ValorInicial > 0 ? new Dinheiro(source.ValorInicial) : null;

            return new Objetivo(
                source.UsuarioId,
                source.Nome,
                valorTotal,
                source.DataAlvo,
                source.Descricao,
                valorInicial
            );
        }
    }
}
