namespace TPIndustriaBD2.Models.ViewModels
{
    public class ProdutoDetailVM
    {
        public string NomeProduto { get; set; }
        public string NomeFornecedor { get; set; }
        public Decimal ValorCompra { get; set; }
        public Decimal ValorConsumido { get; set; }
        public int QuantidadeConsumida { get; set; }
        public int QuantidadeComprada { get; set; }
        public string DiaConsumido { get; set; }
        public string DiaCompra { get; set; }
    }
}
