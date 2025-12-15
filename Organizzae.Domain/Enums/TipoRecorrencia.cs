namespace Organizzae.Domain.Enums
{
    public enum TipoRecorrencia
    {
        /// <summary>
        /// Não é recorrente (única vez)
        /// </summary>
        Nenhuma = 0,

        /// <summary>
        /// Repete mensalmente
        /// </summary>
        Mensal = 1,

        /// <summary>
        /// Repete anualmente
        /// </summary>
        Anual = 2,

        /// <summary>
        /// Repete semanalmente
        /// </summary>
        Semanal = 3,

        /// <summary>
        /// Repete quinzenalmente (a cada 15 dias)
        /// </summary>
        Quinzenal = 4
    }
}
