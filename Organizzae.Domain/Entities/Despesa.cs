using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Entities
{
    public class Despesa : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Descricao { get; private set; }
        public Dinheiro Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public StatusDespesa Status { get; private set; }
        public FormaPagamento? FormaPagamento { get; private set; }
        public TipoRecorrencia TipoRecorrencia { get; private set; }
        public string? Observacoes { get; private set; }
        public string? AnexoComprovante { get; private set; }

        public Usuario Usuario { get; private set; }
        public Categoria Categoria { get; private set; }


        private Despesa() { }


        public Despesa(Guid usuarioId, Guid categoriaId, string descricao, Dinheiro valor,
                   DateTime dataVencimento, TipoRecorrencia tipoRecorrencia = TipoRecorrencia.Nenhuma)
        {
            ValidarDescricao(descricao);
            ValidarDataVencimento(dataVencimento);

            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Descricao = descricao;
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
            DataVencimento = dataVencimento;
            TipoRecorrencia = tipoRecorrencia;
            Status = StatusDespesa.Pendente;
        }

        public void Atualizar(string descricao, Dinheiro valor, DateTime dataVencimento,
                         Guid categoriaId, TipoRecorrencia tipoRecorrencia)
        {
            ValidarDescricao(descricao);
            ValidarDataVencimento(dataVencimento);

            if (Status == StatusDespesa.Paga)
                throw new InvalidOperationException("Não é possível editar uma despesa já paga");

            Descricao = descricao;
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
            DataVencimento = dataVencimento;
            CategoriaId = categoriaId;
            TipoRecorrencia = tipoRecorrencia;

            AtualizarDataAtualizacao();
        }

        public void DefinirObservacoes(string observacoes)
        {
            if (!string.IsNullOrWhiteSpace(observacoes) && observacoes.Length > 500)
                throw new ArgumentException("Observações devem ter no máximo 500 caracteres", nameof(observacoes));

            Observacoes = observacoes;
            AtualizarDataAtualizacao();
        }

        public void AnexarComprovante(string caminhoArquivo)
        {
            if (string.IsNullOrWhiteSpace(caminhoArquivo))
                throw new ArgumentException("Caminho do arquivo não pode ser vazio", nameof(caminhoArquivo));

            AnexoComprovante = caminhoArquivo;
            AtualizarDataAtualizacao();
        }

        public void RemoverComprovante()
        {
            AnexoComprovante = null;
            AtualizarDataAtualizacao();
        }

        public void RegistrarPagamento(DateTime dataPagamento, FormaPagamento formaPagamento)
        {
            ValidarDataPagamento(dataPagamento);

            if (Status == StatusDespesa.Paga)
                throw new InvalidOperationException("Esta despesa já foi paga");

            if (Status == StatusDespesa.Cancelada)
                throw new InvalidOperationException("Não é possível pagar uma despesa cancelada");

            DataPagamento = dataPagamento;
            FormaPagamento = formaPagamento;
            Status = StatusDespesa.Paga;

            AtualizarDataAtualizacao();
        }

        public void Cancelar()
        {
            if (Status == StatusDespesa.Paga)
                throw new InvalidOperationException("Não é possível cancelar uma despesa já paga");

            Status = StatusDespesa.Cancelada;
            AtualizarDataAtualizacao();
        }

        public void MarcarComoAtrasada()
        {
            if (Status != StatusDespesa.Pendente)
                return;

            if (DateTime.Now.Date > DataVencimento.Date)
            {
                Status = StatusDespesa.Atrasada;
                AtualizarDataAtualizacao();
            }
        }

        public void ReativarDespesa()
        {
            if (Status != StatusDespesa.Cancelada)
                throw new InvalidOperationException("Somente despesas canceladas podem ser reativadas");

            Status = DateTime.Now.Date > DataVencimento.Date
                ? StatusDespesa.Atrasada
                : StatusDespesa.Pendente;

            AtualizarDataAtualizacao();
        }

        /// <summary>
        /// Verifica se a despesa está vencida
        /// </summary>
        public bool EstaVencida()
        {
            return Status == StatusDespesa.Pendente && DateTime.Now.Date > DataVencimento.Date;
        }

        /// <summary>
        /// Verifica se a despesa vence hoje
        /// </summary>
        public bool VenceHoje()
        {
            return Status == StatusDespesa.Pendente && DateTime.Now.Date == DataVencimento.Date;
        }

        /// <summary>
        /// Calcula quantos dias faltam para o vencimento (negativo se atrasado)
        /// </summary>
        public int DiasParaVencimento()
        {
            return (DataVencimento.Date - DateTime.Now.Date).Days;
        }

        private static void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição é obrigatória", nameof(descricao));

            if (descricao.Length < 3)
                throw new ArgumentException("Descrição deve ter no mínimo 3 caracteres", nameof(descricao));

            if (descricao.Length > 200)
                throw new ArgumentException("Descrição deve ter no máximo 200 caracteres", nameof(descricao));
        }

        private static void ValidarDataVencimento(DateTime dataVencimento)
        {
            if (dataVencimento == default)
                throw new ArgumentException("Data de vencimento é obrigatória", nameof(dataVencimento));
        }

        private static void ValidarDataPagamento(DateTime dataPagamento)
        {
            if (dataPagamento == default)
                throw new ArgumentException("Data de pagamento é obrigatória", nameof(dataPagamento));

            if (dataPagamento.Date > DateTime.Now.Date)
                throw new ArgumentException("Data de pagamento não pode ser futura", nameof(dataPagamento));
        }
    }
}
