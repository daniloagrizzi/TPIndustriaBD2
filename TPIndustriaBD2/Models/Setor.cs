namespace TPIndustriaBD2.Models
{
    public class Setor
    {
        public int ID_Setor { get; set; }
        public string Nome_Setor { get; set; }

        public ICollection<Consumo> Consumos { get; set; }
    }

}
