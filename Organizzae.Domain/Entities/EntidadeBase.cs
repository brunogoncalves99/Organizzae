namespace Organizzae.Domain.Entities
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public bool Ativo { get; private set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
            Ativo = true;
        }

        protected EntidadeBase(Guid id)
        {
            Id = id;
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
            Ativo = true;
        }

        public void Ativar()
        {
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
            AtualizarDataAtualizacao();
        }

        public void Desativar()
        {
            Ativo = false;
            AtualizarDataAtualizacao();
        }

        public void AtualizarDataAtualizacao()
        {
            DataAtualizacao = DateTime.UtcNow;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntidadeBase other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntidadeBase? a, EntidadeBase? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(EntidadeBase? a, EntidadeBase? b)
        {
            return !(a == b);
        }
    }
}
