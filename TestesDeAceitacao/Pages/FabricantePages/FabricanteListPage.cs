using System.Collections.Generic;
using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages.FabricantePages
{
    class FabricanteListPage
    {
        public List<FabricanteCadastrado> Fabricante
        {
            get
            {
                var fabricantesCadastrados = new List<FabricanteCadastrado>();
                var linhas = SetupGlobal.Driver.FindElements(By.CssSelector("body > div > table > tbody > tr"));
                foreach (var linha in linhas)
                {
                    var colunas = linha.FindElements(By.CssSelector("td"));
                    var fabricanteCadastrado = new FabricanteCadastrado
                    {
                        Nome = colunas[0].Text,
                        CNPJ = colunas[1].Text,
                        Endereco = colunas[2].Text
                    };
                    fabricantesCadastrados.Add(fabricanteCadastrado);
                }
                return fabricantesCadastrados;
            }
        }
    }
}
