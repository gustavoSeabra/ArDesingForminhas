using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using ArDesignForminhas_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ArDesignForminhas_Web.Controllers
{
    public class HomeController : Controller
    {
        private IBannerRepositorio repositorioBanner;
        private IProdutoRepositorio repositorioProduto;
        private ICategoriaRepositorio repositorioCategoria;

        public HomeController(IBannerRepositorio _repositorio, IProdutoRepositorio _repositorioProduto, ICategoriaRepositorio _repositorioCategoria)
        {
            this.repositorioBanner = _repositorio;
            this.repositorioProduto = _repositorioProduto;
            this.repositorioCategoria = _repositorioCategoria;
        }

        public ActionResult Index()
        {
            return View(PreencheViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        private HomeViewModel PreencheViewModel()
        {
            var objViewModel = new HomeViewModel();

            objViewModel.ListaBanner = repositorioBanner.ListarHome();
            objViewModel.ListaProdutoDestaque = repositorioProduto.ListarDestaque();
            objViewModel.ListaCategoria = repositorioCategoria.Listar(string.Empty);

            return objViewModel;
        }

        [HttpGet]
        public string GetMenu()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var objResposta = new List<RespostaMenu>();

            var lista = repositorioCategoria.Listar(string.Empty);

            lista.ForEach(i => objResposta.Add(new RespostaMenu { Id = i.Codigo, Nome = i.Nome }));

            return serializer.Serialize(objResposta);
        }
    }
}