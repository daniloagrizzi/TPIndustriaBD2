using System.Text.RegularExpressions;

namespace TPIndustriaBD2.Models
{
    public class Produto
    {
        public int ID_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public decimal Preco_medio { get; set; }
        public int Saldo { get; set; }

        public int ID_Grupo { get; set; }
        public Grupo Grupo { get; set; }

        public ICollection<Compra> Compras { get; set; }
        public ICollection<Consumo> Consumos { get; set; }
    }

}
