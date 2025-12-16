using System.Globalization;

namespace Organizzae.Domain.ValueObjects
{
    public class Dinheiro
    {
        public decimal Valor { get; private set; }

        public Dinheiro(decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor não pode ser negativo", nameof(valor));

            // Arredonda para 2 casas decimais (centavos)
            Valor = Math.Round(valor, 2);
        }

        /// <summary>
        /// Retorna o valor formatado em moeda brasileira ( Exemplo: R$ 1.234,56)
        /// </summary>
        public string ObterFormatado()
        {
            return Valor.ToString("C", new CultureInfo("pt-BR"));
        }

        /// <summary>
        /// Retorna o valor formatado sem o símbolo da moeda (Exemplo: 1.234,56)
        /// </summary>
        public string ObterFormatadoSemSimbolo()
        {
            return Valor.ToString("N2", new CultureInfo("pt-BR"));
        }

        /// <summary>
        /// Soma dois valores monetários
        /// </summary>
        public Dinheiro Somar(Dinheiro outroDinheiro)
        {
            return new Dinheiro(Valor + outroDinheiro.Valor);
        }

        /// <summary>
        /// Subtrai dois valores monetários
        /// </summary>
        public Dinheiro Subtrair(Dinheiro outroDinheiro)
        {
            decimal resultado = Valor - outroDinheiro.Valor;
            if (resultado < 0)
                throw new InvalidOperationException("Resultado da subtração não pode ser negativo");

            return new Dinheiro(resultado);
        }

        /// <summary>
        /// Multiplica o valor por um número
        /// </summary>
        public Dinheiro Multiplicar(decimal multiplicador)
        {
            if (multiplicador < 0)
                throw new ArgumentException("Multiplicador não pode ser negativo", nameof(multiplicador));

            return new Dinheiro(Valor * multiplicador);
        }

        /// <summary>
        /// Divide o valor por um número
        /// </summary>
        public Dinheiro Dividir(decimal divisor)
        {
            if (divisor <= 0)
                throw new ArgumentException("Divisor deve ser maior que zero", nameof(divisor));

            return new Dinheiro(Valor / divisor);
        }

        /// <summary>
        /// Calcula a porcentagem do valor
        /// </summary>
        public Dinheiro CalcularPorcentagem(decimal porcentagem)
        {
            return new Dinheiro((Valor * porcentagem) / 100);
        }

        /// <summary>
        /// Verifica se o valor é zero
        /// </summary>
        public bool EhZero() => Valor == 0;

        /// <summary>
        /// Verifica se o valor é maior que outro
        /// </summary>
        public bool EhMaiorQue(Dinheiro outro) => Valor > outro.Valor;

        /// <summary>
        /// Verifica se o valor é menor que outro
        /// </summary>
        public bool EhMenorQue(Dinheiro outro) => Valor < outro.Valor;

        public override string ToString() => ObterFormatado();

        public override bool Equals(object? obj)
        {
            if (obj is not Dinheiro other)
                return false;

            return Valor == other.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }

        public static Dinheiro operator +(Dinheiro a, Dinheiro b) => a.Somar(b);
        public static Dinheiro operator -(Dinheiro a, Dinheiro b) => a.Subtrair(b);
        public static Dinheiro operator *(Dinheiro a, decimal b) => a.Multiplicar(b);
        public static Dinheiro operator /(Dinheiro a, decimal b) => a.Dividir(b);
        public static bool operator >(Dinheiro a, Dinheiro b) => a.EhMaiorQue(b);
        public static bool operator <(Dinheiro a, Dinheiro b) => a.EhMenorQue(b);
        public static bool operator >=(Dinheiro a, Dinheiro b) => a.Valor >= b.Valor;
        public static bool operator <=(Dinheiro a, Dinheiro b) => a.Valor <= b.Valor;

        public static implicit operator decimal(Dinheiro dinheiro) => dinheiro.Valor;
        public static explicit operator Dinheiro(decimal valor) => new Dinheiro(valor);

        public static Dinheiro Zero() => new Dinheiro(0);
        public static Dinheiro Criar(decimal valor) => new Dinheiro(valor);

    }
}
