using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Infrastructure.Data.Converters
{
    /// <summary>
    /// Conversor para persistir o Value Object Dinheiro como decimal no banco
    /// </summary>
    public class DinheiroConverter : ValueConverter<Dinheiro, decimal>
    {
        public DinheiroConverter() 
            : base(
                  dinheiro => dinheiro.Valor, 
                  valor => new Dinheiro(valor))
        {
        }
    }
}
