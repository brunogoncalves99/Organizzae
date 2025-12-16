using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Entities
{
    public class Objetivo : EntidadeBase
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public Dinheiro ValorTotal { get; private set; }
        public Dinheiro ValorEconomizado { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataAlvo { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public StatusObjetivo Status { get; private set; }
        public string? ImagemRepresentativa { get; private set; }


        public Usuario Usuario { get; private set; }

        private Objetivo() { }

        public Objetivo(Guid usuarioId, string nome, Dinheiro valorTotal, DateTime dataAlvo,
                    string? descricao = null, Dinheiro? valorInicial = null)
        {
            ValidarNome(nome);
            ValidarValorTotal(valorTotal);
            ValidarDataAlvo(dataAlvo);

            UsuarioId = usuarioId;
            Nome = nome;
            Descricao = descricao;
            ValorTotal = valorTotal;
            ValorEconomizado = valorInicial ?? Dinheiro.Zero();
            DataInicio = DateTime.Now;
            DataAlvo = dataAlvo;
            Status = StatusObjetivo.EmAndamento;

            if (ValorEconomizado >= ValorTotal)
            {
                Status = StatusObjetivo.Alcancado;
                DataConclusao = DateTime.Now;
            }
        }

        public void Atualizar(string nome, string? descricao, Dinheiro valorTotal, DateTime dataAlvo)
        {
            ValidarNome(nome);
            ValidarValorTotal(valorTotal);
            ValidarDataAlvo(dataAlvo);

            if (Status == StatusObjetivo.Alcancado)
                throw new InvalidOperationException("Não é possível editar um objetivo já alcançado");

            Nome = nome;
            Descricao = descricao;
            ValorTotal = valorTotal;
            DataAlvo = dataAlvo;

            // Verifica se com o novo valor total, o objetivo já foi alcançado
            if (ValorEconomizado >= ValorTotal)
            {
                Status = StatusObjetivo.Alcancado;
                DataConclusao = DateTime.Now;
            }

            AtualizarDataAtualizacao();
        }

        public void DefinirImagemRepresentativa(string caminhoImagem)
        {
            if (string.IsNullOrWhiteSpace(caminhoImagem))
                throw new ArgumentException("Caminho da imagem não pode ser vazio", nameof(caminhoImagem));

            ImagemRepresentativa = caminhoImagem;
            AtualizarDataAtualizacao();
        }

        public void RemoverImagem()
        {
            ImagemRepresentativa = null;
            AtualizarDataAtualizacao();
        }

        public void AdicionarValor(Dinheiro valor)
        {
            if (valor == null)
                throw new ArgumentNullException(nameof(valor));

            if (Status == StatusObjetivo.Abandonado)
                throw new InvalidOperationException("Não é possível adicionar valor a um objetivo abandonado");

            if (Status == StatusObjetivo.Alcancado)
                throw new InvalidOperationException("Este objetivo já foi alcançado");

            ValorEconomizado = ValorEconomizado + valor;

            // Verifica se alcançou o objetivo
            if (ValorEconomizado >= ValorTotal)
            {
                Status = StatusObjetivo.Alcancado;
                DataConclusao = DateTime.Now;
            }

            AtualizarDataAtualizacao();
        }

        public void RemoverValor(Dinheiro valor)
        {
            if (valor == null)
                throw new ArgumentNullException(nameof(valor));

            if (Status == StatusObjetivo.Abandonado)
                throw new InvalidOperationException("Não é possível remover valor de um objetivo abandonado");

            if (valor > ValorEconomizado)
                throw new InvalidOperationException("Não é possível remover mais do que o valor economizado");

            ValorEconomizado = ValorEconomizado - valor;

            // Se estava alcançado e removeu valor, volta para em andamento
            if (Status == StatusObjetivo.Alcancado && ValorEconomizado < ValorTotal)
            {
                Status = StatusObjetivo.EmAndamento;
                DataConclusao = null;
            }

            AtualizarDataAtualizacao();
        }

        public void Pausar()
        {
            if (Status == StatusObjetivo.Alcancado)
                throw new InvalidOperationException("Não é possível pausar um objetivo já alcançado");

            if (Status == StatusObjetivo.Abandonado)
                throw new InvalidOperationException("Não é possível pausar um objetivo abandonado");

            Status = StatusObjetivo.Pausado;
            AtualizarDataAtualizacao();
        }

        public void Retomar()
        {
            if (Status != StatusObjetivo.Pausado)
                throw new InvalidOperationException("Somente objetivos pausados podem ser retomados");

            Status = StatusObjetivo.EmAndamento;
            AtualizarDataAtualizacao();
        }

        public void Abandonar()
        {
            if (Status == StatusObjetivo.Alcancado)
                throw new InvalidOperationException("Não é possível abandonar um objetivo já alcançado");

            Status = StatusObjetivo.Abandonado;
            AtualizarDataAtualizacao();
        }

        public void Reativar()
        {
            if (Status != StatusObjetivo.Abandonado)
                throw new InvalidOperationException("Somente objetivos abandonados podem ser reativados");

            Status = StatusObjetivo.EmAndamento;
            AtualizarDataAtualizacao();
        }

        /// <summary>
        /// Calcula o percentual de progresso do objetivo
        /// </summary>
        public decimal CalcularPercentualProgresso()
        {
            if (ValorTotal.EhZero())
                return 0;

            decimal percentual = (ValorEconomizado.Valor / ValorTotal.Valor) * 100;
            return Math.Round(Math.Min(percentual, 100), 2);
        }

        /// <summary>
        /// Calcula quanto falta para alcançar o objetivo
        /// </summary>
        public Dinheiro CalcularValorRestante()
        {
            if (ValorEconomizado >= ValorTotal)
                return Dinheiro.Zero();

            return ValorTotal - ValorEconomizado;
        }

        /// <summary>
        /// Calcula quantos dias faltam para a data alvo
        /// </summary>
        public int DiasRestantes()
        {
            return (DataAlvo.Date - DateTime.Now.Date).Days;
        }

        /// <summary>
        /// Calcula quanto precisa economizar por mês para alcançar o objetivo
        /// </summary>
        public Dinheiro CalcularValorMensalNecessario()
        {
            var diasRestantes = DiasRestantes();

            if (diasRestantes <= 0)
                return CalcularValorRestante();

            var mesesRestantes = Math.Max(1, diasRestantes / 30.0m);
            var valorRestante = CalcularValorRestante();

            return valorRestante / mesesRestantes;
        }

        /// <summary>
        /// Verifica se o objetivo está dentro do prazo
        /// </summary>
        public bool EstaDentroDoPrazo()
        {
            return DateTime.Now.Date <= DataAlvo.Date;
        }

        /// <summary>
        /// Verifica se o objetivo está atrasado
        /// </summary>
        public bool EstaAtrasado()
        {
            return Status == StatusObjetivo.EmAndamento && DateTime.Now.Date > DataAlvo.Date;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do objetivo é obrigatório", nameof(nome));

            if (nome.Length < 3)
                throw new ArgumentException("Nome do objetivo deve ter no mínimo 3 caracteres", nameof(nome));

            if (nome.Length > 100)
                throw new ArgumentException("Nome do objetivo deve ter no máximo 100 caracteres", nameof(nome));
        }

        private static void ValidarValorTotal(Dinheiro valorTotal)
        {
            if (valorTotal == null)
                throw new ArgumentNullException(nameof(valorTotal));

            if (valorTotal.EhZero())
                throw new ArgumentException("Valor total do objetivo deve ser maior que zero", nameof(valorTotal));
        }

        private static void ValidarDataAlvo(DateTime dataAlvo)
        {
            if (dataAlvo == default)
                throw new ArgumentException("Data alvo é obrigatória", nameof(dataAlvo));

            if (dataAlvo.Date < DateTime.Now.Date)
                throw new ArgumentException("Data alvo não pode ser no passado", nameof(dataAlvo));
        }

    }
}
