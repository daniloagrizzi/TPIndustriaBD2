namespace TPIndustriaBD2.Models
{
    public class NotaConsumo
    {
        public int ID_Nota_Consumo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor_Total { get; set; }
        public int Quantidade_Total { get; set; }

        public int ID_Consumo { get; set; }
        public Consumo Consumo { get; set; }
    }

}
