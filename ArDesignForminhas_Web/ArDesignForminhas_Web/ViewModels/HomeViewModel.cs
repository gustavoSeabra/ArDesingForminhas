﻿using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public List<Banner> ListaBanner { get; set; }
        public List<Produto> ListaProdutoDestaque { get; set; }
        public List<Categoria> ListaCategoria { get; set; }
    }
}