namespace WebCadastrador.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int Idade { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
    }
    public enum EstadoCivil : byte
    {
        Solteiro,
        Casado,
        Divorciado
    }
}
