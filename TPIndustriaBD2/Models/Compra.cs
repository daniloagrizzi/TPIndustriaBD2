namespace TPIndustriaBD2.Models
{
    public class Compra
    {
        public int ID_Compra { get; set; }
        public int ID_Fornecedor { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public int ID_Produto { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public decimal Valor_Compra { get; set; }
        public DateTime Data_Compra { get; set; }
    }

}
