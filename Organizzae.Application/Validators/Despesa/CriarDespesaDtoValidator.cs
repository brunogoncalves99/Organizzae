using FluentValidation;
using Organizzae.Application.DTOs.Despesa;

namespace Organizzae.Application.Validators.Despesa
{
    public class CriarDespesaDtoValidator : AbstractValidator<CriarDespesaDto>
    {
        public CriarDespesaDtoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres")
                .MaximumLength(200).WithMessage("Descrição deve ter no máximo 200 caracteres");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(x => x.DataVencimento)
                .NotEmpty().WithMessage("Data de vencimento é obrigatória");

            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage("Categoria é obrigatória");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("Usuário é obrigatório");

            RuleFor(x => x.Observacoes)
                .MaximumLength(500).WithMessage("Observações devem ter no máximo 500 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.Observacoes));
        }
    }

}
