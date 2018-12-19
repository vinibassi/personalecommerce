using Bogus;
using Bogus.Extensions.Brazil;
using System;
using WebCadastrador.Models;
using WebCadastrador.ViewModels;

namespace TestesDeUnidade
{
    public static class Generator
    {
        private static readonly Faker<Cliente> cliente = new Faker<Cliente>()
                                .StrictMode(false)
                                .CustomInstantiator(f => new Cliente())
                                .Rules((f, o) =>
                                {
                                    o.Nome = f.Name.FirstName();
                                    o.Sobrenome = f.Name.LastName();
                                    o.CPF = f.Person.Cpf().Replace(".", "").Replace("-", "");
                                    o.Endereco = f.Address.StreetAddress();
                                    o.Idade = f.Random.Number(30, 50);
                                    o.EstadoCivil = f.PickRandom<EstadoCivil>();
                                });
        private static readonly Faker<ClientesViewModel> clienteVM = new Faker<ClientesViewModel>()
                                .StrictMode(false)
                                .CustomInstantiator(f => new ClientesViewModel())
                                .Rules((f, o) =>
                                {
                                    o.Id = 1;
                                    o.Nome = f.Name.FirstName();
                                    o.Sobrenome = f.Name.LastName();
                                    o.CPF = f.Person.Cpf().Replace(".", "").Replace("-", "");
                                    o.Endereco = f.Address.StreetAddress();
                                    o.Idade = f.Random.Number(30, 50);
                                    o.estadoCivil = f.PickRandom<EstadoCivil>();
                                });
        private static readonly Faker<ClientesViewModel> invalidCliente = new Faker<ClientesViewModel>()
                                .StrictMode(false)
                                .CustomInstantiator(f => new ClientesViewModel())
                                .Rules((f, o) =>
                                {
                                    o.Id = 1;
                                    o.Nome = f.Name.FirstName();
                                    o.Sobrenome = f.Name.LastName();
                                    o.CPF = f.Person.Cpf().Replace(".", "1").Replace("-", "2");
                                    o.Endereco = f.Address.StreetAddress();
                                    o.Idade = f.Random.Number(30, 50);
                                    o.estadoCivil = f.PickRandom<EstadoCivil>();
                                });
        private static readonly Faker<Fabricante> fabricante = new Faker<Fabricante>()
                               .StrictMode(false)
                               .CustomInstantiator(f => new Fabricante())
                               .Rules((f, o) =>
                               {
                                   o.Nome = f.Company.CompanyName();
                                   o.CNPJ = f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", "");
                                   o.Endereco = f.Address.StreetAddress();
                               });
        private static readonly Faker<FabricantesViewModel> fabricanteVM = new Faker<FabricantesViewModel>()
                                .StrictMode(false)
                                .CustomInstantiator(f => new FabricantesViewModel())
                                .Rules((f, o) =>
                                {
                                    o.Id = 1;
                                    o.Nome = f.Company.CompanyName();
                                    o.CNPJ = f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", "");
                                    o.Endereco = f.Address.StreetAddress();
                                });
        private static readonly Faker<FabricantesViewModel> invalidFabricanteVM = new Faker<FabricantesViewModel>()
                         .StrictMode(false)
                         .CustomInstantiator(f => new FabricantesViewModel())
                         .Rules((f, o) =>
                         {
                             o.Id = 1;
                             o.Nome = f.Company.CompanyName();
                             o.CNPJ = f.Company.Cnpj().Replace(".", "1").Replace("-", "1").Replace("/", "1");
                             o.Endereco = f.Address.StreetAddress();
                         });
        private static readonly Faker<Produto> validProduto = new Faker<Produto>()
                          .StrictMode(false)
                          .CustomInstantiator(f => new Produto())
                          .Rules((f, o) =>
                          {
                              o.Nome = f.Commerce.ProductName();
                              o.Preco = f.Random.Int(10, 1000)+ 0.03m;
                          });
        private static readonly Faker<ProdutoEditViewModel>  validProdutoEditVM = new Faker<ProdutoEditViewModel>()
            .StrictMode(false)
                          .CustomInstantiator(f => new ProdutoEditViewModel())
                          .Rules((f, o) =>
                          {
                              o.Id = 1;
                              o.Nome = f.Commerce.ProductName();
                              o.FabricanteId = 1;
                              o.Preco = f.Random.Int(10, 1000) + 0.03m;
                          });
        private static readonly Faker<ProdutoEditViewModel> invalidProdutoEditVM = new Faker<ProdutoEditViewModel>()
            .StrictMode(false)
                          .CustomInstantiator(f => new ProdutoEditViewModel())
                          .Rules((f, o) =>
                          {
                              o.Id = 1;
                              o.Nome = f.Commerce.ProductName();
                              o.FabricanteId = 2;
                              o.Preco = Convert.ToDecimal(f.Commerce.Price().EndsWith("9"));
                          });
        private static readonly Faker<ProdutoCreateViewModel> validProdutoCreateVM = new Faker<ProdutoCreateViewModel>()
            .StrictMode(false)
                          .CustomInstantiator(f => new ProdutoCreateViewModel())
                          .Rules((f, o) =>
                          {
                              o.Nome = f.Commerce.ProductName();
                              o.Fabricante = 1;
                              o.Preco = f.Random.Int(10, 1000) + 0.03m;;
                          });
        private static readonly Faker<ProdutoCreateViewModel> invalidProdutoCreateVM = new Faker<ProdutoCreateViewModel>()
            .StrictMode(false)
                          .CustomInstantiator(f => new ProdutoCreateViewModel())
                          .Rules((f, o) =>
                          {
                              o.Nome = f.Commerce.ProductName();
                              o.Fabricante = 1;
                              o.Preco = Convert.ToDecimal(f.Commerce.Price().EndsWith("9"));
                          });
        public static ClientesViewModel ValidClienteViewModel() => clienteVM.Generate();
        public static Cliente ValidCliente() => cliente.Generate();
        public static ClientesViewModel InvalidCPFClienteViewModel() => invalidCliente.Generate();
        public static FabricantesViewModel ValidFabricanteViewModel() => fabricanteVM.Generate();
        public static Fabricante ValidFabricante() => fabricante.Generate();
        public static FabricantesViewModel InvalidCNPJFabricanteViewModel() => invalidFabricanteVM.Generate();
        public static Produto ValidProduto() => validProduto.Generate();
        public static ProdutoEditViewModel ValidProdutoEditVM() => validProdutoEditVM.Generate();
        public static ProdutoEditViewModel InvalidProdutoEditVM() => invalidProdutoEditVM.Generate();
        public static ProdutoCreateViewModel ValidProdutoCreateVM() => validProdutoCreateVM.Generate();
        public static ProdutoCreateViewModel InvalidProdutoCreateVM() => invalidProdutoCreateVM.Generate();





    }
}
