using System.Collections.Generic;


namespace ArDesignForminhas_Web.Models
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CodCategoria { get; set; }
        public string CaminhoFoto { get; set; }
    }
}