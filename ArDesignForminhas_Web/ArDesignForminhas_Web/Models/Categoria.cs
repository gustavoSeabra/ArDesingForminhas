using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.Models
{
    public class Categoria
    {
        [Display(Name = "Código")]
        public int Codigo { get; set; }
        [Display(Name = "Cód. Categoria Pai")]
        public int? CodigoPai { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}