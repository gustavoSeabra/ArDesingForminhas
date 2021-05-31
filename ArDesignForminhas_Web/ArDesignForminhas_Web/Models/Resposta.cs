using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ArDesignForminhas_Web.Models
{
    public class Resposta
    {
        public HttpStatusCode Status { get; set; }
        public string Mensagem { get; set; }
        public string Acao { get; set; }
    }
}