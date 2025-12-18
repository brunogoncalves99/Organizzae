using FluentValidation;
using Organizzae.Application.DTOs.Objetivo;

namespace Organizzae.Application.Validators.Objetivo
{
    public class CriarObjetivoDtoValidator : AbstractValidator<CriarObjetivoDto>
    {
        public CriarObjetivoDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome do objetivo é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.ValorTotal)
                .GreaterThan(0).WithMessage("Valor total deve ser maior que zero");

            RuleFor(x => x.ValorInicial)
                .GreaterThanOrEqualTo(0).WithMessage("Valor inicial não pode ser negativo")
                .LessThanOrEqualTo(x => x.ValorTotal).WithMessage("Valor inicial não pode ser maior que o valor total");

            RuleFor(x => x.DataAlvo)
                .NotEmpty().WithMessage("Data alvo é obrigatória")
                .GreaterThan(DateTime.Now.Date).WithMessage("Data alvo não pode ser no passado");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("Usuário é obrigatório");

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.Descricao));
        }
    }
}
