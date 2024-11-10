namespace TPIndustriaBD2.Models.ViewModels
{
    public class ProdutoDetailVM
    {
        public string NomeProduto { get; set; }
        public List<CompraViewModel> Compras { get; set; } = new List<CompraViewModel>();
        public List<ConsumoViewModel> Consumos { get; set; } = new List<ConsumoViewModel>();
    }
}
