using WebCadastrador.Models;

namespace WebCadastrador.ViewModels
{
    public class ClientesViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int Idade { get; set; }
        public EstadoCivil estadoCivil { get; set; }
    }
}
