using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.ViewModels
{
    public class CategoriaViewModel
    {
        public Categoria objCategoria { get; set; }
        public List<Produto> listaProdutos { get; set; }
    }
}