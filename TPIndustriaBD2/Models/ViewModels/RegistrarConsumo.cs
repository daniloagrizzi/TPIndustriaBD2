namespace TPIndustriaBD2.Models.ViewModels
{
    public class RegistrarConsumo
    {
        public int ID_Produto { get; set; }
        public int FK_Setor { get; set; }
        public int Quantidade { get; set; }

        public ICollection<Setor> Setor { get; set; }
        public ICollection<Produto> Produto { get; set; }
        public string mensagemErro { get; set; }
    }
}
