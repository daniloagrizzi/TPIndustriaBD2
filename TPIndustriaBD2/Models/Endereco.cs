namespace TPIndustriaBD2.Models
{
    public class Endereco
    {
        public int ID_Endereco { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string CEP { get; set; }

        public int ID_Fornecedor { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }

}
