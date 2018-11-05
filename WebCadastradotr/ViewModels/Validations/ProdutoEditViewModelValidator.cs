using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCadastrador.Models;

namespace WebCadastrador.ViewModels.Validations
{
    public class ProdutoEditViewModelValidator : AbstractValidator<ProdutoEditViewModel>
    {
        public ProdutoEditViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Nome).NotEmpty().NotNull().Length(0,50);
            RuleFor(x => x.FabricanteId).NotEmpty().NotNull();
            RuleFor(x => x.Preco).NotEmpty().NotNull();
        }
    }
}
