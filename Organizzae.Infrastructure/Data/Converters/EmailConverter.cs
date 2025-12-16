using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Infrastructure.Data.Converters
{
    /// <summary>
    /// Conversor para persistir o Value Object Email como string no banco
    /// </summary>
    public class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter()
            : base(
              email => email.Endereco,
              endereco => new Email(endereco))
        {
        }
    }
}
