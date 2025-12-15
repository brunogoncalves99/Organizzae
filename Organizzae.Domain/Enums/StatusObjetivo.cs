namespace Organizzae.Domain.Enums
{
    public enum StatusObjetivo
    {
        /// <summary>
        /// Objetivo está em andamento
        /// </summary>
        EmAndamento = 1,

        /// <summary>
        /// Objetivo foi alcançado
        /// </summary>
        Alcancado = 2,

        /// <summary>
        /// Objetivo foi abandonado/cancelado
        /// </summary>
        Abandonado = 3,

        /// <summary>
        /// Objetivo está pausado temporariamente
        /// </summary>
        Pausado = 4
    }
}
