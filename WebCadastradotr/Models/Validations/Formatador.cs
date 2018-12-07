namespace WebCadastrador.Models
{
    public class Formatador
    {
        public static string CNPJ(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;
            if (texto.Length != 14)
                return texto;
                    
            return $"{texto.Substring(0,2)}.{texto.Substring(2, 3)}.{texto.Substring(5,3)}/{texto.Substring(8,4)}-{texto.Substring(12,2)}";
        }
        public static string CPF(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;
            if (texto.Length != 11)
                return texto;

            return $"{texto.Substring(0,3)}.{texto.Substring(3,3)}.{texto.Substring(6,3)}-{texto.Substring(9,2)}";
        }
    }
}
