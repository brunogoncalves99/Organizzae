using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Infrastructure.Data.Converters
{
    /// <summary>
    /// Conversor para persistir o Value Object Cpf como string no banco
    /// </summary>
    public class CpfConverter : ValueConverter<CPF, string>
    {
        public CpfConverter()
            : base(
                cpf => cpf.Numero,
                numero => new CPF(numero))
        {
        }
    }
}
