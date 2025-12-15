namespace Organizzae.Domain.Enums
{
    public enum StatusReceita
    {
        /// <summary>
        /// Receita ainda não foi recebida
        /// </summary>
        Pendente = 1,

        /// <summary>
        /// Receita foi recebida
        /// </summary>
        Recebida = 2,

        /// <summary>
        /// Receita foi cancelada/não será recebida
        /// </summary>
        Cancelada = 3
    }
}
