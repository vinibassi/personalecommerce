using FluentValidation;

namespace WebCadastrador.Models.Validations
{
    public class FabricanteValidator : AbstractValidator<Fabricante>
    {
        public FabricanteValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Nome).NotNull().NotEmpty().Length(0, 50);
            RuleFor(x => x.CNPJ).NotNull().NotEmpty().Length(14, 14).Matches(@"\d+");
            RuleFor(x => x.Endereco).NotNull().NotEmpty();
        }
        public bool IsCnpj(string CNPJ)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            CNPJ = CNPJ.Trim();

            if (CNPJ.Length != 14)
                return false;

            tempCnpj = CNPJ.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return CNPJ.EndsWith(digito);
        }
    }
}
