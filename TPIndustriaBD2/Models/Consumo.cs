namespace TPIndustriaBD2.Models
{
    public class Consumo
    {
        public int ID_Consumo { get; set; }
        public int ID_Setor { get; set; }
        public Setor Setor { get; set; }

        public int ID_Produto { get; set; }
        public Produto Produto { get; set; }

        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public int ID_Nota_Consumo { get; set; }
        public NotaConsumo NotaConsumo { get; set; }
    }

}
