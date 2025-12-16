using System.Text.RegularExpressions;

namespace Organizzae.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa um endereço de e-mail válido
    /// </summary>
    public class Email
    {
        public string Endereco { get; private set; }
        private Email() { }

        public Email(string endereco)
        {
            if(!Validar(endereco))
                throw new ArgumentException("E-mail inválido", nameof(endereco));

            Endereco = endereco.Trim();
        }

        /// <summary>
        /// Valida se o e-mail está em formato válido
        /// </summary>
        public static bool Validar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Regex para validação de e-mail
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém o domínio do e-mail (parte após @)
        /// </summary>
        public string ObterDominio()
        {
            return Endereco.Split('@')[1];
        }

        /// <summary>
        /// Obtém o nome de usuário do e-mail (parte antes do @)
        /// </summary>
        public string ObterNomeUsuario()
        {
            return Endereco.Split('@')[0];
        }

        public override string ToString() => Endereco;

        public override bool Equals(object? obj)
        {
            if (obj is not Email other)
                return false;

            return Endereco == other.Endereco;
        }

        public override int GetHashCode()
        {
            return Endereco.GetHashCode();
        }

        public static implicit operator string(Email email) => email.Endereco;

        public static explicit operator Email(string endereco) => new Email(endereco);

    }
}
