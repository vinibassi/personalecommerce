using System.Collections.Generic;
using OpenQA.Selenium;

namespace TestesDeAceitacao.Pages
{
    class ClientesListPage
    {
        public List<ClienteCadastrado> Clientes
        {
            get
            {
                var clientesCadastrados = new List<ClienteCadastrado>();
                var linhas = SetupGlobal.Driver.FindElements(By.CssSelector("#clientes>tbody>tr"));
                foreach (var linha in linhas)
                {
                    var colunas = linha.FindElements(By.CssSelector("td"));
                    var clienteCadastrado = new ClienteCadastrado
                    {
                        Nome = colunas[0].Text,
                        Sobrenome = colunas[1].Text,
                        CPF = colunas[2].Text
                    };
                    clientesCadastrados.Add(clienteCadastrado);
                }
                return clientesCadastrados;
            }
        }
        
    }
}
