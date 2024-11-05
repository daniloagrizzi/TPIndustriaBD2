namespace TPIndustriaBD2.Models
{
    public class Grupo
    {
        public int ID_Grupo { get; set; }
        public string Nome_Grupo { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }

}
