namespace TPIndustriaBD2.Models.ViewModels
{
    public class ListarProdutosConsumidosPorSetor
    {
        public string Nome_Setor { get; set; }
        public List<string> Produtos { get; set; } = new List<string>();
    }
}
