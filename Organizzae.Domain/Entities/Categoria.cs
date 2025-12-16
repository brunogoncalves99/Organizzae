using Organizzae.Domain.Enums;

namespace Organizzae.Domain.Entities
{
    public class Categoria : EntidadeBase
    {
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public TipoCategoria Tipo { get; private set; }
        public string? Icone { get; private set; }
        public string CorHexadecimal { get; private set; }
        public bool EhPadrao { get; private set; }

        public ICollection<Despesa> Despesas { get; private set; }
        public ICollection<Receita> Receitas { get; private set; }

        private Categoria()
        {
            Despesas = new List<Despesa>();
            Receitas = new List<Receita>();
        }

        public Categoria(string nome, TipoCategoria tipo, string? descricao = null,
                     string? icone = null, string corHexadecimal = "#6c757d", bool ehPadrao = false)
        {
            ValidarNome(nome);
            ValidarCor(corHexadecimal);

            Nome = nome;
            Tipo = tipo;
            Descricao = descricao;
            Icone = icone;
            CorHexadecimal = corHexadecimal;
            EhPadrao = ehPadrao;

            Despesas = new List<Despesa>();
            Receitas = new List<Receita>();
        }

        public void Atualizar(string nome, string? descricao, string? icone, string corHexadecimal)
        {
            ValidarNome(nome);
            ValidarCor(corHexadecimal);

            if (EhPadrao)
                throw new InvalidOperationException("Não é possível editar uma categoria padrão do sistema");

            Nome = nome;
            Descricao = descricao;
            Icone = icone;
            CorHexadecimal = corHexadecimal;

            AtualizarDataAtualizacao();
        }

        public void AlterarTipo(TipoCategoria novoTipo)
        {
            if (EhPadrao)
                throw new InvalidOperationException("Não é possível alterar o tipo de uma categoria padrão do sistema");

            Tipo = novoTipo;
            AtualizarDataAtualizacao();
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da categoria é obrigatório", nameof(nome));

            if (nome.Length < 3)
                throw new ArgumentException("Nome da categoria deve ter no mínimo 3 caracteres", nameof(nome));

            if (nome.Length > 50)
                throw new ArgumentException("Nome da categoria deve ter no máximo 50 caracteres", nameof(nome));
        }

        private static void ValidarCor(string cor)
        {
            if (string.IsNullOrWhiteSpace(cor))
                throw new ArgumentException("Cor da categoria é obrigatória", nameof(cor));

            if (!cor.StartsWith("#") || cor.Length != 7)
                throw new ArgumentException("Cor deve estar no formato hexadecimal (#RRGGBB)", nameof(cor));
        }
    }
}
