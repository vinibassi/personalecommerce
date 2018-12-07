using FluentValidation;

namespace WebCadastrador.Models.Validations
{
    public class ClientesValidator : AbstractValidator<Clientes>
    {
        public ClientesValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome deve ser informado")
                .Length(3, 50).WithMessage("O campo nome deve ter entre 3 e 50 caracteres");

            RuleFor(x => x.Sobrenome)
                .NotNull().WithMessage("O campo sobrenome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo sobrenome deve ser informado")
                .Length(3, 50).WithMessage("O campo nome deve ter entre 3 e 50 caracteres");

            RuleFor(x => x.CPF)
                .NotNull().WithMessage("O campo CPF não pode ficar vazio")
                .NotEmpty().WithMessage("O campo CPF deve ser informado")
                .Length(11, 11).WithMessage("O CPF deve ter 11 caracteres")
                .Matches(@"\d+");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O campo endereço não pode ficar vazio")
                .NotNull().WithMessage("O campo endereço deve ser informado");

            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("O campo idade não pode ficar vazio")
                .NotNull().WithMessage("O campo idade deve ser informado");

            RuleFor(x => x.EstadoCivil)
                .NotEmpty().WithMessage("O campo estado civil não pode ficar vazio")
                .NotNull().WithMessage("O campo estado civil deve ser informado");
        }
        public bool IsCpf(string CPF)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            if (CPF.Length != 11)
                return false;

            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return CPF.EndsWith(digito);
        }
    }
}
