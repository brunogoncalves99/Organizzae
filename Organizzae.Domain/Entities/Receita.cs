using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Entities
{
    public class Receita : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Descricao { get; private set; }
        public Dinheiro Valor { get; private set; }
        public DateTime DataPrevista { get; private set; }
        public DateTime? DataRecebimento { get; private set; }
        public StatusReceita Status { get; private set; }
        public TipoRecorrencia TipoRecorrencia { get; private set; }
        public string? Observacoes { get; private set; }

        public Usuario Usuario { get; private set; }
        public Categoria Categoria { get; private set; }

        private Receita() { }


        public Receita(Guid usuarioId, Guid categoriaId, string descricao, Dinheiro valor,
                   DateTime dataPrevista, TipoRecorrencia tipoRecorrencia = TipoRecorrencia.Nenhuma)
        {
            ValidarDescricao(descricao);
            ValidarDataPrevista(dataPrevista);

            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Descricao = descricao;
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
            DataPrevista = dataPrevista;
            TipoRecorrencia = tipoRecorrencia;
            Status = StatusReceita.Pendente;
        }

        public void Atualizar(string descricao, Dinheiro valor, DateTime dataPrevista,
                         Guid categoriaId, TipoRecorrencia tipoRecorrencia)
        {
            ValidarDescricao(descricao);
            ValidarDataPrevista(dataPrevista);

            if (Status == StatusReceita.Recebida)
                throw new InvalidOperationException("Não é possível editar uma receita já recebida");

            Descricao = descricao;
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
            DataPrevista = dataPrevista;
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

        public void RegistrarRecebimento(DateTime dataRecebimento)
        {
            ValidarDataRecebimento(dataRecebimento);

            if (Status == StatusReceita.Recebida)
                throw new InvalidOperationException("Esta receita já foi recebida");

            if (Status == StatusReceita.Cancelada)
                throw new InvalidOperationException("Não é possível receber uma receita cancelada");

            DataRecebimento = dataRecebimento;
            Status = StatusReceita.Recebida;

            AtualizarDataAtualizacao();
        }

        public void Cancelar()
        {
            if (Status == StatusReceita.Recebida)
                throw new InvalidOperationException("Não é possível cancelar uma receita já recebida");

            Status = StatusReceita.Cancelada;
            AtualizarDataAtualizacao();
        }

        public void ReativarReceita()
        {
            if (Status != StatusReceita.Cancelada)
                throw new InvalidOperationException("Somente receitas canceladas podem ser reativadas");

            Status = StatusReceita.Pendente;
            AtualizarDataAtualizacao();
        }

        /// <summary>
        /// Verifica se a receita está atrasada (data prevista passou e ainda não foi recebida)
        /// </summary>
        public bool EstaAtrasada()
        {
            return Status == StatusReceita.Pendente && DateTime.Now.Date > DataPrevista.Date;
        }

        /// <summary>
        /// Verifica se a receita tem previsão de recebimento hoje
        /// </summary>
        public bool RecebimentoPrevistoHoje()
        {
            return Status == StatusReceita.Pendente && DateTime.Now.Date == DataPrevista.Date;
        }

        /// <summary>
        /// Calcula quantos dias faltam para o recebimento (negativo se atrasado)
        /// </summary>
        public int DiasParaRecebimento()
        {
            return (DataPrevista.Date - DateTime.Now.Date).Days;
        }

        /// <summary>
        /// Verifica se foi recebida no prazo
        /// </summary>
        public bool FoiRecebidaNoPrazo()
        {
            if (Status != StatusReceita.Recebida || !DataRecebimento.HasValue)
                return false;

            return DataRecebimento.Value.Date <= DataPrevista.Date;
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

        private static void ValidarDataPrevista(DateTime dataPrevista)
        {
            if (dataPrevista == default)
                throw new ArgumentException("Data prevista é obrigatória", nameof(dataPrevista));
        }

        private static void ValidarDataRecebimento(DateTime dataRecebimento)
        {
            if (dataRecebimento == default)
                throw new ArgumentException("Data de recebimento é obrigatória", nameof(dataRecebimento));

            if (dataRecebimento.Date > DateTime.Now.Date)
                throw new ArgumentException("Data de recebimento não pode ser futura", nameof(dataRecebimento));
        }

    }
}
