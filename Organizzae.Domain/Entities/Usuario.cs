using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; private set; }
        public CPF CPF { get; private set; }
        public Email Email { get; private set; }
        public string SenhaHash { get; private set; }
        public string? FotoPerfil { get; private set; }
        public DateTime? UltimoAcesso { get; private set; }


        public ICollection<Despesa> Despesas { get; private set; }
        public ICollection<Receita> Receitas { get; private set; }
        public ICollection<Objetivo> Objetivos { get; private set; }


        private Usuario()
        {
            Despesas = new List<Despesa>();
            Receitas = new List<Receita>();
            Objetivos = new List<Objetivo>();
        }

        public Usuario(string nome, CPF cpf, Email email, string senhaHash)
        {
            ValidarNome(nome);
            ValidarSenhaHash(senhaHash);

            Nome = nome;
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            SenhaHash = senhaHash;

            Despesas = new List<Despesa>();
            Receitas = new List<Receita>();
            Objetivos = new List<Objetivo>();
        }

        public void AtualizarPerfil(string nome, Email email)
        {
            ValidarNome(nome);

            Nome = nome;
            Email = email ?? throw new ArgumentNullException(nameof(email));

            AtualizarDataAtualizacao();
        }

        public void AlterarSenha(string novaSenhaHash)
        {
            ValidarSenhaHash(novaSenhaHash);

            SenhaHash = novaSenhaHash;
            AtualizarDataAtualizacao();
        }

        public void DefinirFotoPerfil(string caminhoFoto)
        {
            if (string.IsNullOrWhiteSpace(caminhoFoto))
                throw new ArgumentException("Caminho da foto não pode ser vazio", nameof(caminhoFoto));

            FotoPerfil = caminhoFoto;
            AtualizarDataAtualizacao();
        }

        public void RemoverFotoPerfil()
        {
            FotoPerfil = null;
            AtualizarDataAtualizacao();
        }

        public void RegistrarAcesso()
        {
            UltimoAcesso = DateTime.Now;
            AtualizarDataAtualizacao();
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório", nameof(nome));

            if (nome.Length < 3)
                throw new ArgumentException("Nome deve ter no mínimo 3 caracteres", nameof(nome));

            if (nome.Length > 100)
                throw new ArgumentException("Nome deve ter no máximo 100 caracteres", nameof(nome));
        }

        private static void ValidarSenhaHash(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Hash da senha é obrigatório", nameof(senhaHash));
        }

        /// <summary>
        /// Obtém as iniciais do nome para exibir quando não houver foto
        /// </summary>
        public string ObterIniciais()
        {
            var partes = Nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 1)
                return partes[0].Substring(0, Math.Min(2, partes[0].Length)).ToUpper();

            return $"{partes[0][0]}{partes[^1][0]}".ToUpper();
        }
    }
}
