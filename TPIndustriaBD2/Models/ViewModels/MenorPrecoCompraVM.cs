namespace TPIndustriaBD2.Models.ViewModels
{
    public class MenorPrecoCompraVM
    {
        public int ID_Compra { get; set; }
        public string Nome_Fornecedor { get; set; }

        public int Quantidade { get; set; }

        public Decimal Desconto { get; set; }

        public Decimal ValorCompra { get; set; }

        public string DataCompra    { get; set; }
    }
}
