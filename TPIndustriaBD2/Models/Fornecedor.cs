namespace TPIndustriaBD2.Models
{
    public class Fornecedor
    {
        public int ID_Fornecedor { get; set; }
        public string Nome_Fornecedor { get; set; }
        public string CNPJ { get; set; }
        public string Contato { get; set; }

        public int ID_Endereco { get; set; }
        public Endereco Endereco { get; set; }

        public ICollection<Compra> Compras { get; set; }
    }

}
