namespace Organizzae.Domain.Enums
{
    public enum StatusDespesa
    {
        /// <summary>
        /// Despesa ainda não foi paga
        /// </summary>
        Pendente = 1,

        /// <summary>
        /// Despesa foi paga dentro do prazo
        /// </summary>
        Paga = 2,

        /// <summary>
        /// Despesa não foi paga e passou da data de vencimento
        /// </summary>
        Atrasada = 3,

        /// <summary>
        /// Despesa foi cancelada
        /// </summary>
        Cancelada = 4
    }
}
