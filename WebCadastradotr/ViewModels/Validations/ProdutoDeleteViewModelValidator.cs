using FluentValidation;
using WebCadastrador.Models;

namespace WebCadastrador.ViewModels
{
    public class ProdutoDeleteViewModelValidator : AbstractValidator<ProdutoDeleteViewModel>
    {
        public ProdutoDeleteViewModelValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Nome).NotNull().NotEmpty().Length(0, 50);
            RuleFor(x => x.Fabricante).NotNull().NotEmpty();
            RuleFor(x => x.Preco).NotNull().NotEmpty();
        }
    }
}
