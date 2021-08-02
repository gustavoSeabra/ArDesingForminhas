using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ArDesignForminhas_Web.Models
{
    public class RespostaMenu
    {
        public HttpStatusCode Status { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}