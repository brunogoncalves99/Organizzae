namespace Organizzae.Domain.Enums
{
    public enum FormaPagamento
    {
        /// <summary>
        /// Pagamento em dinheiro físico
        /// </summary>
        Dinheiro = 1,

        /// <summary>
        /// Pagamento via PIX
        /// </summary>
        Pix = 2,

        /// <summary>
        /// Pagamento com cartão de débito
        /// </summary>
        CartaoDebito = 3,

        /// <summary>
        /// Pagamento com cartão de crédito
        /// </summary>
        CartaoCredito = 4,

        /// <summary>
        /// Transferência bancária (TED/DOC)
        /// </summary>
        TransferenciaBancaria = 5,

        /// <summary>
        /// Boleto bancário
        /// </summary>
        Boleto = 6,

        /// <summary>
        /// Outras formas de pagamento
        /// </summary>
        Outros = 99
    }
}
