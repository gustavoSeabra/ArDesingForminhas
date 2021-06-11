using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.Models
{
    public class ImagemProduto
    {
        public int IdImagem { get; set; }
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
    }
}