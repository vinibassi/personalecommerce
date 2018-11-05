using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Models;

namespace WebCadastrador.ViewModels.Validations
{
    public class ProdutoCreateViewModelValidator : AbstractValidator<ProdutoCreateViewModel>
    {
        public ProdutoCreateViewModelValidator()
        {
            RuleFor(x => x.Nome).Length(0, 50).NotNull().NotEmpty();
            RuleFor(x => x.Fabricante).NotNull().NotEmpty();
            RuleFor(x => x.Preco).InclusiveBetween(1, 15).NotNull().NotEmpty();
        }
    }
}
