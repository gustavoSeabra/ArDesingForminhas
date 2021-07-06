using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.Models
{
    public class Banner
    {
        public int Codigo { get; set; }
        public string Caminho { get; set; }
        public bool EhHome { get; set; }
    }
}