using FluentValidation;
using Organizzae.Application.DTOs.Auth;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Application.Validators.Auth
{
    public class RegistroUsuarioDtoValidator : AbstractValidator<RegistroUsuarioDto>
    {
        public RegistroUsuarioDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(CpfValido).WithMessage("CPF inválido");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .Must(EmailValido).WithMessage("E-mail inválido");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória")
                .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres");

            RuleFor(x => x.ConfirmacaoSenha)
                .Equal(x => x.Senha).WithMessage("As senhas não coincidem");
        }

        private bool CpfValido(string cpf)
        {
            return CPF.Validar(cpf);
        }

        private bool EmailValido(string email)
        {
            return Email.Validar(email);
        }
    }
}
