namespace TPIndustriaBD2.Models.ViewModels
{
    public class RegistrarCompraVM
    {
        public int ID_Produto { get; set; }
        public int ID_Fornecedor { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor_Compra { get; set; }
        public ICollection<Fornecedor> Fornecedor { get; set; }
        public ICollection<Produto> Produto { get; set; }

    }
}
