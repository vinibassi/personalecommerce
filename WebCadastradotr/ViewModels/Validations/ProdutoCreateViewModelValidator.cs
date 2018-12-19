using FluentValidation;
using WebCadastrador.Models;

namespace WebCadastrador.ViewModels.Validations
{
    public class ProdutoCreateViewModelValidator : AbstractValidator<ProdutoCreateViewModel>
    {
        public ProdutoCreateViewModelValidator()
        {
            RuleFor(x => x.Nome).Length(0, 50).NotNull().NotEmpty();
            RuleFor(x => x.Fabricante).NotNull().NotEmpty();
            RuleFor(x => x.Preco).NotNull().NotEmpty();
        }
    }
}
