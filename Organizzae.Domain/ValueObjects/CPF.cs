namespace Organizzae.Domain.ValueObjects
{
    public class CPF
    {
        public string Numero { get; private set; }

        public CPF() { }

        public CPF(string numero)
        {
            if (!Validar(numero))
                throw new ArgumentException("CPF inválido.");

            Numero = LimparFormatacao(numero);
        }

        /// <summary>
        /// Remove formatação do CPF (pontos e traço)
        /// </summary>
        private static string LimparFormatacao(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "").Trim();
        }

        /// <summary>
        /// Valida CPF segundo algoritmo da Receita Federal
        /// </summary>
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = LimparFormatacao(cpf);

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digitoVerificador2;
        }

        /// <summary>
        /// Retorna CPF formatado (000.000.000-00)
        /// </summary>
        public string ObterFormatado()
        {
            return $"{Numero.Substring(0, 3)}.{Numero.Substring(3, 3)}.{Numero.Substring(6, 3)}-{Numero.Substring(9, 2)}";
        }

        public override string ToString() => Numero;

        public override bool Equals(object? obj)
        {
            if (obj is not CPF other)
                return false;

            return Numero == other.Numero;
        }

        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }

        public static implicit operator string(CPF cpf) => cpf.Numero;

        public static explicit operator CPF(string numero) => new CPF(numero);

    }
}
