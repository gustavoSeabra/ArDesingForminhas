using ArDesignForminhas_Web.Interfaces;
using System.Web.Mvc;

namespace ArDesignForminhas_Web.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoRepositorio repositorioProduto;

        public ProdutoController(IProdutoRepositorio _repositorioProduto)
        {
            this.repositorioProduto = _repositorioProduto;
        }


        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult detalhes(int id)
        {
            return View(this.repositorioProduto.ObeterPorCodigo(id));
        }
    }
}